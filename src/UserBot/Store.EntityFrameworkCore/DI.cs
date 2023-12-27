namespace Microsoft.Extensions.DependencyInjection;

using Telegram.UserBot.Store.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public static class DI
{
    public static IServiceCollection AddUserBotEfCoreStore(
        this IServiceCollection services,
        string connectionString
    )
    {
        services.AddDbContext<UserBotDbContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<IUserBotStore, DbUserBotStore>();
        return services;
    }
}
