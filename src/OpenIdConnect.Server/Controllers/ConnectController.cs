namespace Telegram.OpenIdConnect.Controllers;

using Telegram.OpenIdConnect.Options;
using Microsoft.AspNetCore.Mvc;
using Telegram.OpenIdConnect.Models;
using Telegram.OpenIdConnect.Constants;

[Route("/connect")]
public class ConnectController(IOptions<TelegramOpenIdConnectServerOptions> options)
    : ViewControllerBase
{
    [HttpGet("login")]
    public IActionResult Login() => View(new LoginViewModel(options.Value, HttpContext));
}
