using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Telegram.OpenIdConnect.Pages;

namespace Telegram.OpenIdConnect.Server.Pages.Connect;

public class LoginModel(IOptionsMonitor<Options.TelegramOpenIdConnectServerOptions> options) : TelegramPageModelBase(options)
{
    private IQueryCollection Query => Request.Query;

    public string AuthorizeUri => Request.Query["ReturnUrl"];


    public void OnGet() { }
}
