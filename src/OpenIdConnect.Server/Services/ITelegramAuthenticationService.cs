namespace Telegram.AspNetCore.Authentication;

using Dgmjr.Identity.ClaimValueTypes;

using Telegram.OpenIdConnect.Models.Responses;
using Telegram.OpenIdConnect.Models.Requests;

public interface ITelegramJwtFactory : ILog
{
    TokenResponseOrError GenerateToken(TokenRequest request);
}
