using System.Text.Json.Serialization;

using Telegram.OpenIdConnect.Models.Responses;

using ErrorResponse = Telegram.OpenIdConnect.Models.Responses.ErrorResponse;

namespace Telegram.OpenIdConnect.Models.Requests;

public class AuthorizationRequest : Message<AuthorizeResponse, ErrorResponse>
{
    public AuthorizationRequest() { }

    /// <summary>
    /// Response Type, is required
    /// </summary>
    [JProp("response_type")]
    public string ResponseType { get; set; }

    /// <summary>
    /// Client Id, is required
    /// </summary>
    [JProp("client_id")]
    public string ClientId { get; set; }

    /// <summary>
    /// ClientSecret, is required
    /// </summary>
    [JProp("client_secret")]
    public string ClientSecret { get; set; }

    /// <summary>
    /// Redirect Uri, is optional
    /// The redirection endpoint URI MUST be an absolute URI as defined by
    /// [RFC3986] Section 4.3
    /// </summary>
    [JProp("redirect_uri")]
    public string RedirectUri { get; set; }

    /// <summary>
    /// Optional
    /// </summary>
    [JProp("scope")]
    public string Scope { get; set; }

    /// <summary>
    /// Return the state in the result
    /// if it was present in the client authorization request
    /// </summary>
    [JProp("state")]
    public string State { get; set; }
}
