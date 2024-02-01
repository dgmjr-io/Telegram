namespace Telegram.OpenIdConnect.Models;

using BotApiToken = Telegram.Bot.Types.BotApiToken;

internal static class TelegramOidcClientDefaults
{
    #region Defaults
    public const string DefaultCdnUriStringBase = "https://bs.cdn13.net";
    public const string DefaultLogoUriString = "https://tele.cdn13.net/logo.svg";
    public const string DefaultFaviconUriString = "https://tele.cdn13.net/logo.png";
    public const string DefaultLogoAltText = "Telegram Logo";
    public const string DefaultCopyrightHolder = "David G. Moore, Jr.";
    public const string DefaultCopyrightHolderEmail = "david@dgmjr.io";
    public const int DefaultCopyrightStartYear = 2024;
    public static readonly int? DefaultCopyrightEndYear = null;
    public const string DefaultPageTheme = "cerulean";
    public const string DefaultPageTitle = "Telegram Open ID Connect Login";
    public const string DefaultPageDescription = "Login with your Telegram account";
    public const string DefaultLogoTitle = "Telegram Login";
    #endregion
}
