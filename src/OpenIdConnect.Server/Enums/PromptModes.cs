namespace Telegram.OpenIdConnect.Enums;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public enum PromptMode
{
    [Description("none")]
    None,

    [Description("login")]
    Login,

    [Description("consent")]
    Consent,

    [Description("select_account")]
    SelectAccount,

    [Description("create")]
    Create
}
