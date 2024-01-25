using System.Configuration;

using CorrelationId;

using Microsoft.IdentityModel.JsonWebTokens;

using OneOf;

using Telegram.OpenIdConnect.Extensions;
using Telegram.OpenIdConnect.Models.Responses;

namespace Telegram.OpenIdConnect.Models.Responses;

public class TokenResponse : Message
{
    public TokenResponse()
    {
        TokenType = Enums.TokenType.Bearer.GetDescription();
    }

    /// <summary>
    /// Oauth 2
    /// </summary>
    [JProp("access_token")]
    public string AccessToken { get; set; }

    /// <summary>
    /// OpenId Connect
    /// </summary>
    [JProp("id_token")]
    public string IdToken { get; set; }

    /// <summary>
    /// By default is Bearer
    /// </summary>
    [JProp("token_type")]
    public string TokenType { get; set; }

    /// <summary>
    /// Authorization Code. This is always returned when using the Hybrid Flow.
    /// </summary>
    [JProp("code")]
    public string Code { get; set; }
}

public class TokenResponseOrError : OneOfBase<TokenResponse, ErrorResponse>
{
    public TokenResponseOrError(TokenResponse response)
        : base(response) { }

    public TokenResponseOrError(ErrorResponse error)
        : base(error) { }

    public static implicit operator TokenResponseOrError(TokenResponse response) => new(response);

    public static implicit operator TokenResponseOrError(ErrorResponse error) => new(error);

    public static implicit operator TokenResponse(TokenResponseOrError? response) =>
        response.IsT0 ? response.AsT0 : throw new InvalidCastException();

    public static implicit operator ErrorResponse(TokenResponseOrError? response) =>
        response.IsT1 ? response.AsT1 : throw new InvalidCastException();

    public TokenResponse Result => AsT0;
    public ErrorResponse Error => AsT1;
    public bool IsResult => IsT0;
    public bool IsError => IsT1;
}
