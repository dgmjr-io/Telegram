namespace Telegram.OpenIdConnect.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using Telegram.OpenIdConnect.Extensions;
using Telegram.OpenIdConnect.Options;

using static Telegram.OpenIdConnect.Constants.MimeTypes;

public class JwksController(IOptionsMonitor<TelegramOpenIdConnectServerOptions> options)
    : ApiControllerBase
{
    private TelegramOpenIdConnectServerOptions Options => options.CurrentValue;

    [HttpGet("/.well-known/jwks")]
    [Produces(JsonWebKeySetMimeType)]
    [ProducesOKResponse<JsonWebKeySet>]
    public IActionResult Get() => Content(Options.JsonWebKeySet.ToJson(), JsonWebKeySetMimeType);
}
