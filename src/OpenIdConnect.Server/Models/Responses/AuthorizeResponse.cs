namespace Telegram.OpenIdConnect.Models.Responses;

using OneOf;

using Telegram.OpenIdConnect.Enums;
using Telegram.OpenIdConnect.Errors.Abstractions;

public class AuthorizeResponse : Message<AuthorizeResponse, ErrorResponse>
{
    /// <summary>
    /// code or implicit grant or client credential
    /// </summary>
    public string ResponseType { get; set; }
    public string Code { get; set; }

    /// <summary>
    /// required if it was present in the client authorization request
    /// </summary>
    public string State { get; set; }

    public string RedirectUri { get; set; }
    public IList<string> RequestedScopes { get; set; }
    public string GrantType { get; set; }
    public string Nonce { get; set; }

    // public string Error { get; set; } = string.Empty;
    // public string ErrorUri { get; set; }
    // public string ErrorDescription { get; set; }
    // public bool HasError => !IsNullOrEmpty(Error);
    // public bool IsSuccess => !HasError;
}

public class AuthorizeResponseOrError : OneOfBase<AuthorizeResponse, ErrorResponse>
{
    public AuthorizeResponseOrError(AuthorizeResponse response)
        : base(response) { }

    public AuthorizeResponseOrError(ErrorResponse error)
        : base(error) { }

    public static implicit operator AuthorizeResponseOrError(AuthorizeResponse response) =>
        new(response);

    public static implicit operator AuthorizeResponseOrError(ErrorResponse error) => new(error);

    public static implicit operator AuthorizeResponse(AuthorizeResponseOrError? response) =>
        response.IsT0 ? response.AsT0 : throw new InvalidCastException();

    public static implicit operator ErrorResponse(AuthorizeResponseOrError? response) =>
        response.IsT1 ? response.AsT1 : throw new InvalidCastException();

    public AuthorizeResponse Result => AsT0;
    public ErrorResponse Error => AsT1;
    public bool IsResult => IsT0;
    public bool IsError => IsT1;
}
