namespace Telegram.OpenIdConnect.Models;

using BotApiToken = Telegram.Bot.Types.BotApiToken;
using static TelegramOidcClientDefaults;

public class TelegramOidcClient : Duende.IdentityServer.Models.Client
{
    #region Required Bot Info
    [Required, RegularExpression(BotApiToken.RegexString)]
    public BotApiToken BotApiToken
    {
        get => BotApiToken.From(ClientSecrets.FirstOrDefault()?.Value);
        set => ClientSecrets = new[] { new Duende.IdentityServer.Models.Secret(value.ToString()) };
    }

    [Required]
    public string BotUsername
    {
        get => ClientId;
        set => ClientId = value;
    }

    [Required]
    public string BotName
    {
        get => ClientName ?? BotUsername;
        set => ClientName = value;
    }
    #endregion

    #region Optional Look-and-Feel Info
    public string PageTitle { get; set; } = DefaultPageTitle;

    public string PageDescription { get; set; } = DefaultPageDescription;

    public Uri PageIcon { get; set; } = new(DefaultLogoUriString);

    /// <summary>
    /// Select one of the following or an entirely new one
    /// -  cerulean
    /// -  cosmo
    /// -  cyborg
    /// -  darkly
    /// -  flatly
    /// -  journal
    /// -  litera
    /// -  lumen
    /// -  lux
    /// -  materia
    /// -  minty
    /// -  morph
    /// -  pulse
    /// -  quartz
    /// -  sandstone
    /// -  simplex
    /// -  sketchy
    /// -  slate
    /// -  solar
    /// -  spacelab
    /// -  superhero
    /// -  united
    /// -  vapor
    /// -  yeti
    /// -  zephyr
    /// </summary>
    public string PageTheme { get; set; } = DefaultPageTheme;
    public bool Dark { get; set; } = true;
    public string CopyrightHolder { get; set; } = DefaultCopyrightHolder;
    public string CopyrightHolderEmail { get; set; } = DefaultCopyrightHolderEmail;
    public int? CopyrightStartYear { get; set; } = DefaultCopyrightStartYear;
    public int? CopyrightEndYear { get; set; } = DefaultCopyrightEndYear;
    public string LogoAltText { get; set; } = DefaultLogoAltText;
    public string LogoTitle { get; set; } = DefaultLogoTitle;
    public string Title { get; set; } = DefaultPageTitle;
    public Uri CdnUriBase { get; set; } = new(DefaultCdnUriStringBase);
    public Uri FaviconUri { get; set; } = new(DefaultFaviconUriString);
    public new Uri LogoUri { get => new(base.LogoUri ?? DefaultLogoUriString); set => base.LogoUri = value.ToString(); }

    [JsonConverter(typeof(JsonStringEnumConverter<Dgmjr.AspNetCore.TagHelpers.Enumerations.Theme>))]
    public Dgmjr.AspNetCore.TagHelpers.Enumerations.Theme Theme => Dark ? Dgmjr.AspNetCore.TagHelpers.Enumerations.Theme.Dark : Dgmjr.AspNetCore.TagHelpers.Enumerations.Theme.Light;

    #endregion
}
