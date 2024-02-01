namespace Telegram.AspNetCore.Authentication;

using Telegram.OpenIdConnect.Constants;
using Telegram.OpenIdConnect.Enums;
using Telegram.OpenIdConnect.Extensions;

using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using Humanizer;

using OneOf;

using Telegram.OpenIdConnect.Errors;
using Telegram.OpenIdConnect.Models.Requests;
using Telegram.OpenIdConnect.Models.Responses;
using Telegram.OpenIdConnect.Options;

using ErrorType = OpenIdConnect.Errors.ErrorType;

public class TelegramJwtFactory(
    IHttpContextAccessor httpContextAccessor,
    IOptionsMonitor<TelegramOpenIdConnectServerOptions> options,
    ILogger<TelegramJwtFactory> logger,
    TimeProvider time
) : ITelegramJwtFactory
{
    public ILogger Logger => logger;
    private HttpContext HttpContext => httpContextAccessor.HttpContext!;

    // private HttpRequest Request => HttpContext.Request;
    private TelegramOpenIdConnectServerOptions Options => options.CurrentValue;
    private JsonWebKey JsonWebKey => Options.JsonWebKeySet.Keys[0];
    public const string DefaultNonce = "default-nonce";

    public TokenResponseOrError GenerateToken(TokenRequest request)
    {
        var userData = request;

        if (!long.TryParse(userData[DataCheckKeys.AuthTime], out var longAuthTime))
        {
            Logger.LogInformation("Authorization time not found.");
            return request.CreateErrorResponse(ErrorType.InvalidData.Instance);
        }

        var authTime = DateTimeOffset.FromUnixTimeSeconds(longAuthTime);
        if (
            (authTime - 5.Minutes()) > time.GetUtcNow()
            || (authTime + 5.Minutes()) < time.GetUtcNow()
        )
        {
            Logger.LogInformation("Time provided by is not without an acceptable range.");
            return request.CreateErrorResponse(ErrorType.InvalidTimestamp.Instance);
        }

        var generatedHash = ComputeHash(userData);
        if (!generatedHash.Equals(userData[DataCheckKeys.AuthHash]))
        {
            Logger.LogInformation("Hash verification failed.");
            return request.CreateErrorResponse(ErrorType.InvalidHash.Instance);
        }

        return request.CreateTokenResponse(CreateToken(userData));
    }

    /// <summary>
    ///     Creates claims for the provided data from Telegram.
    /// </summary>
    /// <param name="userData">Key/value pairs of Telegram provided user data.</param>
    /// <returns>An authentication ticket.</returns>
    public string CreateToken(TokenRequest userData)
    {
        var identity = new ClaimsIdentity(TelegramAuthenticationDefaults.ClaimsIssuer);

        var firstName = userData[DataCheckKeys.FirstName];
        var lastName = userData[DataCheckKeys.LastName];
        var name = Concat(firstName, !IsNullOrEmpty(lastName) ? $" {lastName}" : "");
        var username = userData[DataCheckKeys.Username];
        var photoUrl = userData[DataCheckKeys.PhotoUrl];
        var hash = userData[DataCheckKeys.AuthHash];
        var id = long.Parse(userData[DataCheckKeys.Id]);
        var authDate = DateTimeOffset.FromUnixTimeSeconds(
            long.Parse(userData[DataCheckKeys.AuthTime])
        );
        var uri = UserUri.Create(id);
        ;
        var expiration = time.GetUtcNow().AddDays(7);

        identity.AddClaims(
            new Claim[]
            {
                new(Claims.FirstName, firstName),
                new(Ct.AuthenticationInstant, authDate.ToString("O")),
                new(
                    Ct.AuthenticationMethod,
                    AuthenticationMethods.MultiFactorAuthentication.GetDescription()
                ),
                new(Ct.Expiration, expiration.ToString("O")),
                new(Ct.GivenName, firstName),
                new(Ct.Hash, hash),
                new(Ct.Name, name),
                new(Ct.NameIdentifier, userData[DataCheckKeys.Id]),
                new(Ct.Surname, lastName),
                new(Ct.Upn, username ?? id.ToString()),
                new(Ct.Uri, uri.ToString()),
                new(Ct.Webpage, uri.ToString()),
                new(JwtRegisteredClaimNames.AuthTime, authDate.ToUnixTimeSeconds().ToString()),
                new(JwtRegisteredClaimNames.Exp, expiration.ToUnixTimeSeconds().ToString()),
                new(JwtRegisteredClaimNames.FamilyName, lastName),
                new(JwtRegisteredClaimNames.GivenName, firstName),
                new(JwtRegisteredClaimNames.Iss, TelegramAuthenticationDefaults.ClaimsIssuer),
                new(JwtRegisteredClaimNames.Nbf, authDate.ToUnixTimeSeconds().ToString()),
                new(JwtRegisteredClaimNames.Nonce, DefaultNonce),
                new(JwtRegisteredClaimNames.Sub, id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, username ?? id.ToString()),
                new(JwtRegisteredClaimNames.Website, uri.ToString()),
                new(FirstName.UriString, firstName),
                new(LastName.UriString, lastName),
                new(PhotoUrl.UriString, photoUrl),
                new(UserId.UriString, id.ToString()),
                new(Username.UriString, username),
            }
        );

        var signingCredentials = new SigningCredentials(JsonWebKey, SecurityAlgorithms.RsaSha256);

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = new JwtSecurityToken(
            issuer: TelegramAuthenticationDefaults.ClaimsIssuer,
            audience: TelegramAuthenticationDefaults.ClaimsIssuer,
            claims: identity.Claims,
            notBefore: authDate.DateTime,
            expires: expiration.DateTime,
            signingCredentials: signingCredentials
        );
        var jwtString = tokenHandler.WriteToken(token);
        var signedJwt = tokenHandler.CreateEncodedJwt(
            issuer: TelegramAuthenticationDefaults.ClaimsIssuer,
            audience: TelegramAuthenticationDefaults.ClaimsIssuer,
            subject: identity,
            notBefore: authDate.DateTime,
            expires: expiration.DateTime,
            issuedAt: authDate.DateTime,
            signingCredentials: signingCredentials
        );

        return signedJwt;
    }

    /// <summary>
    ///     Decode from base64 User Data submitted by a form.
    /// </summary>
    /// <param name="query"><inheritdoc cref="IQueryCollection" />.</param>
    /// <returns>Key/value pairs of user data provided by Telegram.</returns>
    private static StringDictionary? GetUserDataFromQueryString(IQueryCollection query)
    {
        return DataCheckKeys.All.ToDictionary(x => x, x => query[x].FirstOrDefault());
    }

    /// <summary>
    ///     Generates a hash as mentioned in <see href="https://core.telegram.org/widgets/login">Telegram Login Widget</see>
    ///     documentation.
    /// </summary>
    /// <param name="data">Key/value pairs of user data.</param>
    /// <returns>Hash of key/value pairs to validate against Telegram provided hash.</returns>
    private string ComputeHash(TokenRequest data)
    {
        var components = data.Where(x => x.Key is not DataCheckKeys.AuthHash)
            .OrderBy(x => x.Key)
            .Select(x => Concat(x.Key, (char)0x3d, x.Value));

        var dataCheckString = Join((char)0x0a + "", components);

        byte[] key;
        key = SHA256.HashData(UTF8.GetBytes(Options.Clients[data.ClientId].BotApiToken.ToString()));

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
