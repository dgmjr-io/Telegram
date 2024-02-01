namespace Telegram.OpenIdConnect.Options;

public partial class TelegramOpenIdConnectServerOptions
{
    public int? CopyrightStartYear { get; set; } = datetime.Now.Year;

    public int? CopyrightEndYear { get; set; } = datetime.Now.Year;

    public string CopyrightHolder { get; set; }

    public string CopyrightHolderEmail { get; set; }

    public string RedirectUri { get; set; }

    // /// <summary>
    // ///
    // /// Select one of the following or an entirely new one
    // /// -  cerulean
    // /// -  cosmo
    // /// -  cyborg
    // /// -  darkly
    // /// -  flatly
    // /// -  journal
    // /// -  litera
    // /// -  lumen
    // /// -  lux
    // /// -  materia
    // /// -  minty
    // /// -  morph
    // /// -  pulse
    // /// -  quartz
    // /// -  sandstone
    // /// -  simplex
    // /// -  sketchy
    // /// -  slate
    // /// -  solar
    // /// -  spacelab
    // /// -  superhero
    // /// -  united
    // /// -  vapor
    // /// -  yeti
    // /// -  zephyr
    // /// </summary>
    // public string ColorTheme { get; set; } = "cerulean";
}
