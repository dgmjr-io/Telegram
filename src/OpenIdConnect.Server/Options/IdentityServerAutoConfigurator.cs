using Dgmjr.Configuration.Extensions;

using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

using Telegram.OpenIdConnect.Services;

namespace Telegram.OpenIdConnect.Options
{ // https://localhost:7003/connect/authorize?client_id=jsonschema.xyz&redirect_uri=https://localhost:7003/signin-oidc
    public class IdentityServerAutoConfigurator()
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
                .AddInMemoryIdentityResources([new IdentityResources.OpenId(), new IdentityResources.Profile(), new IdentityResources.Email()])
                .AddInMemoryClients(clients)
                .AddSigningCredential(config.SigningCredentials)
                .AddAuthorizeInteractionResponseGenerator<TelegramAuthorizeInteractionResponseGenerator>()
                .AddCustomAuthorizeRequestValidator<TelegramAuthorizeRequestValidator>();

            builder.Services.RemoveAll<IAuthorizeRequestValidator>();
            builder.Services.AddTransient<IAuthorizeRequestValidator, TelegramAuthorizeRequestValidator>();

            /* do nothing */
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseIdentityServer();
        }
    }
};
