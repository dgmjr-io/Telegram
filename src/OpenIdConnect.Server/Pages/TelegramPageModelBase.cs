namespace Telegram.OpenIdConnect.Pages;

using Microsoft.AspNetCore.Mvc.RazorPages;

using Telegram.OpenIdConnect.Options;

public class TelegramPageModelBase(IOptionsMonitor<TelegramOpenIdConnectServerOptions> options)
    : Dgmjr.AspNetCore.Razor.PageModel<TelegramOpenIdConnectServerOptions>(options.CurrentValue)
{
    public TelegramOpenIdConnectServerOptions Options => options.CurrentValue;
}
