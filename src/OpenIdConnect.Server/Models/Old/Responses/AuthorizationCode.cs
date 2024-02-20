using System.Security.Claims;

using OneOf;
using ClaimsPrincipal = System.Security.Claims.ClaimsPrincipal;

namespace Telegram.OpenIdConnect.Models.Responses;

public class AuthorizationCode
{
    public Client Client { get; set; }

    [JProp("code")]
    public string Code { get; set; }

    [JProp("client_id")]
    public string ClientId { get; set; }

    [JProp("client_secret")]
    public string ClientSecret { get; set; }

    [JProp("redirect_uris")]
    public string[] RedirectUris { get; set; }

    [JProp("is_active")]
    public bool IsActive { get; set; } = true;

    [JProp("creation_time")]
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;

    [JProp("is_openid")]
    public bool IsOpenId =>
        RequestedScopes.Contains("openid", StringComparer.OrdinalIgnoreCase)
        || RequestedScopes.Contains("profile", StringComparer.OrdinalIgnoreCase);

    [JProp("requested_scopes")]
    public IList<string> RequestedScopes { get; set; }

    [JProp("subject")]
    public ClaimsPrincipal Subject { get; set; }

    [JProp("nonce")]
    public string Nonce { get; set; }
}

public class AuthorizationCodeOrError : OneOfBase<AuthorizationCode, ErrorResponse>
{
    public AuthorizationCodeOrError(AuthorizationCode code)
        : base(code) { }

    public AuthorizationCodeOrError(ErrorResponse error)
        : base(error) { }

    public static implicit operator AuthorizationCodeOrError(AuthorizationCode code) => new(code);

    public static implicit operator AuthorizationCodeOrError(ErrorResponse error) => new(error);

    public static implicit operator AuthorizationCode(AuthorizationCodeOrError? code) =>
        code.IsT0 ? code.AsT0 : throw new InvalidCastException();

    public static implicit operator ErrorResponse(AuthorizationCodeOrError? code) =>
        code.IsT1 ? code.AsT1 : throw new InvalidCastException();

    public AuthorizationCode Result => AsT0;
    public ErrorResponse Error => AsT1;
    public bool IsResult => IsT0;
    public bool IsError => IsT1;
}
