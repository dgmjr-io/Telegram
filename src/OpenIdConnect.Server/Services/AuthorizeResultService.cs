namespace Telegram.OpenIdConnect.Services;

using Telegram.OpenIdConnect.Extensions;
using Telegram.OpenIdConnect.Models.Responses;
using Telegram.OpenIdConnect.Services.CodeService;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Telegram.OpenIdConnect.Options;
using Telegram.OpenIdConnect.Enums;
using Telegram.AspNetCore.Authentication;
using Telegram.OpenIdConnect.Models.Requests;
using OneOf;
using ErrorType = Errors.ErrorType;

public class AuthorizationService(
    ICodeStoreService codeStoreService,
    IOptionsMonitor<TelegramOpenIdConnectServerOptions> options,
    ITelegramJwtFactory teleJwtFactory,
    ILogger<AuthorizationService> logger
) : IAuthorizationService, ILog
{
    public ILogger Logger => logger;
    private ClientStore ClientStore => Options.Clients;
    private ICodeStoreService CodeStoreService => codeStoreService;
    private TelegramOpenIdConnectServerOptions Options => options.CurrentValue;
    private JsonWebKeySet Jwks => Options.JsonWebKeySet;

    public async Task<AuthorizeResponseOrError> AuthorizeRequestAsync(
        IHttpContextAccessor httpContextAccessor,
        AuthorizationRequest request
    )
    {
        var response = new AuthorizeResponse();

        if (httpContextAccessor == null)
        {
            Logger.AuthorizationRequestRejected(
                request.CorrelationId,
                request.ClientId,
                "HttpContextAccessor is null."
            );
            return request.CreateErrorResponse(ErrorType.ServerError.Instance);
        }

        var accessCode = VerifyClientById(request);
        if (!accessCode.IsSuccess)
        {
            Logger.AuthorizationRequestValidated(request.CorrelationId, request.ClientId);
            return request.CreateErrorResponse(accessCode.Error);
        }

        if (IsNullOrEmpty(request.ResponseType) || request.ResponseType != "code")
        {
            Logger.AuthorizationRequestRejected(
                request.CorrelationId,
                request.ClientId,
                ErrorType.UnsupportedResponseType.Description
            );
            return request.CreateErrorResponse(ErrorType.UnsupportedResponseType.Instance);
        }

        if (
            !request.RedirectUri.IsRedirectUriStartWithHttps()
            && !httpContextAccessor.HttpContext.Request.IsHttps
        )
        {
            Logger.AuthorizationRequestRejected(
                request.CorrelationId,
                request.ClientId,
                ErrorType.InvalidRedirectUri.Description
            );
            return request.CreateErrorResponse(ErrorType.InvalidRedirectUri.Instance);
        }

        // check the return url is match the one that in the client store
        if (
            !accessCode.Client.RedirectUriStrings.Contains(
                request.RedirectUri,
                StringComparer.OrdinalIgnoreCase
            )
        )
        {
            Logger.AuthorizationRequestRejected(
                request.CorrelationId,
                request.ClientId,
                ErrorType.InvalidRedirectUri.Description
            );
            return request.CreateErrorResponse(ErrorType.InvalidRedirectUri.Instance);
        }

        // check the Scope in the client store with the
        // one that is coming from the request MUST be matched at least one

        var scopes = request.Scope?.Split(' ') ?? [];

        var requestedScopes =
            from m in accessCode.Client.AllowedScopes
            where scopes.Contains(m)
            select m;

        if (!requestedScopes.Any())
        {
            Logger.AuthorizationRequestRejected(
                request.CorrelationId,
                request.ClientId,
                ErrorType.InvalidScope.Description
            );
            return request.CreateErrorResponse(ErrorType.InvalidScope.Instance);
        }

        var nonce = httpContextAccessor.HttpContext.Request.Query["nonce"].ToString();

        // Verify that a Scope parameter is present and contains the openid Scope value.
        // (If no openid Scope value is present,
        // the request may still be a valid OAuth 2.0 request, but is not an OpenID Connect request.)

        var code = await CodeStoreService.GenerateAuthorizationCodeAsync(
            request.ClientId,
            requestedScopes.ToList()
        );
        if (code == null)
        {
            Logger.AuthorizationRequestRejected(
                request.CorrelationId,
                request.ClientId,
                ErrorType.TemporarilyUnavailable.Description
            );
            return request.CreateErrorResponse(ErrorType.TemporarilyUnavailable.Instance);
        }

        response.RedirectUri = new UriBuilder(request.RedirectUri)
            .AddQuery("code", code)
            .AddQuery("state", request.State)
            .ToString();
        response.Code = code;
        response.State = request.State;
        response.RequestedScopes = requestedScopes.ToList();
        response.Nonce = nonce;

        return response;
    }

    private CheckClientResult VerifyClientById(AuthorizationRequest request)
    {
        var result = new CheckClientResult() { IsSuccess = false };
        var clientId = request.ClientId;
        var clientSecret = request.ClientSecret;
        var checkWithSecret = !IsNullOrEmpty(clientSecret);

        if (!IsNullOrWhiteSpace(clientId))
        {
            var client = ClientStore.Find(x => x.ClientId.Equals(clientId, OrdinalIgnoreCase));

            if (client != null)
            {
                if (checkWithSecret && !IsNullOrEmpty(clientSecret))
                {
                    var hasSameSecretId = client.ClientSecrets.Any(secret => secret.Value.Equals(
                        clientSecret,
                        InvariantCulture
                    ));
                    if (!hasSameSecretId)
                    {
                        result.Error = ErrorType.InvalidClientSecret.Instance;
                        return result;
                    }
                }

                // check if client is enabled or not

                // if (client.IsActive)
                // {
                //     result.IsSuccess = true;
                //     result.ClientId = client.ClientId;
                //     result.Client = client;

                //     return result;
                // }
                // else
                // {
                //     result.Error = ErrorType.UnauthorizedClient.Instance;
                //     return result;
                // }
            }
        }

        result.Error = ErrorType.AccessDenied.Instance;
        return result;
    }

    private async Task<CheckClientResult> VerifyClientByCode(TokenRequest request)
    {
        var result = new CheckClientResult() { IsSuccess = false };

        if (!IsNullOrWhiteSpace(request.Code))
        {
            var client = await CodeStoreService.GetClientDataByCodeAsync(request.Code);

            if (client != null)
            {
                // check if client is enabled or not

                if (client.IsActive)
                {
                    result.IsSuccess = true;
                    result.ClientId = request.ClientId;

                    return result;
                }
                else
                {
                    result.Error = ErrorType.UnauthorizedClient.Instance;
                    return result;
                }
            }
        }

        result.Error = ErrorType.AccessDenied.Instance;
        return result;
    }

    public async Task<TokenResponseOrError> GenerateTokenAsync(TokenRequest request)
    {
        var checkClientResult = await VerifyClientByCode(request);
        if (!checkClientResult.IsSuccess)
        {
            return request.CreateErrorResponse(checkClientResult.Error);
        }

        // check code from the cache
        var clientCodeChecker = await CodeStoreService.GetClientDataByCodeAsync(request.Code);
        if (clientCodeChecker == null)
        {
            return request.CreateErrorResponse(checkClientResult.Error);
        }

        // check if the current client who is one made this authentication request
        if (request.ClientId != clientCodeChecker.ClientId)
        {
            return request.CreateErrorResponse(checkClientResult.Error);
        }

        // also I have to check the redirect uri
        if (
            !clientCodeChecker.RedirectUris.Contains(
                request.RedirectUri,
                StringComparer.OrdinalIgnoreCase
            )
        )
        {
            return request.CreateErrorResponse(ErrorType.InvalidRedirectUri.Instance);
        }

        // Now here I will Issue the Id_token

        return teleJwtFactory.GenerateToken(request);
    }
}
