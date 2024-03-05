namespace Microsoft.Extensions.DependencyInjection;
using Telegram.UserBot.Config;
using Telegram.UserBot;

public static class DI
{
    public static IServiceCollection AddUserBot(this IServiceCollection services, Action<UserBotConfig> config)
    {
        services.AddSingleton<IUserBot, UserBot>();
        services.ConfigureAll<UserBotConfig>(config);
        return services;
    }
}
