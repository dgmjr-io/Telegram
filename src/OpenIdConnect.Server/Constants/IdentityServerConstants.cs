namespace Telegram.OpenIdConnect.Constants;

using Duende.IdentityServer.Models;

using IdentityModel;

public static partial class IdentityServerConstants
{
    public const string IdentityServerName = "Duende.IdentityServer";
    public const string IdentityServerAuthenticationType = IdentityServerName;
    public const string ExternalAuthenticationMethod = "external";
    public const string DefaultHashAlgorithm = "SHA256";

    public static readonly TimeSpan DefaultCookieTimeSpan = TimeSpan.FromHours(10);
    public static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromMinutes(60);

    public static readonly List<string> SupportedResponseTypes =
        new()
        {
            OidcConstants.ResponseTypes.Code,
            OidcConstants.ResponseTypes.Token,
            OidcConstants.ResponseTypes.IdToken,
            OidcConstants.ResponseTypes.IdTokenToken,
            OidcConstants.ResponseTypes.CodeIdToken,
            OidcConstants.ResponseTypes.CodeToken,
            OidcConstants.ResponseTypes.CodeIdTokenToken
        };

    public static readonly StringDictionary ResponseTypeToGrantTypeMapping =
        new()
        {
            { OidcConstants.ResponseTypes.Code, GrantType.AuthorizationCode },
            { OidcConstants.ResponseTypes.Token, GrantType.Implicit },
            { OidcConstants.ResponseTypes.IdToken, GrantType.Implicit },
            { OidcConstants.ResponseTypes.IdTokenToken, GrantType.Implicit },
            { OidcConstants.ResponseTypes.CodeIdToken, GrantType.Hybrid },
            { OidcConstants.ResponseTypes.CodeToken, GrantType.Hybrid },
            { OidcConstants.ResponseTypes.CodeIdTokenToken, GrantType.Hybrid }
        };

    public static readonly List<string> AllowedGrantTypesForAuthorizeEndpoint =
        new() { GrantType.AuthorizationCode, GrantType.Implicit, GrantType.Hybrid };

    public static readonly List<string> SupportedCodeChallengeMethods =
        new()
        {
            OidcConstants.CodeChallengeMethods.Plain,
            OidcConstants.CodeChallengeMethods.Sha256
        };

    public enum ScopeRequirement
    {
        None,
        ResourceOnly,
        IdentityOnly,
        Identity
    }

    public static readonly Dictionary<string, ScopeRequirement> ResponseTypeToScopeRequirement =
        new()
        {
            { OidcConstants.ResponseTypes.Code, ScopeRequirement.None },
            { OidcConstants.ResponseTypes.Token, ScopeRequirement.ResourceOnly },
            { OidcConstants.ResponseTypes.IdToken, ScopeRequirement.IdentityOnly },
            { OidcConstants.ResponseTypes.IdTokenToken, ScopeRequirement.Identity },
            { OidcConstants.ResponseTypes.CodeIdToken, ScopeRequirement.Identity },
            { OidcConstants.ResponseTypes.CodeToken, ScopeRequirement.Identity },
            { OidcConstants.ResponseTypes.CodeIdTokenToken, ScopeRequirement.Identity }
        };

    public static readonly Dictionary<
        string,
        IEnumerable<string>
    > AllowedResponseModesForGrantType =
        new()
        {
            {
                GrantType.AuthorizationCode,
                new[]
                {
                    OidcConstants.ResponseModes.Query,
                    OidcConstants.ResponseModes.FormPost,
                    OidcConstants.ResponseModes.Fragment
                }
            },
            {
                GrantType.Hybrid,
                new[] { OidcConstants.ResponseModes.Fragment, OidcConstants.ResponseModes.FormPost }
            },
            {
                GrantType.Implicit,
                new[] { OidcConstants.ResponseModes.Fragment, OidcConstants.ResponseModes.FormPost }
            }
        };

