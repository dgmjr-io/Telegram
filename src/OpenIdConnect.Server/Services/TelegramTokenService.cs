namespace Telegram.OpenIdConnect.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

using Duende.IdentityServer;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Extensions;

using IdentityModel;

using Telegram.OpenIdConnect.Extensions;
using Telegram.OpenIdConnect.Telemetry;
using Telegram.OpenIdConnect.Constants;

public class TelegramTokenService(
    IClaimsService claimsProvider,
    IReferenceTokenStore referenceTokenStore,
    ITokenCreationService creationService,
    IClock clock,
    IKeyMaterialService keyMaterialService,
    IdentityServerOptions options,
    ILogger<TelegramTokenService> logger
)
    : DefaultTokenService(
        claimsProvider,
        referenceTokenStore,
        creationService,
        clock,
        keyMaterialService,
        options,
        logger
    ),
        ILog
{
    public new ILogger Logger => logger;
    private static readonly SHA256 Sha = SHA256.Create();

    // private IKeyMaterialService KeyMaterialService => keyMaterialService;

    public override async Task<Token> CreateAccessTokenAsync(TokenCreationRequest request)
    {
        Logger.StartingAccessTokenCreation();
        using var activity = Activities.ServiceActivitySource.StartActivity(
            $"{nameof(TelegramTokenService)}.{nameof(CreateAccessTokenAsync)}"
        );

        request.Validate();
        var claims = new List<Claim>();
        var list = claims;
        list.AddRange(
            await ClaimsProvider.GetAccessTokenClaimsAsync(
                request.Subject,
                request.ValidatedResources,
                request.ValidatedRequest
            )
        );
        if (request.ValidatedRequest.SessionId.IsPresent())
        {
            claims.Add(new(JwtRegisteredClaimNames.Sid, request.ValidatedRequest.SessionId));
        }
        var issuerName = request.ValidatedRequest.IssuerName;
        var token = new Token(Constants.IdentityServerConstants.TokenTypes.AccessToken)
        {
            CreationTime = Clock.UtcNow.UtcDateTime,
            Issuer = issuerName,
            Lifetime = request.ValidatedRequest.AccessTokenLifetime,
            IncludeJwtId = request.ValidatedRequest.Client.IncludeJwtId,
            Claims = claims.Distinct(new ClaimComparer()).ToList(),
            ClientId = request.ValidatedRequest.Client.ClientId,
            Description = request.Description,
            AccessTokenType = request.ValidatedRequest.AccessTokenType,
            AllowedSigningAlgorithms = request.ValidatedResources.Resources.ApiResources
                .SelectMany(res => res.AllowedAccessTokenSigningAlgorithms)
                .Distinct()
                .ToList()
        };
        foreach (
            var item in request.ValidatedResources.Resources.ApiResources
                .Select((ApiResource x) => x.Name)
                .Distinct()
        )
        {
            token.Audiences.Add(item);
        }
        if (Options.EmitStaticAudienceClaim)
        {
            token.Audiences.Add($"{issuerName.EnsureTrailingSlash()}resources");
        }
        if (request.ValidatedRequest.Confirmation.IsPresent())
        {
            token.Confirmation = request.ValidatedRequest.Confirmation;
        }
        return token;
    }

    public override async Task<Token> CreateIdentityTokenAsync(TokenCreationRequest request)
    {
        Logger.StartingIdTokenCreation();

        request.Validate();
        var algorithm = (
            (
                await KeyMaterialService.GetSigningCredentialsAsync(
                    request.ValidatedRequest.Client.AllowedIdentityTokenSigningAlgorithms
                )
            ) ?? throw new InvalidOperationException("No signing credential is configured.")
        ).Algorithm;
        var claims = new List<Claim>();
        if (request.Nonce.IsPresent())
        {
            claims.Add(new(JwtRegisteredClaimNames.Nonce, request.Nonce));
        }
        if (request.AccessTokenToHash.IsPresent())
        {
            claims.Add(
                new(
                    JwtRegisteredClaimNames.AtHash,
                    CryptoHelper.CreateHashClaimValue(request.AccessTokenToHash, algorithm)
                )
            );
        }
        if (request.AuthorizationCodeToHash.IsPresent())
        {
            claims.Add(
                new(
                    JwtRegisteredClaimNames.CHash,
                    CryptoHelper.CreateHashClaimValue(request.AuthorizationCodeToHash, algorithm)
                )
            );
        }
        if (request.StateHash.IsPresent())
        {
            claims.Add(new(Constants.IdentityServerConstants.SHash, request.StateHash));
        }
        if (request.ValidatedRequest.SessionId.IsPresent())
        {
            claims.Add(new(JwtRegisteredClaimNames.Sid, request.ValidatedRequest.SessionId));
        }
        var list = claims;
        list.AddRange(
            await ClaimsProvider.GetIdentityTokenClaimsAsync(
                request.Subject,
                request.ValidatedResources,
                request.IncludeAllIdentityClaims,
                request.ValidatedRequest
            )
        );
        var issuerName = request.ValidatedRequest.IssuerName;
        return new(Constants.IdentityServerConstants.TokenTypes.IdentityToken)
        {
            CreationTime = Clock.UtcNow.UtcDateTime,
            Audiences = { request.ValidatedRequest.Client.ClientId },
            Issuer = issuerName,
            Lifetime = request.ValidatedRequest.Client.IdentityTokenLifetime,
            Claims = claims.Distinct(new ClaimComparer()).ToList(),
            ClientId = request.ValidatedRequest.Client.ClientId,
            AccessTokenType = request.ValidatedRequest.AccessTokenType,
            AllowedSigningAlgorithms = request
                .ValidatedRequest
                .Client
                .AllowedIdentityTokenSigningAlgorithms
        };
    }

    public override async Task<string> CreateSecurityTokenAsync(Token token)
    {
        using var activity = Activities.ServiceActivitySource.StartActivity(
            $"{nameof(TelegramTokenService)}.{nameof(CreateSecurityTokenAsync)}"
        );
        string result;
        if (token.Type == Constants.IdentityServerConstants.TokenTypes.AccessToken)
        {
            var claim = token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti);
            if (token.IncludeJwtId || (claim != null && token.Version < 5))
            {
                if (claim != null)
                {
                    token.Claims.Remove(claim);
                }
                token.Claims.Add(
                    new(
                        JwtRegisteredClaimNames.Jti,
                        CryptoRandom.CreateUniqueId(16, CryptoRandom.OutputFormat.Hex)
                    )
                );
            }
            if (token.AccessTokenType == AccessTokenType.Jwt)
            {
                Logger.CreatingJwtAccessToken();
                result = await CreationService.CreateTokenAsync(token);
            }
            else
            {
                Logger.CreatingReferenceAccessToken();
                result = await ReferenceTokenStore.StoreReferenceTokenAsync(token);
            }
        }
        else
        {
            if (token.Type is not Constants.IdentityServerConstants.TokenTypes.IdentityToken)
            {
                throw new InvalidOperationException("Invalid token type.");
            }
            Logger.CreatingJwtIdentityToken();
            result = await CreationService.CreateTokenAsync(token);
        }
        return result;
    }

    /// <summary>
    /// Hashes an additional data (e.g. for c_hash or at_hash).
    /// </summary>
    /// <param name="tokenToHash">The token to hash.</param>
    /// <returns></returns>
    protected virtual string HashAdditionalData(string tokenToHash)
    {
        var hash = Sha.ComputeHash(ASCII.GetBytes(tokenToHash));

        var leftPart = new byte[16];
        Copy(hash, leftPart, 16);

        return Base64Url.Encode(leftPart);
    }
}
