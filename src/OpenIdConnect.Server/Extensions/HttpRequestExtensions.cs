namespace Telegram.OpenIdConnect.Extensions;
using Constants;
using Options;
using Models;

public static partial class HttpRequestExtensions
{
    private const string ClientIdKey = "client_id";

    public static TelegramOpenIdConnectServerOptions? GetOptions(this HttpRequest request)
        => request.HttpContext.RequestServices.GetService<IOptionsMonitor<TelegramOpenIdConnectServerOptions>>().CurrentValue;

    public static TelegramOidcClient[] GetClients(this HttpRequest request)
        => [..request.GetOptions().Clients];

    public static TelegramOidcClient? GetClient(this HttpRequest request)
        => request.GetOptions().Clients[request.GetClientId()] ??
                request.GetOptions().Clients.FirstOrDefault();

    public static string? GetClientId(this HttpRequest request)
        => !IsNullOrEmpty(request.GetClientIdFromQuery()) ? request.GetClientIdFromQuery() :
            !IsNullOrEmpty(request.GetClientIdFromReturnUrl()) ? request.GetClientIdFromReturnUrl() :
            !IsNullOrEmpty(request.GetClientIdFromCookies()) ? request.GetClientIdFromCookies() :
            !IsNullOrEmpty(request.GetClientIdFromForm()) ? request.GetClientIdFromForm() : null;

    public static string? GetClientIdFromQuery(this HttpRequest request)
        => request.Query[ClientIdKey];

    [GeneratedRegex("client_id=(?<client_id>[^&]+)")]
    public static partial Regex ClientIdFromReturnUrlRegex ();

    public static string? GetClientIdFromReturnUrl(this HttpRequest request)
        => !IsNullOrEmpty(request.Query["ReturnUrl"].FirstOrDefault()) ?
            ClientIdFromReturnUrlRegex().Match(request.Query["ReturnUrl"]).Groups[ClientIdKey]?.Value :
            null;

    public static string? GetClientIdFromCookies(this HttpRequest request)
        => request.Cookies[SessionKeys.ClientId];

    public static string? GetClientIdFromForm(this HttpRequest request)
        => request.ContentType == Application.FormUrlEncoded.DisplayName && request.Form.ContainsKey(ClientIdKey)
            ? request.Form[ClientIdKey].ToString() : null;

        // !IsNullOrEmpty(HttpContext.Request.Query[ClientIdKey])
        //     ? HttpContext.Request.Query[ClientIdKey] :
        //         !IsNullOrEmpty(ClientIdFromReturnUrl) ? ClientIdFromReturnUrl :
        //             HttpContext.Request.ContentType == Application.FormUrlEncoded.DisplayName && !IsNullOrEmpty(HttpContext.Request.Form[ClientIdKey])
        //                 ? HttpContext.Request.Form[ClientIdKey]
        //                 : !IsNullOrEmpty(HttpContext.Request.Cookies[SessionKeys.ClientId])
        //                     ? HttpContext.Request.Cookies[SessionKeys.ClientId]
        //                     : Options.Clients.BotNames.FirstOrDefault();
}
