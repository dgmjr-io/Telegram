namespace Telegram.OpenIdConnect.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Http.Extensions; // Add this using directive

using Telegram.OpenIdConnect.Errors;
using Telegram.OpenIdConnect.Models.Requests;
using Telegram.OpenIdConnect.Models.Responses;
using Telegram.OpenIdConnect.Extensions;
using CorrelationId.Abstractions;
using Application = Dgmjr.Mime.Application;

public partial class OidcController
{
    private JsonOptions JsonOptions =>
        httpContextAccessor.HttpContext.RequestServices
            .GetRequiredService<IOptionsMonitor<JsonOptions>>()
            .CurrentValue;
    private ICorrelationContextAccessor CorrelationContextAccessor =>
        httpContextAccessor.HttpContext.RequestServices.GetRequiredService<ICorrelationContextAccessor>();
    private string CorrelationId => CorrelationContextAccessor.CorrelationContext.CorrelationId;
    private Jso Jso => JsonOptions.JsonSerializerOptions;

    [HttpPost(ActiveDirectoryOpenIdConnectEndpoints.Authorize)]
    [Consumes(
        "application/x-www-form-urlencoded",
        Multipart.FormData.DisplayName,
        Application.Json.DisplayName
    )]
    [ProducesResponseType<AuthorizeResponse>(StatusCodes.Status200OK)]
    [ProducesUnauthorizedError, ProducesInternalServerError, ProducesBadRequestError]
    [ProducesOKResponse<AuthorizeResponse>]
    [ProducesErrorResponseType(typeof(ErrorResponse))]
    public async Task<IActionResult> Authorize(AuthorizationRequest authRequest)
    {
        authRequest.CorrelationId = CorrelationId;
        Logger.AuthorizationRequestReceived(Serialize(authRequest, Jso));

        var authResult = await AuthorizationService.AuthorizeRequestAsync(
            HttpContextAccessor,
            authRequest
        );
        if (!authResult.IsError)
        {
            return Ok(authResult.Result);
        }
        else
        {
            var error = authResult.Error.ErrorType.DisplayName;

            return error switch
            {
                ErrorType.InvalidRequest.DisplayName => BadRequest(authResult.Error),
                ErrorType.UnsupportedResponseType.DisplayName => BadRequest(authResult.Error),
                ErrorType.InvalidClient.DisplayName => BadRequest(authResult.Error),
                ErrorType.InvalidGrant.DisplayName => BadRequest(authResult.Error),
                ErrorType.UnauthorizedClient.DisplayName => Unauthorized(authResult.Error),
                ErrorType.AccessDenied.DisplayName => Unauthorized(authResult.Error),
                ErrorType.UnsupportedGrantType.DisplayName => BadRequest(authResult.Error),
                ErrorType.InvalidScope.DisplayName => BadRequest(authResult.Error),
                ErrorType.ServerError.DisplayName
                    => StatusCode(StatusCodes.Status500InternalServerError, authResult.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, authResult.Error),
            };
            // };
            // }
            // {
            //     return Unauthorized(authorizationResult.Error);
            // }
        }
    }

    [HttpGet(ActiveDirectoryOpenIdConnectEndpoints.Authorize)]
    [ProducesResponseType<AuthorizationRequest>(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(ErrorResponse))]
    public async Task<IActionResult> Authorize()
    {
        return View(Options);
    }
}
