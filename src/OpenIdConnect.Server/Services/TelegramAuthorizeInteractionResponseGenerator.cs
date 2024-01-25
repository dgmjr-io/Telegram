using System.Threading.Tasks;

using Duende.IdentityServer.Models;
using Duende.IdentityServer.ResponseHandling;
using Duende.IdentityServer.Validation;

using Telegram.OpenIdConnect.Constants;
using Telegram.OpenIdConnect.Extensions;
using Telegram.OpenIdConnect.Controllers;
using Duende.IdentityServer;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Services;

namespace Telegram.OpenIdConnect.Services;

public class TelegramAuthorizeInteractionResponseGenerator(
    IdentityServerOptions options,
    IClock clock,
    ILogger<AuthorizeInteractionResponseGenerator> logger,
    IConsentService consentService,
    IProfileService profileService,
    IHttpContextAccessor httpContextAccessor,
    LinkGenerator linkGenerator
)
    : AuthorizeInteractionResponseGenerator(options, clock, logger, consentService, profileService),
        ILog
{
    private HttpContext HttpContext => httpContextAccessor.HttpContext!;
    private HttpRequest Request => HttpContext.Request;
    public ILogger Logger => logger;

    public override Task<InteractionResponse> ProcessInteractionAsync(
        ValidatedAuthorizeRequest request,
        ConsentResponse? consent = null
    )
    {
        Logger.ReceivedValidatedAuthorizedRequest(request);

        if (ShouldRedirectToTelegramLoginPage())
        {
            Logger.RequestRequiresInteraction();
            request.RedirectUri = TelegramLoginUri; //ShouldRedirectToTelegramLoginPage();
            return Task.FromResult(
                new InteractionResponse { IsLogin = true, RedirectUrl = TelegramLoginUri }
            );
        }
        else
        {
            return Task.FromResult(new InteractionResponse());
        }
    }

    private string TelegramLoginUri =>
        linkGenerator.GetActionUri<OidcController>(HttpContext, nameof(OidcController.Login))!;

    private bool ShouldRedirectToTelegramLoginPage() =>
        IsNullOrEmpty(Request.Query[DataCheckKeys.AuthHash]);
}
