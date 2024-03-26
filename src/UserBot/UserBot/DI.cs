namespace Microsoft.Extensions.DependencyInjection;
using Telegram.UserBot.Config;
using Telegram.UserBot;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Telegram.UserBot.Store.Abstractions;
using Telegram.UserBot.Store.FileSystem;

public static class DI
{
    public static IHostApplicationBuilder AddUserBot(this IHostApplicationBuilder builder, Action<UserBotConfig> config)
    {
        builder.Services.AddSingleton<IUserBot, UserBot>();
        Action<UserBotConfig> config2 = cfg =>
        {
            builder.Configuration.GetSection("UserBot").Bind(cfg);
            config(cfg);
        };
        builder.Services.AddSingleton<IUserBotStore, FileUserBotStore>();
        builder.Services.ConfigureAll(config2);
        return builder;
    }
}
