namespace Telegram.OpenIdConnect.Controllers;

using Microsoft.AspNetCore.Mvc;

[Route("/")]
public class IndexController : ViewControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return RedirectToAction(nameof(OidcController.Login), "Oidc");
    }
}
