using Telegram.Bot.Identity;

namespace Microsoft.Extensions.DependencyInjection;

public static class DiExtensions
{
    public static IServiceCollection AddTelegramBotIdentity(
        this IServiceCollection services,
        string configSectionName = "Bot",
        Action<BotIdentityOptions>? configure = default
    )
    {
        services
            .AddAuthentication(BotIdentityDefaults.AuthenticationScheme)
            .AddScheme<BotIdentityOptions, BotIdentityHandler>(
                BotIdentityDefaults.AuthenticationScheme,
                configure ?? (_ => { })
            );
        services.AddSingleton<
            IConfigureOptions<BotIdentityOptions>,
            TelegramBotIdentityConfigurator
        >(
            sp =>
                new TelegramBotIdentityConfigurator(
                    sp.GetRequiredService<IConfiguration>(),
                    configSectionName
                )
        );
        return services;
    }

    public class TelegramBotIdentityConfigurator(
        IConfiguration configuration,
        string configSectionName
    ) : IConfigureOptions<BotIdentityOptions>
    {
        public void Configure(BotIdentityOptions options)
        {
            configuration.GetSection(configSectionName).Bind(options);
        }
    }
}
