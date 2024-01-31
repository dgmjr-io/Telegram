namespace Telegram.OpenIdConnect.Models;

using BotApiToken = Telegram.Bot.Types.BotApiToken;

public class TelegramOidcClient : Duende.IdentityServer.Models.Client
{
    [Required, RegularExpression(BotApiToken.RegexString)]
    public BotApiToken BotApiToken { get => BotApiToken.From(ClientSecrets.FirstOrDefault()?.Value); set => ClientSecrets = new[] { new Duende.IdentityServer.Models.Secret(value.ToString()) }; }

    [Required]
    public string BotUsername { get => ClientId; set => ClientId = value;}

    [Required]
    public string BotName
    {
        get => ClientName ?? BotUsername;
        set => ClientName = value;
    }
}
