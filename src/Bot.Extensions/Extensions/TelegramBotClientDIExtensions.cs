using Telegram.Bot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Scriban.Parsing;

namespace Microsoft.Extensions.DependencyInjection;

public static class TelegramBotClientDIExtensions
{
    public static IServiceCollection AddTelegramBot(this IServiceCollection services, IConfiguration config, string configurationKey = BotConfiguration.Key, bool configureWebhook = true)
    {
        services.AddTelegramBot<TelegramBotClient>(config, configurationKey, configureWebhook);
        return services;
    }
    public static IServiceCollection AddTelegramBot<TBot>(this IServiceCollection services, IConfiguration config, string configurationKey = BotConfiguration.Key, bool configureWebhook = true)
        where TBot : class, ITelegramBotClient
    {
        // Register named HttpClient to get benefits of IHttpClientFactory
        // and consume it with ITelegramBotClient typed client.
        // More read:
        //  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests#typed-clients
        //  https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
        services
            .AddHttpClient("telegram_bot_client")
            .AddTypedClient<ITelegramBotClient, TBot>();
        services.AddSingleton<ITelegramBotClient>(y => y.GetRequiredService<TBot>());
        services.AddSingleton<TBot>();
        services.ConfigureBotOptions(config, configurationKey);
        if(configureWebhook)
        {
            services.ConfigureBotWebhook();
        }
        return services;
    }

    public static IServiceCollection ConfigureBotOptions(this IServiceCollection services, IConfiguration config, string configurationKey = BotConfiguration.Key)
    {
        var botConfigSection = config.GetSection(configurationKey);
        var botConfig = new BotConfiguration(botConfigSection.GetValue<string>(nameof(BotConfiguration.Token)))
        {
            HostAddress = !IsNullOrEmpty(botConfigSection.GetValue<string>(nameof(BotConfiguration.HostAddress))) ? new Uri(botConfigSection.GetValue<string>(nameof(BotConfiguration.HostAddress))) : null,
            Route = !IsNullOrEmpty(botConfigSection.GetValue<string>(nameof(BotConfiguration.Route))) ? botConfigSection.GetValue<string>(nameof(BotConfiguration.Route)) : "/bot",
            SecretToken = !IsNullOrEmpty(botConfigSection.GetValue<string>(nameof(BotConfiguration.SecretToken))) ? botConfigSection.GetValue<string>(nameof(BotConfiguration.SecretToken)) : guid.NewGuid().ToString(),
        };
        services.AddSingleton<IOptions<BotConfiguration>>(new  OptionsWrapper<BotConfiguration>(botConfig));
        services.AddSingleton<IOptions<TelegramBotClientOptions>>(y => y.GetRequiredService<IOptions<BotConfiguration>>());
        services.AddSingleton(y => y.GetRequiredService<IOptions<BotConfiguration>>().Value);
        services.AddSingleton(y => y.GetRequiredService<IOptions<TelegramBotClientOptions>>().Value);
        return services;
    }
}
