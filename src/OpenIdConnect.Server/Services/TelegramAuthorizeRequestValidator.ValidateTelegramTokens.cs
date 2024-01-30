namespace Telegram.OpenIdConnect.Services;

using System.Collections.Specialized;
using System.Security.Claims;
using System.Threading.Tasks;

using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Validation;

using Humanizer;

using IdentityModel;

using Telegram.OpenIdConnect.Constants;
using Telegram.OpenIdConnect.Extensions;
using AuthenticationMethods = Telegram.OpenIdConnect.Enums.AuthenticationMethods;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

using ErrorMessage = Constants.ErrorMessage;

public partial class TelegramAuthorizeRequestValidator
{
    public const string DefaultNonce = "default-nonce";

    private AuthorizeRequestValidationResult ValidateTelegramTokens(
        ValidatedAuthorizeRequest request
    )
    {
        using var activity = Telemetry.Activities.ServiceActivitySource.StartActivity(
            $"{nameof(TelegramAuthorizeRequestValidator)}.{nameof(ValidateTelegramTokens)}"
        );

        var userData = request.Raw;

        if (
            !userData.Keys
                .OfType<string>()
                .Contains(DataCheckKeys.AuthHash, StringComparer.CurrentCultureIgnoreCase)
        )
        {
            return new(request);
        }

        if (!long.TryParse(userData[DataCheckKeys.AuthTime], out var longAuthTime))
        {
            return LogErrorAndReturn(request, ErrorMessage.AuthTimeMissing.Instance);
        }

        var authTime = DateTimeOffset.FromUnixTimeSeconds(longAuthTime);
        if (
            (authTime - 5.Minutes()) > DateTimeOffset.UtcNow
            || (authTime + 5.Minutes()) < DateTimeOffset.UtcNow
        )
        {
            return LogErrorAndReturn(request, ErrorMessage.AuthTimeOutOfRange.Instance);
        }

        var generatedHash = ComputeHash(userData);
        if (!generatedHash.Equals(userData[DataCheckKeys.AuthHash]))
        {
            return LogErrorAndReturn(request, ErrorMessage.HashValidationFailed.Instance);
        }

        var claims = request.ClientClaims;

        var firstName = userData[DataCheckKeys.FirstName] ?? "";
        var lastName = userData[DataCheckKeys.LastName] ?? "";
        var name = Concat(firstName, !IsNullOrEmpty(lastName) ? $" {lastName}" : "");
        var username = userData[DataCheckKeys.Username];
        var photoUrl = userData[DataCheckKeys.PhotoUrl];
        var hash = userData[DataCheckKeys.AuthHash];
        var id = long.Parse(userData[DataCheckKeys.Id]);
        var authDate = DateTimeOffset.FromUnixTimeSeconds(
            long.Parse(userData[DataCheckKeys.AuthTime])
        );
        var uri = UserUri.Create(id);

        var now = DateTimeOffset.UtcNow;
        var expiration = DateTimeOffset.UtcNow.AddDays(7);

        claims.Add(new(Claims.FirstName, firstName));
        claims.Add(new(Ct.AuthenticationInstant, authDate.ToString("O")));
        claims.Add(
            new(
                Ct.AuthenticationMethod,
                AuthenticationMethods.MultiFactorAuthentication.GetDescription()
            )
        );
        claims.Add(new(Ct.Expiration, expiration.ToString("O"), Cvt.DateTime));
        claims.Add(new(Ct.GivenName, firstName, Cvt.String));
        claims.Add(new(Ct.Hash, hash, Cvt.Base64Binary));
        claims.Add(new(Ct.Name, name, Cvt.String));
        claims.Add(new(Ct.NameIdentifier, userData[DataCheckKeys.Id], Cvt.Integer64));
        claims.Add(new(Ct.Surname, lastName, Cvt.String));
        claims.Add(new(Ct.Upn, username ?? id.ToString(), Cvt.String));
        claims.Add(new(Ct.Uri, uri.ToString(), DgmjrCvt.AnyUri.UriString));
        claims.Add(new(Ct.Webpage, uri.ToString(), DgmjrCvt.AnyUri.UriString));
        claims.Add(
            new(JwtRegisteredClaimNames.AuthTime, authDate.ToUnixTimeSecondsString(), Cvt.DateTime)
        );
        claims.Add(new(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSecondsString(), Cvt.DateTime));
        claims.Add(
            new(JwtRegisteredClaimNames.Exp, expiration.ToUnixTimeSecondsString(), Cvt.DateTime)
        );
        claims.Add(new(JwtRegisteredClaimNames.FamilyName, lastName, Cvt.String));
        claims.Add(new(JwtRegisteredClaimNames.GivenName, firstName, Cvt.String));
        claims.Add(
            new(
                JwtRegisteredClaimNames.Iss,
                TelegramAuthenticationDefaults.ClaimsIssuer,
                DgmjrCvt.AnyUri.UriString
            )
        );
        claims.Add(
            new(JwtRegisteredClaimNames.Nbf, authDate.ToUnixTimeSecondsString(), Cvt.DateTime)
        );
        claims.Add(new(JwtRegisteredClaimNames.Nonce, request.Nonce ?? DefaultNonce, Cvt.String));
        claims.Add(new(JwtRegisteredClaimNames.Sub, id.ToString(), Cvt.String));
        claims.Add(new(JwtRegisteredClaimNames.UniqueName, username ?? id.ToString(), Cvt.String));
        claims.Add(new(JwtRegisteredClaimNames.Website, uri.ToString(), DgmjrCvt.AnyUri.UriString));
        claims.Add(new(IdentityServerConstants.Idp, TelegramAuthenticationDefaults.IdentityProvider, DgmjrCvt.String.UriString));
        claims.Add(new(IdentityServerConstants.UserId, id.ToString(), Cvt.Integer64));
        claims.Add(new(FirstName.UriString, firstName, Cvt.String));
        claims.Add(new(LastName.UriString, lastName, Cvt.String));
        claims.Add(new(PhotoUrl.UriString, photoUrl, DgmjrCvt.AnyUri.UriString));
        claims.Add(new(UserId.UriString, id.ToString(), Cvt.String));
        claims.Add(new(Username.UriString, username, Cvt.String));

        request.Subject = new([new(claims)]);

        return new(request);
    }

