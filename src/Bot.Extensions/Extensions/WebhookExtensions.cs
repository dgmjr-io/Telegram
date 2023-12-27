namespace Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Routing;

public static class WebhookExtensions
{
    public static IServiceCollection ConfigureBotWebhook(this IServiceCollection services)
    {
        services.AddHostedService<WebhookRegistrar>();
        return services;
    }

    public static T GetConfiguration<T>(this IServiceProvider serviceProvider)
        where T : class
    {
        var o = serviceProvider.GetService<IOptions<T>>();
        if (o is null)
            throw new ArgumentNullException(nameof(T));

        return o.Value;
    }

#if !NETSTANDARD
    public static ControllerActionEndpointConventionBuilder UseTelegramBotWebhook<T>(
        this IEndpointRouteBuilder endpoints,
        string route
    )
        where T : ControllerBase
    {
        var controllerName = typeof(T).Name.Replace("Controller", "", StringComparison.Ordinal);
        var actionName = typeof(T).GetMethods()[0].Name;

        return endpoints.MapControllerRoute(
            name: "bot_webhook",
            pattern: route,
            defaults: new { controller = controllerName, action = actionName }
        );
    }
#endif
}
