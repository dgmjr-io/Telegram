namespace Telegram.OpenIdConnect.Models;

using BotApiToken = Telegram.Bot.Types.BotApiToken;

public class TelegramOidcClient : Duende.IdentityServer.Models.Client
{
    [Required, RegularExpression(BotApiToken.RegexString)]
    public BotApiToken BotApiToken { get; set; } = BotApiToken.Empty;

    [Required]
    public string BotUsername { get; set; } = string.Empty;

    [Required]
    public string BotName
    {
        get => ClientName ?? BotUsername;
        set => ClientName = value;
    }
}
