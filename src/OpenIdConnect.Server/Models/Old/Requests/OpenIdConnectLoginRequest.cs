namespace Telegram.OpenIdConnect.Models.Requests;

public class TelegramOpenIdConnectLoginRequest
{
    [JProp("id")]
    public long Id { get; set; }

    [JProp("first_name")]
    public string GivenName { get; set; }

    [JProp("last_name")]
    public string FamilyName { get; set; }

    [JProp("username")]
    public string Username { get; set; }

    [JProp("photo_url")]
    public string PhotoUrl { get; set; }

    [JIgnore]
    public Uri PhotoUri => new(PhotoUrl);

    [JIgnore]
    public byte[] PhotoBytes => PhotoUri.DownloadBytes();

    [JProp("auth_date")]
    public long AuthDate { get; set; }

    [JProp("hash")]
    public string Hash { get; set; }
}
