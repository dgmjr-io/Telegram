namespace Telegram.Bot.Identity;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using Directory = System.IO.Directory;

public class TelegramB2CDbContextDesignTimeFactory : IDesignTimeDbContextFactory<TelegramB2CDbContext<TelegramB2CUser>>
{
    public virtual TelegramB2CDbContext<TelegramB2CUser> CreateDbContext(string[] args)
    {
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile(
                $"appsettings.{env.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                optional: true
            )
            .AddEnvironmentVariables()
            .AddUserSecrets<TelegramB2CDbContextDesignTimeFactory>()
            .AddCommandLine(args);

        var configuration = configBuilder.Build();
        var dbName = configuration["DatabaseName"];

        return new TelegramB2CDbContext<TelegramB2CUser>(
            new DbContextOptionsBuilder<TelegramB2CDbContext<TelegramB2CUser>>()
                .UseSqlServer(
                    configuration.GetConnectionString(dbName),
                    x => x.UseAzureSqlDefaults()
                )
                .Options
        );
    }
}
