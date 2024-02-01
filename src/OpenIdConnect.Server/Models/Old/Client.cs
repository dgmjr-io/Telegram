namespace Telegram.OpenIdConnect.Models.Responses;

public class Client
{
    public Client() { }

    [JProp("client_name")]
    public string ClientName { get; set; }

    [JProp("client_id")]
    public string ClientId { get; set; }

    /// <summary>
    /// Client Password
    /// </summary>
    [JProp("client_secret")]
    public string ClientSecret { get; set; }

    [JProp("grant_types")]
    public IList<string> GrantType { get; set; }

    /// <summary>
    /// by default false
    /// </summary>
    [JProp("is_active")]
    public bool IsActive { get; set; } = false;

    [JProp("allowed_scopes")]
    public IList<string> AllowedScopes { get; set; }

    [JProp("request_uris")]
    public Uri[] ClientUris { get; set; }

    [JProp("logo_uri")]
    public Uri LogoUri { get; set; }

    [JProp("redirect_uris")]
    public Uri[] RedirectUris { get; set; }

    [JIgnore]
    public string[] RedirectUriStrings => RedirectUris.Select(x => x.ToString()).ToArray();
}
