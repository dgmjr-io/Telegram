using System.Collections;

using Telegram.OpenIdConnect.Constants;
using Telegram.OpenIdConnect.Enums;
using Telegram.OpenIdConnect.Extensions;
using Telegram.OpenIdConnect.Models.Responses;

using ErrorResponse = Telegram.OpenIdConnect.Models.Responses.ErrorResponse;

namespace Telegram.OpenIdConnect.Models.Requests;

public class TokenRequest : Message<TokenResponse, ErrorResponse>, IEnumerable<StrKvp>
{
    [JProp("code")]
    public string Code { get; set; }

    [JProp("id")]
    public long Id { get; set; }

    [JProp("hash")]
    public string AuthHash { get; set; }

    [JProp("auth_date")]
    public string AuthDate { get; set; }

    [JProp("first_name")]
    public string FirstName { get; set; }

    [JProp("last_name")]
    public string LastName { get; set; }

    [JProp("username")]
    public string Username { get; set; }

    [JProp("photo_url")]
    public string PhotoUrl { get; set; }

    [JProp("redirect_uri")]
    public string RedirectUri { get; set; }

    [JProp("client_id")]
    public string ClientId { get; set; }

    public string? this[string key]
    {
        get
        {
            var property =
                GetType().GetProperty(key)
                ?? GetType().GetProperty(key.SnakeCaseToPascalCase())
                ?? GetType()
                    .GetRuntimeProperties()
                    .FirstOrDefault(p => p.GetCustomAttribute<JPropAttribute>()?.Name == key);
            if (property is null)
            {
                return null;
            }

            var value = property.GetValue(this);
            return value?.ToString();
        }
    }

    public TokenResponse CreateTokenResponse(string accessToken, string? idToken = default) =>
        new()
        {
            AccessToken = accessToken,
            IdToken = idToken ?? accessToken,
            TokenType = TokenType.Bearer.GetDescription(),
            Code = Code,
            CorrelationId = CorrelationId,
        };

    public IEnumerator<StrKvp> GetEnumerator()
    {
        return DataCheckKeys.All.Select(key => new StrKvp(key, this[key])).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
