using Duende.IdentityServer.Models;

using Telegram.OpenIdConnect.Extensions;

namespace Telegram.OpenIdConnect.Events;

public class TelegramOpenIdConnectEvents(ILogger<TelegramOpenIdConnectEvents> logger) : ILog
{
    public ILogger Logger => logger;

    public event TelegramOpenIdConnectEventHandler OnLogin;
    public event TelegramOpenIdConnectEventHandler OnLogout;
    public event TelegramOpenIdConnectEventHandler OnRedirectToTelegramLogin;
    public event TelegramOpenIdConnectEventHandler<TelegramOpenIdConnectEventArgs<TokenCreationRequest>> OnValidatingTelegramToken;
    public event TelegramOpenIdConnectEventHandler<TelegramOpenIdConnectEventArgs<Token>> OnTelegramTokenValidated;
    public event TelegramOpenIdConnectEventHandler OnClearingBotId;
    public event TelegramOpenIdConnectEventHandler OnValidatingTelegramTokenCreationRequest;

    internal async Task ValidatingTelegramTokenCreationRequest(TokenCreationRequest e)
    {
        Logger.ValidatingTelegramTokenCreationRequest(e);
        await (OnValidatingTelegramTokenCreationRequest?.Invoke(this, new TelegramOpenIdConnectEventArgs<TokenCreationRequest>(e)) ?? Task.CompletedTask);
    }

    internal async Task ClearingBotId(TelegramOpenIdConnectEventArgs e)
    {
        Logger.ClearingBotId();
        await (OnClearingBotId?.Invoke(this, e) ?? Task.CompletedTask);
    }

    internal async Task TelegramTokenValidated(TelegramOpenIdConnectEventArgs<Token> e)
    {
        Logger.TelegramTokenValidated(e.Value);
        await (OnTelegramTokenValidated?.Invoke(this, e) ?? Task.CompletedTask);
    }

    internal async Task ValidatingTelegramToken(TelegramOpenIdConnectEventArgs<TokenCreationRequest> e)
    {
        Logger.ValidatingTelegramToken(e.Value);
        await (OnValidatingTelegramToken?.Invoke(this, e) ?? Task.CompletedTask);
    }

    internal async Task RedirectToTelegramLogin(TelegramOpenIdConnectEventArgs e)
    {
        Logger.RedirectToTelegramLogin();
        await (OnRedirectToTelegramLogin?.Invoke(this, e) ?? Task.CompletedTask);
    }

    internal async Task Login(TelegramOpenIdConnectEventArgs e)
    {
        Logger.Login();
        await (OnLogin?.Invoke(this, e) ?? Task.CompletedTask);
    }
    internal async Task Logout(TelegramOpenIdConnectEventArgs e)
    {
        Logger.Logout();
        await (OnLogout?.Invoke(this, e) ?? Task.CompletedTask);
    }
}

public delegate Task TelegramOpenIdConnectEventHandler(object sender, TelegramOpenIdConnectEventArgs e);
public delegate Task TelegramOpenIdConnectEventHandler<TEventArgs>(object sender, TelegramOpenIdConnectEventArgs e)
    where TEventArgs : TelegramOpenIdConnectEventArgs;

public class TelegramOpenIdConnectEventArgs : EventArgs
{
}

public class TelegramOpenIdConnectEventArgs<T>(T value) : TelegramOpenIdConnectEventArgs
{
    public T Value => value;
}
