namespace Telegram.OpenIdConnect.Pages;

using Microsoft.AspNetCore.Mvc.RazorPages;

using Telegram.OpenIdConnect.Options;

public class TelegramPageModelBase(IOptionsMonitor<TelegramOpenIdConnectServerOptions> options)
    : PageModel
{
    public TelegramOpenIdConnectServerOptions Options => options.CurrentValue;
}