    private AuthorizeRequestValidationResult LogErrorAndReturn(
        ValidatedAuthorizeRequest request,
        Constants.Abstractions.IErrorMessage errorMessage
    )
    {
        Logger.TelegramAuthenticationError(errorMessage, request);
        return new(request, errorMessage.ShortName, errorMessage.Description);
    }

    /// <summary>
    ///     Generates a hash as mentioned in <see href="https://core.telegram.org/widgets/login">Telegram Login Widget</see>
    ///     documentation.
    /// </summary>
    /// <param name="data">Key/value pairs of user data.</param>
    /// <returns>Hash of key/value pairs to validate against Telegram provided hash.</returns>
    private string ComputeHash(NameValueCollection data)
    {
        using var hashComputationActivity = Telemetry.Activities.ValidationActivitySource.StartActivity(
            $"{nameof(TelegramAuthorizeRequestValidator)}.{nameof(ComputeHash)}"
        );
        var components = DataCheckKeys.All
            .OfType<string>()
            .Where(x => x is not DataCheckKeys.AuthHash)
            .OrderBy(x => x)
            .Select(x => Concat(x, (char)0x3d, data[x]));

        var dataCheckString = Join((char)0x0a + "", components);

        byte[] key;
        key =  null;//SHA256.HashData(UTF8.GetBytes(TgOidcOptions.Clients[data.].BotApiToken.ToString()));

        byte[] hash;
        using (var hmac = new HMACSHA256(key))
        {
            hash = hmac.ComputeHash(UTF8.GetBytes(dataCheckString));
        }

        var result = new StringBuilder();
        foreach (var b in hash)
            result.Append(b.ToString("x2"));
        return result.ToString();
    }
}
