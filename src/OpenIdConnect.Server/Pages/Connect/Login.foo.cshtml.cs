using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Telegram.OpenIdConnect.Options;
using Telegram.OpenIdConnect.Pages;

namespace Telegram.OpenIdConnect.Server.Pages.Connect;

public class LoginModel(IOptionsMonitor<TelegramOpenIdConnectServerOptions> options)
    : TelegramPageModelBase(options)
{
    public void OnGet() { }
}
