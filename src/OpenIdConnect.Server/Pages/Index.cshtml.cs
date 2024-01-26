using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dgmjr.AspNetCore.Razor;

namespace Telegram.OpenIdConnect.Pages;

public class IndexModel(IOptionsMonitor<Options.TelegramOpenIdConnectServerOptions> options)
    : TelegramPageModelBase(options)
{
    public IActionResult OnGet()
    {
        ViewData["Title"] = $"Welcome! Please Log in";
        return Page();
    }
}
