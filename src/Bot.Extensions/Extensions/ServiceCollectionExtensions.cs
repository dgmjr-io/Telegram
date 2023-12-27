namespace Microsoft.Extensions.DependencyInjection;

using Telegram.Bot;
using Telegram.Bot.Types;

public static class ServiceProviderExtensions
{
    public static ITelegramBotClient GetTelegramBotClient(this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredService<ITelegramBotClient>();
    }
}
