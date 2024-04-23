namespace Telegram.Bot.Components;

using System.Net.Http;

using Azure.Core;

using Dgmjr.BotFramework.Middleware;

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Extensions.Logging;

using Telegram.Bot.Components.Converters;
using Telegram.Bot.Components.Middleware;

using ILogger = ILogger;
using JsonConverterFactory = Microsoft.Bot.Builder.Dialogs.Declarative.Converters.JsonConverterFactory;
using TgConstants = Constants;

public class TelegramBotComponent : CustomBotComponent
{
    public const string ConfigurationKey = "Telegram.Bot.Components";

    public override void ConfigureServices(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        base.ConfigureServices(services, configuration);
        Logger.ServiceLoaded(nameof(TelegramBotComponent), nameof(ConfigureServices));
        // services.Insert(
        //     0,
        //     ServiceDescriptor.Singleton<IBotMiddleware, RegisterClassMiddleware<MsBotUserState>>()
        // );
        // services.Insert(
        //     0,
        //     ServiceDescriptor.Singleton<
        //         IBotMiddleware,
        //         RegisterClassMiddleware<MsBotConversationState>
        //     >()
        // );
        // Actions
        // Get all the classes where they have a ComponentRegistration attribute
        // IEnumerable<type> typesToInstatiate = typeof(TelegramBotComponent).Assembly
        //     .GetTypes()
        //     .Where(
        //         t =>
        //             t.CustomAttributes.Any(
        //                 attr =>
        //                     attr.AttributeType == typeof(CustomActionAttribute)
        //                     || attr.AttributeType == typeof(CustomExpressionAttribute)
        //             )
        //     );

        // foreach (var type in typesToInstatiate)
        // {
        //     if (
        //         type.GetCustomAttribute(typeof(CustomActionAttribute))
        //         is CustomActionAttribute attribute
        //     )
        //     {
        //         services.AddSingleton(_ => new DeclarativeType(attribute.DeclarativeType, type));
        //     }
        //     if (
        //         type.GetCustomAttribute(typeof(CustomExpressionAttribute))
        //         is CustomExpressionAttribute attribute2
        //     )
        //     {
        //         services.AddSingleton(_ => new DeclarativeType(attribute2.DeclarativeType, type));
        //     }
        // }

        services.AddLoggingHttpClient<TelegramBotClient>(
            nameof(TelegramBotClient),
            client =>
            {
                client.BaseAddress = new Uri(TgConstants.ApiBaseUri);
                client.DefaultRequestHeaders.Add(
                    "User-Agent",
                    $"TelegramBot (version={GetType().Assembly.GetName().Version})"
                );
            },
            y => y.GetRequiredService<ILogger<ITelegramBotClient>>()
        );

        services.TryAddSingleton<ITelegramBotClient>(
            y =>
                new TelegramBotClient(
                    configuration.Get<TelegramBotComponentSettings>().Token,
                    y.GetRequiredService<IHttpClientFactory>()
                        .CreateClient(nameof(TelegramBotClient))
                )
        );
        Logger.ServiceLoaded(nameof(IBotMiddleware), nameof(MessageForwardingMiddleware));
        services.AddSingleton<IBotMiddleware, MessageForwardingMiddleware>();
        Logger.ServiceLoaded(nameof(IBotMiddleware), nameof(UserDataMiddleware));
        services.AddSingleton<IBotMiddleware, UserDataMiddleware>();
        services.TryAddSingleton<
            JsonConverterFactory,
            JsonConverterFactory<StringExpressionConverter>
        >();
        services.TryAddSingleton<
            JsonConverterFactory,
            JsonConverterFactory<NumberExpressionConverter>
        >();
        services.TryAddTransient<UserDataAccessor>();
        // services.AddSingleton<JsonConverterFactory, JsonConverterFactory<ChatIdExpressionConverter>>();

        // services.AddSingleton<
        //     JsonConverterFactory,
        //     JsonConverterFactory<ObjectExpressionConverter<CalendarSkillEventModel>>
        // >();
        // services.AddSingleton<
        //     JsonConverterFactory,
        //     JsonConverterFactory<ObjectExpressionConverter<OrdinalV2>>
        // >();
        // services.AddSingleton<
        //     JsonConverterFactory,
        //     JsonConverterFactory<ObjectExpressionConverter<DateTime?>>
        // >();
        // services.AddSingleton<
        //     JsonConverterFactory,
        //     JsonConverterFactory<ArrayExpressionConverter<Attendee>>
        // >();
        // services.AddSingleton<
        //     JsonConverterFactory,
        //     JsonConverterFactory<ArrayExpressionConverter<CalendarSkillEventModel>>
        // >();
    }
}
