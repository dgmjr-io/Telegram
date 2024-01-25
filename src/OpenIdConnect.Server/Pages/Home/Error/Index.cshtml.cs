namespace Telegram.OpenIdConnect.Pages.Error;

using System.Threading.Tasks;
using Duende.IdentityServer.Services;

using IdentityServerHost.Pages;
using IdentityServerHost.Pages.Error;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;

using Telegram.OpenIdConnect.Options;
using Telegram.OpenIdConnect.Pages;

[AllowAnonymous]
[SecurityHeaders]
public class Index(
    IOptionsMonitor<TelegramOpenIdConnectServerOptions> options,
    IIdentityServerInteractionService interaction,
    IWebHostEnvironment environment
) : TelegramPageModelBase(options)
{
    public ViewModel View { get; set; }

    public async Task OnGet(string errorId)
    {
        View = new ViewModel();

        // retrieve error details from identityserver
        var message = await interaction.GetErrorContextAsync(errorId);
        if (message != null)
        {
            View.Error = message;

            if (!environment.IsDevelopment())
            {
                // only show in development
                message.ErrorDescription = null;
            }
        }
    }
}
