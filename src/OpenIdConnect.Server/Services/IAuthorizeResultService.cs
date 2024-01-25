namespace Telegram.OpenIdConnect.Services;

using OneOf;

using Telegram.OpenIdConnect.Models.Responses;
using Telegram.OpenIdConnect.Models.Requests;

public interface IAuthorizationService
{
    Task<AuthorizeResponseOrError> AuthorizeRequestAsync(
        IHttpContextAccessor httpContextAccessor,
        AuthorizationRequest authorizationRequest
    );
    Task<TokenResponseOrError> GenerateTokenAsync(TokenRequest request);
}
