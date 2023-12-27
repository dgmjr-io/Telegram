using Telegram.Bot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection;

public static class TelegramBotClientDIExtensions
{
    public static IServiceCollection AddTelegramBot(this IServiceCollection services)
    {
        // Register named HttpClient to get benefits of IHttpClientFactory
        // and consume it with ITelegramBotClient typed client.
        // More read:
        //  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests#typed-clients
        //  https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
        services
            .AddHttpClient("telegram_bot_client")
            .AddTypedClient<ITelegramBotClient>(
                (httpClient, sp) =>
                {
                    var botConfig = sp.GetService<IConfiguration>()
                        .GetSection(BotConfiguration.Key)
                        .Get<BotConfiguration>();
                    TelegramBotClientOptions options = new((string)botConfig.BotApiToken);
                    return new TelegramBotClient(options, httpClient);
                }
            );
        services.ConfigureBotWebhook();
        return services;
    }
}