    public static readonly List<string> SupportedResponseModes =
        new()
        {
            OidcConstants.ResponseModes.FormPost,
            OidcConstants.ResponseModes.Query,
            OidcConstants.ResponseModes.Fragment
        };

    public static string[] SupportedSubjectTypes = { "pairwise", "public" };

    public static class SigningAlgorithms
    {
        public const string RSA_SHA_256 = "RS256";
    }

    public static readonly List<string> SupportedDisplayModes =
        new()
        {
            OidcConstants.DisplayModes.Page,
            OidcConstants.DisplayModes.Popup,
            OidcConstants.DisplayModes.Touch,
            OidcConstants.DisplayModes.Wap
        };

    public static readonly List<string> SupportedPromptModes =
        new()
        {
            OidcConstants.PromptModes.None,
            OidcConstants.PromptModes.Login,
            OidcConstants.PromptModes.Consent,
            OidcConstants.PromptModes.SelectAccount,
            // Create not in here by default -- it's added if customer sets the CreateAccountUrl user interaction option
            //OidcConstants.PromptModes.Create,
        };

    /// <summary>
    /// The name of the parameter passed to the authorize callback to indicate
    /// prompt modes that have already been used. This constant is deprecated in
    /// favor of <see cref="ProcessedPrompt"/>.
    /// </summary>
    [Obsolete("Use the ProcessedPrompt constant instead.")]
    public const string SuppressedPrompt = ProcessedPrompt;

    /// <summary>
    /// The name of the parameter passed to the authorize callback to indicate
    /// prompt modes that have already been used. This constant replaces the
    /// deprecated <see cref="SuppressedPrompt"/>, while keeping the underlying
    /// value unchanged.
    /// </summary>
    public const string ProcessedPrompt = "suppressed_" + OidcConstants.AuthorizeRequest.Prompt;

    public static class KnownAcrValues
    {
        public const string HomeRealm = "idp:";
        public const string Tenant = "tenant:";

        public static readonly string[] All = { HomeRealm, Tenant };
    }

    public static Dictionary<string, int> ProtectedResourceErrorStatusCodes =
        new()
        {
            { OidcConstants.ProtectedResourceErrors.InvalidToken, 401 },
            { OidcConstants.ProtectedResourceErrors.ExpiredToken, 401 },
            { OidcConstants.ProtectedResourceErrors.InvalidRequest, 400 },
            { OidcConstants.ProtectedResourceErrors.InsufficientScope, 403 }
        };

