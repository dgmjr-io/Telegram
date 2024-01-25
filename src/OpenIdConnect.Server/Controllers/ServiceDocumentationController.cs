using Microsoft.AspNetCore.Mvc;

namespace Telegram.OpenIdConnect.Controllers;

[ApiController]
[Route("/service-documentation")]
public class ServiceDocumentationController
{
    [HttpGet]
    public IActionResult Get()
    {
        return new ContentResult
        {
            Content = "ServiceDocumentation",
            ContentType = "text/plain",
            StatusCode = 200
        };
    }
}
