using System.Collections.Concurrent;
using System.Security.Claims;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

using Telegram.OpenIdConnect.Models.Responses;
using Telegram.OpenIdConnect.Options;

namespace Telegram.OpenIdConnect.Services.CodeService;

public class CodeStoreService(
    IDistributedCache cache,
    IOptionsMonitor<TelegramOpenIdConnectServerOptions> options,
    IOptionsMonitor<JsonOptions> jsonOptions
) : ICodeStoreService
{
    private TelegramOpenIdConnectServerOptions Options => options.CurrentValue;
    private IDistributedCache Cache => cache;
    private ClientStore Clients => Options.Clients;
    private Jso JsonOptions => jsonOptions.CurrentValue.JsonSerializerOptions;

    // Here I generate the code for authorization, and I will store it
    // in the Concurrent Dictionary
    public async Task<string?> GenerateAuthorizationCodeAsync(
        string clientId,
        IList<string> requestedScope
    )
    {
        var client = Clients.Find(x => x.ClientId == clientId);

        if (client != null)
        {
            var code = guid.NewGuid().ToString();

            var authCode = new AuthorizationCode
            {
                Code = code,
                ClientId = clientId,
                RedirectUris = client.RedirectUris.Select(uri => uri.ToString()).ToArray(),
                RequestedScopes = requestedScope,
            };

            // then store the code is the distributed cache
            await Cache.SetAsync(
                $"AUTH_CODE:{code}",
                Serialize(authCode.ToString(), JsonOptions).ToUTF8Bytes()
            );

            return code;
        }
        return null;
    }

    public async Task<AuthorizationCode?> GetClientDataByCodeAsync(string code) =>
        Deserialize<AuthorizationCode>(
            await Cache.GetStringAsync($"AUTH_CODE:{code}"),
            JsonOptions
        );

    public async Task<AuthorizationCode?> RemoveClientDataByCodeAsync(string key)
    {
        AuthorizationCode? authorizationCode = default;
        await Cache
            .GetStringAsync($"AUTH_CODE:{key}")
            .ContinueWith(task =>
            {
                authorizationCode = Deserialize<AuthorizationCode>(task.Result, JsonOptions);
                Cache.Remove($"AUTH_CODE:{key}");
            });
        return authorizationCode;
    }

    // TODO
    // Before updated the Concurrent Dictionary I have to Process User Sign In,
    // and check the user credential first
    // But here I merge this process here inside update Concurrent Dictionary method
    public async Task<AuthorizationCode?> UpdatedClientDataByCodeAsync(
        string code,
        IList<string> requestedScopes,
        string userName,
        string? password = null,
        string? nonce = null
    )
    {
        var oldValue = await GetClientDataByCodeAsync(code);

        if (oldValue != null)
        {
            // check the requested scopes with the one that are stored in the Client Store
            var client = Clients.Find(x => x.ClientId == oldValue.ClientId);

            if (client != null)
            {
                var clientScope = (
                    from m in client.AllowedScopes
                    where requestedScopes.Contains(m)
                    select m
                ).ToList();

                if (clientScope.Count == 0)
                    return null;

                var newValue = new AuthorizationCode
                {
                    Code = oldValue.Code,
                    ClientId = oldValue.ClientId,
                    CreationTime = oldValue.CreationTime,
                    RedirectUris = oldValue.RedirectUris,
                    RequestedScopes = requestedScopes,
                    Nonce = nonce
                };

                // ------------------ I suppose the user name and password is correct  -----------------
                var claims = new List<Claim>();

                if (newValue.IsOpenId)
                {
                    // TODO
                    // Add more claims to the claims
                }

                var claimIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );
                newValue.Subject = new ClaimsPrincipal(claimIdentity);
                // ------------------ -----------------------------------------------  -----------------

                await Cache.SetAsync(
                    $"AUTH_CODE:{code}",
                    Serialize(newValue.ToString(), JsonOptions).ToUTF8Bytes()
                );

                return newValue;
            }
        }
        return null;
    }
}
