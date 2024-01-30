namespace Telegram.OpenIdConnect.Models;

using Telegram.OpenIdConnect.Constants;
using Telegram.OpenIdConnect.Options;

public class LoginViewModel(TelegramOpenIdConnectServerOptions options, HttpContext context)
{
    public TelegramOpenIdConnectServerOptions Options => options;
    private const string ClientIdKey = "client_id";

    public string? AuthHash => context.Request.Query[DataCheckKeys.AuthHash];

    public string? ClientId =>
        !IsNullOrEmpty(context.Request.Query[ClientIdKey])
            ? context.Request.Query[ClientIdKey]
            : !IsNullOrEmpty(context.Request.Form[ClientIdKey])
                ? context.Request.Form[ClientIdKey]
                : !IsNullOrEmpty(context.Request.Cookies[ClientIdKey])
                    ? context.Request.Cookies[ClientIdKey]
                    : Options.Clients.BotNames.FirstOrDefault();

    public TelegramOidcClient? Client => Options.Clients[ClientId];

    public string? BotName => Client?.BotName;

    public string? BotUsername => Client?.BotUsername;

    public string? BotAvatarUrl => "";
}
