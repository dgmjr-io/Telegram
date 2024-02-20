namespace Telegram.Bot.Extensions.Configuration;
using Dgmjr.Configuration.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Extensions;

public class TelegramBotAutoConfigurator : IConfigureIHostApplicationBuilder, IConfigureIApplicationBuilder
{
    public ConfigurationOrder Order => ConfigurationOrder.AnyTime;

    public void Configure(WebApplicationBuilder builder)
    {
        builder.Services.AddTelegramBot(configuration: builder.Configuration);
    }

    public void Configure(IApplicationBuilder app)
    {
        app.Services.CreateScope().ServiceProvider.GetRequiredService<ITelegramBotClient>().SetWebhookAsync().Wait();
    }
}
