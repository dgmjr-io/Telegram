namespace Telegram.Bot.Components;

using Microsoft.Extensions.DependencyInjection.Extensions;
using JsonConverterFactory = Microsoft.Bot.Builder.Dialogs.Declarative.Converters.JsonConverterFactory;
using TgConstants = Constants;
using Telegram.Bot.Components.Middleware;
using Telegram.Bot.Components.Converters;

public class TelegramBotComponent : BotComponent
{
    public const string ConfigurationKey = "Telegram.Bot.Components";

    public override void ConfigureServices(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        Console.WriteLine("TelegramBotComponent.ConfigureServices");
        // Actions
        // Get all the classes where they have a ComponentRegistration attribute
        IEnumerable<type> typesToInstatiate = typeof(TelegramBotComponent).Assembly
            .GetTypes()
            .Where(
                t =>
                    t.CustomAttributes.Any(
                        attr =>
                            attr.AttributeType == typeof(CustomActionAttribute)
                            || attr.AttributeType == typeof(CustomExpressionAttribute)
                    )
            );

        foreach (var type in typesToInstatiate)
        {
            if (
                type.GetCustomAttribute(typeof(CustomActionAttribute))
                is CustomActionAttribute attribute
            )
            {
                services.AddSingleton(_ => new DeclarativeType(attribute.DeclarativeType, type));
            }
            if (
                type.GetCustomAttribute(typeof(CustomExpressionAttribute))
                is CustomExpressionAttribute attribute2
            )
            {
                services.AddSingleton(_ => new DeclarativeType(attribute2.DeclarativeType, type));
            }
        }

        services.TryAddSingleton<ITelegramBotClient>(
            _ => new TelegramBotClient(configuration[TgConstants.Token])
        );
        services.AddTransient<IMiddleware, MessageForwardingMiddleware>();
        services.AddTransient<IMiddleware, UserDataMiddleware>();
        services.AddSingleton<
            JsonConverterFactory,
            JsonConverterFactory<StringExpressionConverter>
        >();
        services.AddSingleton<
            JsonConverterFactory,
            JsonConverterFactory<NumberExpressionConverter>
        >();
        services.AddSingleton<
            JsonConverterFactory,
            JsonConverterFactory<FileExpressionConverter>
        >();
        services.AddTransient<UserDataAccessor>();
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
