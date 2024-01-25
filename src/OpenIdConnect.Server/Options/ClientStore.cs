namespace Telegram.OpenIdConnect.Options;

using Client = Duende.IdentityServer.Models.Client;

public class ClientStore : List<Client>
{
    public const string ConfigurationSectionKey =
        $"{TelegramOpenIdConnectServerOptions.ConfigurationSectionKey}:{nameof(Client)}s";
}
