namespace Telegram.OpenIdConnect.Models;

using Telegram.OpenIdConnect.Constants;
using Telegram.OpenIdConnect.Extensions;
using Telegram.OpenIdConnect.Options;

public class LoginViewModel(TelegramOpenIdConnectServerOptions options, HttpContext context)
{
    public TelegramOpenIdConnectServerOptions Options => options;
    private const string ClientIdKey = "client_id";

    public string? AuthHash => context.Request.Query[DataCheckKeys.AuthHash];

    public string? ClientId => context.Request.GetClientId();

    public TelegramOidcClient? Client => context.Request.GetClient();

    public string? BotName => Client?.BotName;

    public string? BotUsername => Client?.BotUsername;

    public string? BotAvatarUrl => "";
}
