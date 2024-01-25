namespace Microsoft.Extensions.DependencyInjection;

using System.Linq;
using System.Net.Http;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Telegram.Bot;
using Telegram.Bot.Types;

#if NET6_0_OR_GREATER
using Microsoft.AspNetCore.Builder;
#endif

public static class TelegramBotDIExtensions
{
#if NET6_0_OR_GREATER
    /// <summary>Adds OpenAPI description for the <see cref="BotApiToken" /></summary>
    public static WebApplicationBuilder DescribeBotApiToken(this WebApplicationBuilder builder)
    {
        builder.Services.DescribeBotApiToken(); //.ConfigureSwaggerGen(ConfigureSwaggerGen);
        return builder;
    }

    public static WebApplicationBuilder AddTelegramBot(
        this WebApplicationBuilder builder,
        string botName = "Bot",
        Action<TelegramBotClientOptions>? optionsConfigurator = default
    )
    {
        builder.Services.AddTelegramBot(botName, optionsConfigurator);
        return builder;
    }

    public static WebApplicationBuilder AddTelegramBot(
        this WebApplicationBuilder builder,
        Action<TelegramBotClientOptions>? optionsConfigurator = default
    )
    {
        builder.Services.AddTelegramBot(optionsConfigurator);
        return builder;
    }

    public static WebApplicationBuilder AddTelegramBot(
        this WebApplicationBuilder builder,
        string botName,
        string token
    )
    {
        builder.Services.AddTelegramBot(botName, token);
        return builder;
    }

    public static WebApplicationBuilder AddTelegramBot(
        this WebApplicationBuilder builder,
        string botName = "Bot",
        IConfiguration? configuration = default
    ) => builder.AddTelegramBot(botName, cfg => configuration?.Bind(cfg));
#endif

    /// <summary>Adds OpenAPI description for the <see cref="BotApiToken" /></summary>
    public static IServiceCollection DescribeBotApiToken(this IServiceCollection services)
    {
        services.Describe<BotApiToken>();
        return services;
    }

    public static IServiceCollection AddTelegramBot(
        this IServiceCollection services,
        string botName,
        Action<BotConfiguration> optionsConfigurator
    ) =>
        services
            .DescribeBotApiToken()
            .Configure(botName, optionsConfigurator ?? (_ => { }))
            .Configure<TelegramBotClientOptions>(
                botName,
                options => optionsConfigurator?.Invoke(new(options))
            )
            .AddHttpClient<TelegramBotClient>()
            .AddTypedClient<ITelegramBotClient>()
            .Services.AddKeyedScoped<ITelegramBotClient, TelegramBotClient>(
                botName,
                (y, key) =>
                    new TelegramBotClient(
                        y.GetRequiredService<IOptionsMonitor<TelegramBotClientOptions>>()
                            .Get(Convert.ToString(key)),
                        y.GetRequiredService<HttpClient>()
                    )
            );

    public static IServiceCollection AddTelegramBot(
        this IServiceCollection services,
        string botName = "Bot",
        IConfiguration? configuration = default
    ) => services.AddTelegramBot(botName, cfg => configuration?.Bind(cfg));

    public static IServiceCollection AddTelegramBot(
        this IServiceCollection services,
        Action<TelegramBotClientOptions> optionsConfigurator
    ) => services.AddTelegramBot("Bot", optionsConfigurator);

    public static IServiceCollection AddTelegramBot(
        this IServiceCollection services,
        string botName,
        string token
    ) =>
        services
            .ConfigureOptions(new TelegramBotClientOptions(token))
            .ConfigureOptions(new BotConfiguration(token))
            .AddTelegramBot(botName);

    public static ITelegramBotClient GetBot(
        this IServiceProvider services,
        string botName = "Bot"
    ) => services.GetRequiredKeyedService<ITelegramBotClient>(botName);
}
