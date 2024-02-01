namespace Telegram.OpenIdConnect.Extensions;
using Constants;
using Options;
using Models;
using Application = Dgmjr.Mime.Application;

public static partial class HttpRequestExtensions
{
    private const string ClientIdKey = "client_id";

    public static void ClearClientId(this HttpContext context)
    {
        context.Session.Remove(SessionKeys.ClientId);
        context.Response.Cookies.Delete(SessionKeys.ClientId);
        context.Response.Redirect("/");
    }

    private static string? SetClientId(this HttpRequest request, string? clientId)
    {
        if (!IsNullOrEmpty(clientId))
        {
            request.HttpContext.Session.SetString(SessionKeys.ClientId, clientId);
            request.HttpContext.Response.Cookies.Delete(SessionKeys.ClientId);
            request.HttpContext.Response.Cookies.Append(SessionKeys.ClientId, clientId);
        }

        return clientId;
    }

    public static TelegramOpenIdConnectServerOptions? GetOptions(this HttpRequest request)
        => request.HttpContext.RequestServices.GetService<IOptionsMonitor<TelegramOpenIdConnectServerOptions>>().CurrentValue;

    public static TelegramOidcClient[] GetClients(this HttpRequest request)
        => [..request.GetOptions().Clients];

    public static TelegramOidcClient? GetClient(this HttpRequest request)
        => request.GetOptions().Clients[request.GetClientId()] ??
                request.GetOptions().Clients.FirstOrDefault();

    public static string? GetClientId(this HttpRequest request)
        => request.SetClientId(!IsNullOrEmpty(request.GetClientIdFromQuery()) ? request.GetClientIdFromQuery() :
            !IsNullOrEmpty(request.GetClientIdFromReturnUrl()) ? request.GetClientIdFromReturnUrl() :
            !IsNullOrEmpty(request.GetClientIdFromCookies()) ? request.GetClientIdFromCookies() :
            !IsNullOrEmpty(request.GetClientIdFromCookies()) ? request.GetClientIdFromCookies() :
            !IsNullOrEmpty(request.GetClientIdFromForm()) ? request.GetClientIdFromForm() : null);

    public static string? GetClientIdFromQuery(this HttpRequest request)
        => request.Query[ClientIdKey];

    [GeneratedRegex("client_id=(?<client_id>[^&]+)")]
    public static partial Regx ClientIdFromReturnUrlRegex ();

    public static string? GetClientIdFromReturnUrl(this HttpRequest request)
        => !IsNullOrEmpty(request.Query["ReturnUrl"].FirstOrDefault()) ?
            ClientIdFromReturnUrlRegex().Match(request.Query["ReturnUrl"]).Groups[ClientIdKey]?.Value :
            null;

    public static string? GetClientIdFromCookies(this HttpRequest request)
        => request.Cookies[SessionKeys.ClientId];

    public static string? GetClientIdFromSession(this HttpRequest request)
        => request.HttpContext.Session.TryGetValue(SessionKeys.ClientId, out var clientId) ? clientId.ToUTF8String() : null;

    public static string? GetClientIdFromForm(this HttpRequest request)
        => request.ContentType == Application.FormUrlEncoded.DisplayName && request.Form.ContainsKey(ClientIdKey)
            ? request.Form[ClientIdKey].ToString() : null;
}
