using Dgmjr.Configuration.Extensions;

using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Telegram.OpenIdConnect.Options
{
    public class IdentityServerAutoConfigurator
        : IConfigureIHostApplicationBuilder,
            IConfigureIApplicationBuilder
    {
        public ConfigurationOrder Order => ConfigurationOrder.Early;

        public void Configure(IHostApplicationBuilder builder)
        {
            var config = builder.Configuration.GetSection(TelegramOpenIdConnectServerOptions.ConfigurationSectionKey).Get<TelegramOpenIdConnectServerOptions>();
            var clients = builder.Configuration
                .GetSection(ClientStore.ConfigurationSectionKey)
                .Get<ClientStore>();

            Console.WriteLine($"Adding clients: {Join(", ", clients.Select(c => Serialize(c)))}");
            builder.Services
                .AddIdentityServer()
                .AddInMemoryIdentityResources([new IdentityResources.OpenId()])
                .AddInMemoryClients(clients)
                .AddSigningCredential(config.SigningCredentials);

            /* do nothing */
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseIdentityServer();
        }
    }
};
