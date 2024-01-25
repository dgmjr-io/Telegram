using Telegram.OpenIdConnect.Models.Responses;

namespace Telegram.OpenIdConnect.Services.CodeService;

public interface ICodeStoreService
{
    Task<string?> GenerateAuthorizationCodeAsync(string clientId, IList<string> requestedScope);
    Task<AuthorizationCode?> GetClientDataByCodeAsync(string code);
    Task<AuthorizationCode?> RemoveClientDataByCodeAsync(string key);
    Task<AuthorizationCode?> UpdatedClientDataByCodeAsync(
        string code,
        IList<string> requestedScopes,
        string userName,
        string? password = null,
        string? nonce = null
    );
}
