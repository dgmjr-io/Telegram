namespace Microsoft.Extensions.DependencyInjection;

public static class DI
{
    public static IServiceCollection AddUserBot(this IServiceCollection services)
    {
        services.AddUserBotEfCoreStore("Data Source=userbot.db");
        return services;
    }
}
