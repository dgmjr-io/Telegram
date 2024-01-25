namespace Telegram.OpenIdConnect.Extensions;

using Microsoft.AspNetCore.Mvc;

public static class LinkGeneratorExtensions
{
    public static string? GetActionUri(
        this LinkGenerator linkGenerator,
        HttpContext httpContext,
        string actionName,
        string controllerName,
        object? routeValues = default,
        string? scheme = "https",
        HostString host = default,
        PathString pathBase = default,
        FragmentString fragment = default,
        LinkOptions? options = null
    ) =>
        linkGenerator.GetUriByAction(
            httpContext,
            actionName,
            controllerName.Replace(nameof(Controller), ""),
            routeValues,
            scheme,
            host,
            pathBase,
            fragment,
            options
        );

    public static string? GetActionUri<TController>(
        this LinkGenerator linkGenerator,
        HttpContext httpContext,
        string actionName,
        object? routeValues = default,
        string? scheme = "https",
        HostString host = default,
        PathString pathBase = default,
        FragmentString fragment = default,
        LinkOptions? options = null
    )
        where TController : ControllerBase =>
        linkGenerator.GetActionUri(
            httpContext,
            actionName,
            typeof(TController).Name.Replace(nameof(Controller), ""),
            routeValues,
            scheme,
            host,
            pathBase,
            fragment,
            options
        );

    // public static string? GetActionUri(this LinkGenerator linkGenerator, Expression<Action> action,
    //     string? scheme,
    //     HostString host,
    //     PathString pathBase = default,
    //     FragmentString fragment = default,
    //     LinkOptions? options = null)
    // => linkGenerator.GetActionUri(action.AsMethod().Name, action.AsMethod().DeclaringType.Name, action.Parameters.ToDictionary(p => p.Name, p => p.))
}