    public static readonly Dictionary<string, IEnumerable<string>> ScopeToClaimsMapping =
        new()
        {
            {
                StandardScopes.Profile,
                new[]
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.FamilyName,
                    JwtClaimTypes.GivenName,
                    JwtClaimTypes.MiddleName,
                    JwtClaimTypes.NickName,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Profile,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.WebSite,
                    JwtClaimTypes.Gender,
                    JwtClaimTypes.BirthDate,
                    JwtClaimTypes.ZoneInfo,
                    JwtClaimTypes.Locale,
                    JwtClaimTypes.UpdatedAt
                }
            },
            { StandardScopes.Email, new[] { JwtClaimTypes.Email, JwtClaimTypes.EmailVerified } },
            { StandardScopes.Address, new[] { JwtClaimTypes.Address } },
            {
                StandardScopes.Phone,
                new[] { JwtClaimTypes.PhoneNumber, JwtClaimTypes.PhoneNumberVerified }
            },
            { StandardScopes.OpenId, new[] { JwtClaimTypes.Subject } }
        };

    public static class UIConstants
    {
        // the limit after which old messages are purged
        public const int CookieMessageThreshold = 2;

        public static class DefaultRoutePathParams
        {
            public const string Error = "errorId";
            public const string Login = "returnUrl";
            public const string Consent = "returnUrl";
            public const string CreateAccount = "returnUrl";
            public const string Logout = "logoutId";
            public const string EndSessionCallback = "endSessionId";
            public const string Custom = "returnUrl";
            public const string UserCode = "userCode";
        }

        public static class DefaultRoutePaths
        {
            public const string Login = "/account/login";
            public const string Logout = "/account/logout";
            public const string Consent = "/consent";
            public const string Error = "/home/error";
            public const string DeviceVerification = "/device";
        }
    }

    public static class EnvironmentKeys
    {
        public const string IdentityServerBasePath = "idsvr:IdentityServerBasePath";
        public const string SignOutCalled = "idsvr:IdentityServerSignOutCalled";
    }

    public static class TokenTypeHints
    {
        public const string RefreshToken = "refresh_token";
        public const string AccessToken = "access_token";
    }

    public static List<string> SupportedTokenTypeHints = new List<string>
    {
        TokenTypeHints.RefreshToken,
        TokenTypeHints.AccessToken
    };

    public static class RevocationErrors
    {
        public const string UnsupportedTokenType = "unsupported_token_type";
    }

    public class Filters
    {
        // filter for claims from an incoming access token (e.g. used at the user profile endpoint)
        public static readonly string[] ProtocolClaimsFilter =
        {
            JwtClaimTypes.AccessTokenHash,
            JwtClaimTypes.Audience,
            JwtClaimTypes.AuthorizedParty,
            JwtClaimTypes.AuthorizationCodeHash,
            JwtClaimTypes.ClientId,
            JwtClaimTypes.Expiration,
            JwtClaimTypes.IssuedAt,
            JwtClaimTypes.Issuer,
            JwtClaimTypes.JwtId,
            JwtClaimTypes.Nonce,
            JwtClaimTypes.NotBefore,
            JwtClaimTypes.ReferenceTokenId,
            JwtClaimTypes.SessionId,
            JwtClaimTypes.Scope
        };

        // filter list for claims returned from profile service prior to creating tokens
        public static readonly string[] ClaimsServiceFilterClaimTypes =
        {
            // TODO: consider JwtClaimTypes.AuthenticationContextClassReference,
            JwtClaimTypes.AccessTokenHash,
            JwtClaimTypes.Audience,
            JwtClaimTypes.AuthenticationMethod,
            JwtClaimTypes.AuthenticationTime,
            JwtClaimTypes.AuthorizedParty,
            JwtClaimTypes.AuthorizationCodeHash,
            JwtClaimTypes.ClientId,
            JwtClaimTypes.Expiration,
            JwtClaimTypes.IdentityProvider,
            JwtClaimTypes.IssuedAt,
            JwtClaimTypes.Issuer,
            JwtClaimTypes.JwtId,
            JwtClaimTypes.Nonce,
            JwtClaimTypes.NotBefore,
            JwtClaimTypes.ReferenceTokenId,
            JwtClaimTypes.SessionId,
            JwtClaimTypes.Subject,
            JwtClaimTypes.Scope,
            JwtClaimTypes.Confirmation
        };

        public static readonly string[] JwtRequestClaimTypesFilter =
        {
            JwtClaimTypes.Audience,
            JwtClaimTypes.Expiration,
            JwtClaimTypes.IssuedAt,
            JwtClaimTypes.Issuer,
            JwtClaimTypes.NotBefore,
            JwtClaimTypes.JwtId
        };
    }

    public static class WsFedSignOut
    {
        public const string LogoutUriParameterName = "wa";
        public const string LogoutUriParameterValue = "wsignoutcleanup1.0";
    }

    public static class AuthorizationParamsStore
    {
        public const string MessageStoreIdParameterName = "authzId";
    }

    public static class CurveOids
    {
        public const string P256 = "1.2.840.10045.3.1.7";
        public const string P384 = "1.3.132.0.34";
        public const string P521 = "1.3.132.0.35";
    }

    public const string SHash = "s_hash";
    public const string Idp = "idp";
    public const string UserId = nameof(UserId);
    public const string RedirectUri = "redirect_uri";
}
