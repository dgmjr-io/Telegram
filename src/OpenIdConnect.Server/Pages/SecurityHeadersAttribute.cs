// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telegram.OpenIdConnect.Extensions;

namespace IdentityServerHost.Pages;

public class SecurityHeadersAttribute : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        var services = context.HttpContext.RequestServices;
        var options = services
            .GetRequiredService<
                IOptions<Telegram.OpenIdConnect.Options.TelegramOpenIdConnectServerOptions>
            >()
            .Value;
        var result = context.Result;
        if (result is PageResult)
        {
            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Content-Type-Options
            if (!context.HttpContext.Response.Headers.ContainsKey("X-Content-Type-Options"))
            {
                context.HttpContext.Response.Headers.TryAdd("X-Content-Type-Options", "nosniff");
            }

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options
            if (!context.HttpContext.Response.Headers.ContainsKey("X-Frame-Options"))
            {
                context.HttpContext.Response.Headers.TryAdd("X-Frame-Options", "SAMEORIGIN");
            }

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy
            var csp =
                $"default-src 'self' {context.HttpContext.Request.GetClient().CdnUriBase.Host} {context.HttpContext.Request.GetClient().LogoUri.Host}; object-src 'none' {context.HttpContext.Request.GetClient().CdnUriBase.Host}; frame-ancestors 'none' {context.HttpContext.Request.GetClient().CdnUriBase.Host}; sandbox allow-forms allow-same-origin allow-scripts; base-uri 'self' {context.HttpContext.Request.GetClient().CdnUriBase.Host};";
            // also consider adding upgrade-insecure-requests once you have HTTPS in place for production
            //csp += "upgrade-insecure-requests;";
            // also an example if you need client images to be displayed from twitter
            // csp += "img-src 'self' https://pbs.twimg.com;";

            // once for standards compliant browsers
            if (!context.HttpContext.Response.Headers.ContainsKey("Content-Security-Policy"))
            {
                context.HttpContext.Response.Headers.TryAdd("Content-Security-Policy", csp);
            }
            // and once again for IE
            if (!context.HttpContext.Response.Headers.ContainsKey("X-Content-Security-Policy"))
            {
                context.HttpContext.Response.Headers.TryAdd("X-Content-Security-Policy", csp);
            }

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Referrer-Policy
            var referrer_policy = "no-referrer";
            if (!context.HttpContext.Response.Headers.ContainsKey("Referrer-Policy"))
            {
                context.HttpContext.Response.Headers.TryAdd("Referrer-Policy", referrer_policy);
            }
        }
    }
}
