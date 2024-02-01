namespace Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Hosting;

using Telegram.AspNetCore.Authentication;
using Telegram.OpenIdConnect.Json;
using Telegram.OpenIdConnect.Middleware;
using Telegram.OpenIdConnect.Options;
using Telegram.OpenIdConnect.Services;
using Telegram.OpenIdConnect.Services.CodeService;

public class TelegramOpenIdConnectServerAutoConfigurator
    : IConfigureIApplicationBuilder,
        IConfigureIHostApplicationBuilder
{
    public ConfigurationOrder Order => ConfigurationOrder.AnyTime;

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseMiddleware<ClientSessionMiddleware>();
    }

    public void Configure(WebApplicationBuilder builder)
    {
        builder.Services.Configure<TelegramOpenIdConnectServerOptions>(options =>
        {
            builder.Configuration
                .GetSection(TelegramOpenIdConnectServerOptions.ConfigurationSectionKey)
                .Bind(options);
        });
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSingleton<ClientSessionMiddleware>();
        builder.Services.AddSingleton<ICodeStoreService, CodeStoreService>();
        builder.Services.AddSingleton<IAuthorizationService, AuthorizationService>();
        builder.Services.AddSingleton<ITelegramJwtFactory, TelegramJwtFactory>();
        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services
            .AddMvc()
            .AddJsonOptions(jso =>
            {
                jso.JsonSerializerOptions.DictionaryKeyPolicy = JNaming.SnakeCaseLower;
                jso.JsonSerializerOptions.PropertyNamingPolicy = JNaming.SnakeCaseLower;
                jso.JsonSerializerOptions.DefaultIgnoreCondition = JIgnore.Never;
                jso.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                jso.JsonSerializerOptions.Converters.Add(new EnumToDisplayStringJsonConverter());
                jso.JsonSerializerOptions.Converters.Add(
                    new System.Globalization.JsonLocaleConverter()
                );
            });
        builder.Services.AddScoped<Telegram.OpenIdConnect.Events.TelegramOpenIdConnectEvents>();
        builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        builder.Services.AddSingleton<IUrlHelper>(
            y => new UrlHelper(y.GetRequiredService<IActionContextAccessor>().ActionContext)
        );
    }
}
