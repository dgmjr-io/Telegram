namespace Telegram.OpenIdConnect.Client;

public class TelegramOpenIdConnectEvents : RemoteAuthenticationEvents
{
    public override Task RemoteFailure(RemoteFailureContext context)
    {
        context.Response.Redirect("/Account/Login");
        context.HandleResponse();
        return Task.CompletedTask;
    }
}
