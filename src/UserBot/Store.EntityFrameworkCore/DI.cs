namespace Microsoft.Extensions.DependencyInjection;

using Telegram.UserBot.Store.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Sqlite;

using Telegram.UserBot.Store;
using Telegram.UserBot.Store.Abstractions;

public static class DI
{
    public static IServiceCollection AddUserBotEfCoreStoreSqlServer(
        this IServiceCollection services,
        string connectionString
    )
    {
        services.AddDbContext<UserBotDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IUserBotStore, DbUserBotStore>();
        return services;
    }
}
