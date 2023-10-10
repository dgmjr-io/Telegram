namespace Microsoft.Extensions.DependencyInjection;

using System.Linq;
using System.Net.Http;

using Microsoft.Extensions.DependencyInjection.Extensions;

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
        builder.Services.Describe<BotApiToken>(); //.ConfigureSwaggerGen(ConfigureSwaggerGen);
        return builder;
    }
#endif

    /// <summary>Adds OpenAPI description for the <see cref="BotApiToken" /></summary>
    public static IServiceCollection DescribeBotApiToken(this IServiceCollection services)
    {
        services.Describe<BotApiToken>();
        return services;
    }

    public static IServiceCollection AddBot(
        this IServiceCollection services,
        BotApiToken token,
        Uri? baseUri = default,
        bool useTestEnvironment = false,
        string botName = "Bot"
    )
    {
        services.TryAddEnumerable(
            new ServiceDescriptor(
                typeof(KeyValuePair<string, ITelegramBotClient>),
                y =>
                    new KeyValuePair<string, ITelegramBotClient>(
                        botName,
                        new TelegramBotClient(
                            new TelegramBotClientOptions(
                                token.ToString(),
                                baseUri?.ToString(),
                                useTestEnvironment
                            ),
                            y.GetService<HttpClient>()
                        )
                    ),
                ServiceLifetime.Singleton
            )
        );
        services.TryAddSingleton(y => y.GetBot(botName));
        return services;
    }

    public static ITelegramBotClient GetBot(this IServiceProvider services, string botName = "Bot")
    {
        return services
            .GetRequiredService<IEnumerable<KeyValuePair<string, ITelegramBotClient>>>()
            .SingleOrDefault(x => x.Key == botName)
            .Value;
    }
}
