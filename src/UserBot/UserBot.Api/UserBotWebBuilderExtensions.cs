namespace Telegram.UserBot;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Telegram.UserBot.Config;

public static class UserBotWebBuilderExtensions
{
    public static WebApplicationBuilder AddUserBot(this WebApplicationBuilder builder)
    {
        builder.Services.AddUserBot(builder.Configuration);
        return builder;
    }

    public static IServiceCollection AddUserBot(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<UserBotConfig>(config.GetSection("Telegram:UserBot"));
        services.AddSingleton<IUserBot, UserBot>();
        return services;
    }
}
