namespace Telegram.OpenIdConnect.Pages;

using Duende.IdentityServer.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

using Telegram.OpenIdConnect.Constants;
using Telegram.OpenIdConnect.Extensions;
using Telegram.OpenIdConnect.Models;
using Telegram.OpenIdConnect.Options;

public class TelegramPageModelBase(
    IOptionsMonitor<TelegramOpenIdConnectServerOptions>? options = null
) : Dgmjr.AspNetCore.Razor.PageModel<TelegramOpenIdConnectServerOptions?>(options?.CurrentValue)
{
    public TelegramOpenIdConnectServerOptions Options => ViewModel!;

    public string? ReturnUrl => ((string?)Request.Query["ReturnUrl"]) ?? "/";

    // private Dictionary<string, StringValues>? ReturnUrlQuery =>
    //     QueryHelpers.ParseQuery(new Uri(ReturnUrl).Query);

    // public string? ClientId =>
    //     Request.Query.ContainsKey("client_id")
    //         ? Request.Query["client_id"]
    //         : ReturnUrlQuery?.ContainsKey("client_id") == true
    //             ? ReturnUrlQuery["client_id"]
    //             : default;

    public TelegramOidcClient? Client =>
        Options.Clients[HttpContext.Request.Cookies[SessionKeys.ClientId]];
}
