namespace Telegram.OpenIdConnect.Enums;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public enum StandardScope
{
    [Description("openid")]
    [Uri("https://oidc.telegram.technology/openid")]
    OpenId,

    [Description("profile")]
    [Uri("https://oidc.telegram.technology/profile")]
    Profile,

    [Description("email")]
    [Display(
        Name = "email",
        Description = "Allows the app to read the user's email address",
        ShortName = "email"
    )]
    [Uri("https://oidc.telegram.technology/email")]
    Email,

    [Description("address")]
    [Display(
        Name = "address",
        Description = "Allows the app to read the user's address",
        ShortName = "address"
    )]
    [Uri("https://oidc.telegram.technology/address")]
    Address,

    [Description("phone")]
    [Display(
        Name = "phone",
        Description = "Allows the app to read the user's phone number",
        ShortName = "phone"
    )]
    [Uri("https://oidc.telegram.technology/phone")]
    Phone,

    [Description("offline_access")]
    [Display(
        Name = "offline_access",
        Description = "Allows the app to maintain access to resources it's been granted access to",
        ShortName = "offline_access"
    )]
    [Uri("https://oidc.telegram.technology/offline_access")]
    OfflineAccess,

    [Description("app")]
    [Display(Name = "app", Description = "App", ShortName = "app")]
    [Uri("http://[::1]")]
    App
}
