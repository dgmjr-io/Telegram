namespace Telegram.OpenIdConnect.Options;

using BotApiToken = Telegram.Bot.Types.BotApiToken;
using Telegram.OpenIdConnect.Models;
using Client = Duende.IdentityServer.Models.Client;

public class ClientStore : List<TelegramOidcClient>
{
    public const string ConfigurationSectionKey =
        $"{TelegramOpenIdConnectServerOptions.ConfigurationSectionKey}:{nameof(Client)}s";

    public TelegramOidcClient? this[string identifier] =>
        Find(client => client.BotUsername == identifier || client.ClientId == identifier);

    public TelegramOidcClient? this[BotApiToken botApiToken] =>
        Find(client => client.BotApiToken == botApiToken);

    public bool Contains(BotApiToken botApiToken) => this[botApiToken] != null;

    public bool Contains(string identifier) => this[identifier] != null;

    public virtual string[] BotNames => this.Select(client => client.BotName).ToArray();
}
