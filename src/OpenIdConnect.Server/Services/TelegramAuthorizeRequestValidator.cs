namespace Telegram.OpenIdConnect.Services;

using System.Collections.Specialized;
using System.Security.Claims;
using System.Threading.Tasks;

using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Validation;

using IdentityModel;

using Telegram.OpenIdConnect.Constants;
using Telegram.OpenIdConnect.Extensions;
using Telegram.OpenIdConnect.Options;

public partial class TelegramAuthorizeRequestValidator(
    IdentityServerOptions options,
    IIssuerNameService issuerNameService,
    IClientStore clients,
    // ICustomAuthorizeRequestValidator customValidator,
    IRedirectUriValidator uriValidator,
    IResourceValidator resourceValidator,
    IUserSession userSession,
    ILogger<TelegramAuthorizeRequestValidator> logger,
    IOptionsMonitor<TelegramOpenIdConnectServerOptions> tgOidcOptions
)
    : /*AuthorizeRequestValidator, */
    ICustomAuthorizeRequestValidator,
        IAuthorizeRequestValidator,
        ILog
{
    public ILogger Logger => logger;

    private TelegramOpenIdConnectServerOptions TgOidcOptions => tgOidcOptions.CurrentValue;

    public Task ValidateAsync(CustomAuthorizeRequestValidationContext context)
    {
        Logger.ValidAuthorizeRequest();
        context.Result.IsError = false;
        return Task.CompletedTask;
    }

    public async Task<AuthorizeRequestValidationResult> ValidateAsync(
        NameValueCollection parameters,
        ClaimsPrincipal? subject = default,
        AuthorizeRequestType authorizeRequestType = AuthorizeRequestType.Authorize
    )
    {
        Logger.LogDebug("Start authorize request protocol validation");
        var validatedAuthorizeRequest = new ValidatedAuthorizeRequest { Options = options };
        var validatedAuthorizeRequest2 = validatedAuthorizeRequest;
        validatedAuthorizeRequest2.IssuerName = await issuerNameService.GetCurrentAsync();
        validatedAuthorizeRequest.Subject = subject ?? Principal.Anonymous;
        validatedAuthorizeRequest.Raw = parameters ?? throw new ArgumentNullException("parameters");
        validatedAuthorizeRequest.AuthorizeRequestType = authorizeRequestType;
        var request = validatedAuthorizeRequest;
        var authorizeRequestValidationResult = await LoadClientAsync(request);
        if (authorizeRequestValidationResult.IsError)
        {
            return authorizeRequestValidationResult;
        }
        // AuthorizeRequestValidationResult authorizeRequestValidationResult2 =
        //     await requestObjectValidator.LoadRequestObjectAsync(request);
        // if (authorizeRequestValidationResult2.IsError)
        // {
        //     return authorizeRequestValidationResult2;
        // }
        // AuthorizeRequestValidationResult authorizeRequestValidationResult3 =
        //     await requestObjectValidator.ValidateRequestObjectAsync(request);
        // if (authorizeRequestValidationResult3.IsError)
        // {
        //     return authorizeRequestValidationResult3;
        // }
        var validateTelegramTokensResult = ValidateTelegramTokens(request);
        if (validateTelegramTokensResult.IsError)
        {
            logger.InvalidTelegramTokens(request);
            return validateTelegramTokensResult;
        }

        var authorizeRequestValidationResult4 = await ValidateClientAsync(request);
        if (authorizeRequestValidationResult4.IsError)
        {
            return authorizeRequestValidationResult4;
        }
        var authorizeRequestValidationResult5 = ValidateCoreParameters(request);
        if (authorizeRequestValidationResult5.IsError)
        {
            return authorizeRequestValidationResult5;
        }
        var authorizeRequestValidationResult6 = await ValidateScopeAndResourceAsync(request);
        if (authorizeRequestValidationResult6.IsError)
        {
            return authorizeRequestValidationResult6;
        }
        var authorizeRequestValidationResult7 = await ValidateOptionalParametersAsync(request);
        if (authorizeRequestValidationResult7.IsError)
        {
            return authorizeRequestValidationResult7;
        }
        // Logger.LogDebug(
        //     "Calling into custom validator: {type}",
        //     customValidator.GetType().FullName
        // );
        var context = new CustomAuthorizeRequestValidationContext
        {
            Result = new AuthorizeRequestValidationResult(request)
        };
        // await customValidator.ValidateAsync(context);
        var result = context.Result;
        if (result.IsError)
        {
            LogError("Error in custom validation", result.Error, request);
            return Invalid(request, result.Error, result.ErrorDescription);
        }
        Logger.LogTrace("Authorize request protocol validation successful");
        // IdentityServerLicenseValidator.Instance.ValidateClient(request.ClientId);
        return Valid(request);
    }

    private async Task<AuthorizeRequestValidationResult> LoadClientAsync(
        ValidatedAuthorizeRequest request
    )
    {
        var clientId = request.Raw.Get("client_id");
        if (clientId.IsMissingOrTooLong(options.InputLengthRestrictions.ClientId))
        {
            LogError("client_id is missing or too long", request);
            return Invalid(request, "invalid_request", "Invalid client_id");
        }
        request.ClientId = clientId;
        var client = await clients.FindEnabledClientByIdAsync(request.ClientId);
        if (client == null)
        {
            LogError("Unknown client or not enabled", request.ClientId, request);
            return Invalid(request, "unauthorized_client", "Unknown client or client not enabled");
        }
        request.SetClient(client);
        return Valid(request);
    }

    private async Task<AuthorizeRequestValidationResult> ValidateClientAsync(
        ValidatedAuthorizeRequest request
    )
    {
        if (request.Client.RequireRequestObject && !request.RequestObjectValues.Any())
        {
            return Invalid(
                request,
                "invalid_request",
                "Client must use request object, but no request or request_uri parameter present"
            );
        }
        var redirectUri = request.Raw.Get("redirect_uri");
        if (redirectUri.IsMissingOrTooLong(options.InputLengthRestrictions.RedirectUri))
        {
            LogError("redirect_uri is missing or too long", request);
            return Invalid(request, "invalid_request", "Invalid redirect_uri");
        }
        if (!redirectUri.IsUri())
        {
            LogError("malformed redirect_uri", redirectUri, request);
            return Invalid(request, "invalid_request", "Invalid redirect_uri");
        }
        if (request.Client.ProtocolType != "oidc")
        {
            LogError(
                "Invalid protocol type for OIDC authorize endpoint",
                request.Client.ProtocolType,
                request
            );
            return Invalid(request, "unauthorized_client", "Invalid protocol");
        }
        var context = new RedirectUriValidationContext(redirectUri, request);
        if (!await uriValidator.IsRedirectUriValidAsync(context))
        {
            LogError("Invalid redirect_uri", redirectUri, request);
            return Invalid(request, "invalid_request", "Invalid redirect_uri");
        }
        request.RedirectUri = redirectUri;
        return Valid(request);
    }

    private AuthorizeRequestValidationResult ValidateCoreParameters(
        ValidatedAuthorizeRequest request
    )
    {
        var responseTypeEqualityComparer = new ResponseTypeEqualityComparer();
        var state = request.Raw.Get("state");
        if (state.IsPresent())
        {
            request.State = state;
        }
        var responseType = request.Raw.Get("response_type");
        if (responseType.IsMissing())
        {
            LogError("Missing response_type", request);
            return Invalid(request, "invalid_request", "Missing response_type");
        }
        if (
            !IdentityServerConstants.SupportedResponseTypes.Contains(
                responseType,
                responseTypeEqualityComparer
            )
        )
        {
            Logger.InvalidThingForClient(
                "response type",
                responseType,
                IdentityServerConstants.SupportedResponseTypes
            );
            // LogError("Response type not supported", responseType, request);
            return Invalid(request, "unsupported_response_type", "Response type not supported");
        }
        request.ResponseType = IdentityServerConstants.SupportedResponseTypes.First(
            (string supportedResponseType) =>
                responseTypeEqualityComparer.Equals(supportedResponseType, responseType)
        );
        request.GrantType = IdentityServerConstants.ResponseTypeToGrantTypeMapping[
            request.ResponseType
        ];
        request.ResponseMode = IdentityServerConstants.AllowedResponseModesForGrantType[
            request.GrantType
        ].First();
        if (
            !IdentityServerConstants.AllowedGrantTypesForAuthorizeEndpoint.Contains(
                request.GrantType
            )
        )
        {
            Logger.InvalidThingForClient(
                "grant type",
                request.GrantType,
                IdentityServerConstants.AllowedGrantTypesForAuthorizeEndpoint
            );
            // LogError("Invalid grant type", request.GrantType, request);
            return Invalid(request, "invalid_request", "Invalid response_type");
        }
        if (request.GrantType == "authorization_code" || request.GrantType == "hybrid")
        {
            Logger.LogDebug("Checking for PKCE parameters");
            var authorizeRequestValidationResult = ValidatePkceParameters(request);
            if (authorizeRequestValidationResult.IsError)
            {
                return authorizeRequestValidationResult;
            }
        }
        var responseMode = request.Raw.Get("response_mode");
        if (responseMode.IsPresent())
        {
            if (!IdentityServerConstants.SupportedResponseModes.Contains(responseMode))
            {
                Logger.InvalidThingForClient(
                    "response type",
                    responseMode,
                    IdentityServerConstants.SupportedResponseModes
                );
                return Invalid(request, "unsupported_response_type", "Invalid response_mode");
            }
            if (
                !IdentityServerConstants.AllowedResponseModesForGrantType[
                    request.GrantType
                ].Contains(responseMode)
            )
            {
                Logger.InvalidThingForClient(
                    "response_mode",
                    responseMode,
                    IdentityServerConstants.AllowedResponseModesForGrantType[request.GrantType]
                );
                return Invalid(
                    request,
                    "invalid_request",
                    "Invalid response_mode for response_type"
                );
            }
            request.ResponseMode = responseMode;
        }
        if (!request.Client.AllowedGrantTypes.Contains(request.GrantType))
        {
            Logger.InvalidGrantTypeForClient(request.GrantType, request.Client.AllowedGrantTypes);
            return Invalid(request, "unauthorized_client", "Invalid grant type for client");
        }
        if (
            responseType.FromSpaceSeparatedString().Contains("token")
            && !request.Client.AllowAccessTokensViaBrowser
        )
        {
            LogError(
                "Client requested access token - but client is not configured to receive access tokens via browser",
                request
            );
            return Invalid(
                request,
                "invalid_request",
                "Client not configured to receive access tokens via browser"
            );
        }
        return Valid(request);
    }

    private AuthorizeRequestValidationResult ValidatePkceParameters(
        ValidatedAuthorizeRequest request
    )
    {
        var authorizeRequestValidationResult = Invalid(request);
        var codeChallenge = request.Raw.Get("code_challenge");
        if (codeChallenge.IsMissing())
        {
            if (request.Client.RequirePkce)
            {
                LogError("code_challenge is missing", request);
                authorizeRequestValidationResult.ErrorDescription = "code challenge required";
                return authorizeRequestValidationResult;
            }
            Logger.LogDebug("No PKCE used.");
            return Valid(request);
        }
        if (
            codeChallenge.Length < options.InputLengthRestrictions.CodeChallengeMinLength
            || codeChallenge.Length > options.InputLengthRestrictions.CodeChallengeMaxLength
        )
        {
            LogError("code_challenge is either too short or too long", request);
            authorizeRequestValidationResult.ErrorDescription = "Invalid code_challenge";
            return authorizeRequestValidationResult;
        }
        request.CodeChallenge = codeChallenge;
        string codeChallengeMethod = request.Raw.Get("code_challenge_method");
        if (codeChallengeMethod.IsMissing())
        {
            Logger.LogDebug("Missing code_challenge_method, defaulting to plain");
            codeChallengeMethod = "plain";
        }
        if (!IdentityServerConstants.SupportedCodeChallengeMethods.Contains(codeChallengeMethod))
        {
            LogError("Unsupported code_challenge_method", codeChallengeMethod, request);
            authorizeRequestValidationResult.ErrorDescription = "Transform algorithm not supported";
            return authorizeRequestValidationResult;
        }
        if (codeChallengeMethod == "plain" && !request.Client.AllowPlainTextPkce)
        {
            LogError("code_challenge_method of plain is not allowed", request);
            authorizeRequestValidationResult.ErrorDescription = "Transform algorithm not supported";
            return authorizeRequestValidationResult;
        }
        request.CodeChallengeMethod = codeChallengeMethod;
        return Valid(request);
    }

    private async Task<AuthorizeRequestValidationResult> ValidateScopeAndResourceAsync(
        ValidatedAuthorizeRequest request
    )
    {
        var scope = request.Raw.Get("scope");
        if (scope.IsMissing())
        {
            LogError("scope is missing", request);
            return Invalid(request, "invalid_request", "Invalid scope");
        }
        if (scope.Length > options.InputLengthRestrictions.Scope)
        {
            LogError("scopes too long.", request);
            return Invalid(request, "invalid_request", "Invalid scope");
        }
        request.RequestedScopes = scope.FromSpaceSeparatedString().Distinct().ToList();
        request.IsOpenIdRequest = request.RequestedScopes.Contains("openid");
        var requirement = IdentityServerConstants.ResponseTypeToScopeRequirement[
            request.ResponseType
        ];
        if (
            (
                requirement == IdentityServerConstants.ScopeRequirement.Identity
                || requirement == IdentityServerConstants.ScopeRequirement.IdentityOnly
            ) && !request.IsOpenIdRequest
        )
        {
            LogError("response_type requires the openid scope", request);
            return Invalid(request, "invalid_request", "Missing openid scope");
        }
        var resource = request.Raw.GetValues("resource");
        var resourceIndicators = resource ?? Enumerable.Empty<string>();
        if (
            resourceIndicators?.Any(
                (string x) => x.Length > options.InputLengthRestrictions.ResourceIndicatorMaxLength
            ) ?? false
        )
        {
            return Invalid(request, "invalid_target", "Resource indicator maximum length exceeded");
        }
        /// TODO: Fix this
        // if (!resourceIndicators.AreValidResourceIndicatorFormat(Logger))
        // {
        //     return Invalid(request, "invalid_target", "Invalid resource indicator format");
        // }
        if (request.GrantType == "implicit" && resourceIndicators.Any())
        {
            return Invalid(
                request,
                "invalid_target",
                "Resource indicators not allowed for response_type 'token'."
            );
        }
        request.RequestedResourceIndicators = resourceIndicators;
        var resourceValidationResult = await resourceValidator.ValidateRequestedResourcesAsync(
            new ResourceValidationRequest
            {
                Client = request.Client,
                Scopes = request.RequestedScopes,
                ResourceIndicators = resourceIndicators
            }
        );
        if (!resourceValidationResult.Succeeded)
        {
            if (resourceValidationResult.InvalidResourceIndicators.Any())
            {
                return Invalid(request, "invalid_target", "Invalid resource indicator");
            }
            if (resourceValidationResult.InvalidScopes.Any())
            {
                return Invalid(request, "invalid_scope", "Invalid scope");
            }
        }
        // IdentityServerLicenseValidator.Instance.ValidateResourceIndicators(resourceIndicators);
        if (resourceValidationResult.Resources.IdentityResources.Any() && !request.IsOpenIdRequest)
        {
            LogError("Identity related scope requests, but no openid scope", request);
            return Invalid(
                request,
                "invalid_scope",
                "Identity scopes requested, but openid scope is missing"
            );
        }
        if (resourceValidationResult.Resources.ApiScopes.Any())
        {
            request.IsApiResourceRequest = true;
        }
        bool flag = true;
        switch (requirement)
        {
            case IdentityServerConstants.ScopeRequirement.Identity:
                if (!resourceValidationResult.Resources.IdentityResources.Any())
                {
                    Logger.LogError(
                        "Requests for id_token response type must include identity scopes"
                    );
                    flag = false;
                }
                break;
            case IdentityServerConstants.ScopeRequirement.IdentityOnly:
                if (
                    !resourceValidationResult.Resources.IdentityResources.Any()
                    || resourceValidationResult.Resources.ApiScopes.Any()
                )
                {
                    Logger.LogError(
                        "Requests for id_token response type only must not include resource scopes"
                    );
                    flag = false;
                }
                break;
            case IdentityServerConstants.ScopeRequirement.ResourceOnly:
                if (
                    resourceValidationResult.Resources.IdentityResources.Any()
                    || !resourceValidationResult.Resources.ApiScopes.Any()
                )
                {
                    Logger.LogError(
                        "Requests for token response type only must include resource scopes, but no identity scopes."
                    );
                    flag = false;
                }
                break;
        }
        if (!flag)
        {
            return Invalid(request, "invalid_scope", "Invalid scope for response type");
        }
        request.ValidatedResources = resourceValidationResult;
        return Valid(request);
    }

    private async Task<AuthorizeRequestValidationResult> ValidateOptionalParametersAsync(
        ValidatedAuthorizeRequest request
    )
    {
        var nonce = request.Raw.Get("nonce");
        if (nonce.IsPresent())
        {
            if (nonce.Length > options.InputLengthRestrictions.Nonce)
            {
                LogError("Nonce too long", request);
                return Invalid(request, "invalid_request", "Invalid nonce");
            }
            request.Nonce = nonce;
        }
        else if (request.ResponseType.FromSpaceSeparatedString().Contains("id_token"))
        {
            LogError("Nonce required for flow with id_token response type", request);
            return Invalid(request, "invalid_request", "Invalid nonce");
        }
        var prompt = request.Raw.Get("prompt");
        if (prompt.IsPresent())
        {
            var array = prompt.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (
                !array.All(
                    p =>
                        // TODO: Fix this
                        /*options.UserInteraction.PromptValuesSupported?.Contains(p) ?? false*/true
                )
            )
            {
                LogError("Unsupported prompt mode", request);
                return Invalid(request, "invalid_request", "Unsupported prompt mode");
            }
            if (array.Contains("none") && array.Length > 1)
            {
                LogError(
                    "prompt contains 'none' and other values. 'none' should be used by itself.",
                    request
                );
                return Invalid(request, "invalid_request", "Invalid prompt");
            }
            if (array.Contains("create") && array.Length > 1)
            {
                LogError(
                    "prompt contains 'create' and other values. 'create' should be used by itself.",
                    request
                );
                return Invalid(request, "invalid_request", "Invalid prompt");
            }
            request.OriginalPromptModes = array;
        }
        var suppressedPrompt = request.Raw.Get("suppressed_prompt");
        if (suppressedPrompt.IsPresent())
        {
            var array2 = suppressedPrompt.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (
                !array2.All(
                    (string p) =>
                        // TODO: Fix this
                        /*options.UserInteraction.PromptValuesSupported?.Contains(p) ??false*/true
                )
            )
            {
                LogError("Unsupported processed_prompt mode.", request);
                return Invalid(request, "invalid_request", "Invalid prompt");
            }
            if (array2.Contains("none") && array2.Length > 1)
            {
                LogError(
                    "processed_prompt contains 'none' and other values. 'none' should be used by itself.",
                    request
                );
                return Invalid(request, "invalid_request", "Invalid prompt");
            }
            if (array2.Contains("create") && array2.Length > 1)
            {
                LogError(
                    "prompt contains 'create' and other values. 'create' should be used by itself.",
                    request
                );
                return Invalid(request, "invalid_request", "Invalid prompt");
            }
            request.ProcessedPromptModes = array2;
        }
        request.PromptModes = request.OriginalPromptModes
            .Except(request.ProcessedPromptModes)
            .ToArray();
        var uiLocales = request.Raw.Get("ui_locales");
        if (uiLocales.IsPresent())
        {
            if (uiLocales.Length > options.InputLengthRestrictions.UiLocale)
            {
                LogError("UI locale too long", request);
                return Invalid(request, "invalid_request", "Invalid ui_locales");
            }
            request.UiLocales = uiLocales;
        }
        var display = request.Raw.Get("display");
        if (display.IsPresent())
        {
            if (IdentityServerConstants.SupportedDisplayModes.Contains(display))
            {
                request.DisplayMode = display;
            }
            Logger.LogDebug("Unsupported display mode - ignored: " + display);
        }
        var maxAge = request.Raw.Get("max_age");
        if (maxAge.IsPresent())
        {
            if (!int.TryParse(maxAge, out var result))
            {
                LogError("Invalid max_age.", request);
                return Invalid(request, "invalid_request", "Invalid max_age");
            }
            if (result < 0)
            {
                LogError("Invalid max_age.", request);
                return Invalid(request, "invalid_request", "Invalid max_age");
            }
            request.MaxAge = result;
        }
        var loginHint = request.Raw.Get("login_hint");
        if (loginHint.IsPresent())
        {
            if (loginHint.Length > options.InputLengthRestrictions.LoginHint)
            {
                LogError("Login hint too long", request);
                return Invalid(request, "invalid_request", "Invalid login_hint");
            }
            request.LoginHint = loginHint;
        }
        var acrValue = request.Raw.Get("acr_values");
        if (acrValue.IsPresent())
        {
            if (acrValue.Length > options.InputLengthRestrictions.AcrValues)
            {
                LogError("Acr values too long", request);
                return Invalid(request, "invalid_request", "Invalid acr_values");
            }
            request.AuthenticationContextReferenceClasses = acrValue
                .FromSpaceSeparatedString()
                .Distinct()
                .ToList();
        }
        var idP = request.GetIdP();
        if (
            idP.IsPresent()
            && request.Client.IdentityProviderRestrictions != null
            && request.Client.IdentityProviderRestrictions.Any()
            && !request.Client.IdentityProviderRestrictions.Contains(idP)
        )
        {
            Logger.LogWarning("idp requested ({idp}) is not in client restriction list.", idP);
            request.RemoveIdP();
        }
        if (request.Subject.IsAuthenticated())
        {
            var sessionId = await userSession.GetSessionIdAsync();
            if (sessionId.IsPresent())
            {
                request.SessionId = sessionId;
            }
            else
            {
                LogError("SessionId is missing", request);
            }
        }
        else
        {
            request.SessionId = "";
        }
        var dpopJkt = request.Raw.Get("dpop_jkt");
        if (dpopJkt.IsPresent())
        {
            if (dpopJkt.Length > options.InputLengthRestrictions.DPoPKeyThumbprint)
            {
                LogError("dpop_jwt value too long", request);
                return Invalid(request, "invalid_request", "Invalid dpop_jwt");
            }
            request.DPoPKeyThumbprint = dpopJkt;
        }
        return Valid(request);
    }

    private AuthorizeRequestValidationResult Invalid(
        ValidatedAuthorizeRequest request,
        string error = "invalid_request",
        string description = null
    )
    {
        return new AuthorizeRequestValidationResult(request, error, description);
    }

    private AuthorizeRequestValidationResult Valid(ValidatedAuthorizeRequest request)
    {
        return new AuthorizeRequestValidationResult(request);
    }

    private void LogError(string message, ValidatedAuthorizeRequest request)
    {
        Logger.InvalidRequest(message, request);
        // TODO: Fix this
        // var authorizeRequestValidationLog =
        //     new AuthorizeRequestValidationLog(
        //         request,
        //         options.Logging.AuthorizeRequestSensitiveValuesFilter
        //     );
        // Logger.LogError(
        //     message + "\n{@requestDetails}" /*, authorizeRequestValidationLog*/
        // );
    }

    private void LogError(string message, string detail, ValidatedAuthorizeRequest request)
    {
        // TODO: Fix this
        /*AuthorizeRequestValidationLog authorizeRequestValidationLog =
            new AuthorizeRequestValidationLog(
                request,
                options.Logging.AuthorizeRequestSensitiveValuesFilter
            );*/
        Logger.InvalidRequest(message, detail, request);
        //     message + ": {detail}\n{@requestDetails}",
        //     detail /*,
        //     authorizeRequestValidationLog*/
        // );
    }
}
