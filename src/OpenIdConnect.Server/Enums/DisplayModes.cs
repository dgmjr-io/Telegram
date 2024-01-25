namespace Telegram.OpenIdConnect.Enums;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public enum DisplayMode
{
    [Description("page")]
    Page,

    [Description("popup")]
    Popup,

    [Description("touch")]
    Touch,

    [Description("wap")]
    Wap
}
