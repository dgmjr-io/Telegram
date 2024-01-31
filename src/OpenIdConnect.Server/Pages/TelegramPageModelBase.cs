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

public partial class TelegramPageModelBase(
    IOptionsMonitor<TelegramOpenIdConnectServerOptions>? options = null
) : Dgmjr.AspNetCore.Razor.PageModel<TelegramOpenIdConnectServerOptions?>(options?.CurrentValue)
{
    private const string ClientIdKey = "client_id";
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

    public string? ClientId => Request.GetClientId();

    public string? BotUsername => IsNullOrEmpty(Client?.BotUsername) ? "DGMJRBot" : Client.BotUsername;

    public TelegramOidcClient? Client => Request.GetClient();
}
