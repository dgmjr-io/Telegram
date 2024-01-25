namespace Telegram.OpenIdConnect.Controllers;

using CorrelationId.Abstractions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

using Telegram.OpenIdConnect.Errors;
using Telegram.OpenIdConnect.Models.Requests;
using Telegram.OpenIdConnect.Models.Responses;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static Telegram.OpenIdConnect.Constants.MimeTypes;

public partial class OidcController
{
    [HttpPost(ActiveDirectoryOpenIdConnectEndpoints.Token)]
    [ProducesResponseType<TokenResponse>(Status200OK, TokenResponseMimeType)]
    public async Task<IActionResult> Token(TokenRequest tokenRequest)
    {
        tokenRequest.CorrelationId = httpContextAccessor.HttpContext.RequestServices
            .GetRequiredService<ICorrelationContextAccessor>()
            .CorrelationContext.CorrelationId;
        var result = await AuthorizationService.GenerateTokenAsync(tokenRequest);

        if (result.IsError)
        {
            var error = result.Error.ErrorType.DisplayName;
            return error switch
            {
                ErrorType.InvalidRequest.DisplayName => BadRequest(result.Error),
                ErrorType.InvalidClient.DisplayName => BadRequest(result.Error),
                ErrorType.InvalidGrant.DisplayName => BadRequest(result.Error),
                ErrorType.UnauthorizedClient.DisplayName => Unauthorized(result.Error),
                ErrorType.UnsupportedGrantType.DisplayName => BadRequest(result.Error),
                ErrorType.InvalidScope.DisplayName => BadRequest(result.Error),
                ErrorType.ServerError.DisplayName
                    => StatusCode(Status500InternalServerError, result.Error),
                _ => StatusCode(Status500InternalServerError, result.Error),
            };
        }

        return Ok(result);
    }
}
