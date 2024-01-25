namespace Telegram.OpenIdConnect.Options;

public partial class TelegramOpenIdConnectServerOptions
{
    private const string DefaultCdnUriStringBase = "https://bs.cdn13.net";
    private const string DefaultLogoUriString =
        "https://upload.wikimedia.org/wikipedia/commons/8/82/Telegram_logo.svg";
    private const string DefaultFaviconUriString =
        "https://upload.wikimedia.org/wikipedia/commons/8/82/Telegram_logo.svg";

    public Uri LogoUri { get; set; } = new Uri(DefaultLogoUriString);
    public Uri FaviconUri { get; set; } = new Uri(DefaultFaviconUriString);
    public string LogoAlt { get; set; } = "Telegram Logo";
    public string LogoTitle { get; set; } = "Telegram Login";
    public string Title { get; set; } = "Welcome!";
    public Uri CdnUriBase { get; set; } = new(DefaultCdnUriStringBase);

    [JsonConverter(typeof(EnumToStringConverter<Dgmjr.AspNetCore.TagHelpers.Enumerations.Theme>))]
    public Dgmjr.AspNetCore.TagHelpers.Enumerations.Theme Theme { get; set; } =
        Dgmjr.AspNetCore.TagHelpers.Enumerations.Theme.Dark;

    public int? CopyrightStartYear { get; set; } = datetime.Now.Year;

    public int? CopyrightEndYear { get; set; } = datetime.Now.Year;

    public string CopyrightHolder { get; set; }

    public string CopyrightHolderEmail { get; set; }

    public string RedirectUri { get; set; }

    /// <summary>
    ///
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
    public string ColorTheme { get; set; } = "cerulean";
}
