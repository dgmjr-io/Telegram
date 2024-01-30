namespace Telegram.OpenIdConnect.Middleware;

using System.Threading.Tasks;

using Telegram.OpenIdConnect.Models;
using Telegram.OpenIdConnect.Constants;
using Telegram.OpenIdConnect.Extensions;
using Telegram.OpenIdConnect.Options;

public class ClientSessionMiddleware : IMiddleware
{
    const string ClientIdKey = "client_id";

    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var clientId = context.Request.Query[ClientIdKey].ToString();

        if (!IsNullOrEmpty(clientId))
        {
            context.Response.Cookies.Append(SessionKeys.ClientId, clientId);
        }

        return Task.CompletedTask;
    }
}
