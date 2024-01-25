using System.ComponentModel;

namespace Telegram.OpenIdConnect.Enums;

public enum OidcResponseType
{
    [Description("")]
    None = 0,

    [Description("code")]
    Code,

    [Description("token")]
    Token,

    [Description("id_token token")]
    IdToken,

    [Description("code id_token")]
    CodeIdToken,

    [Description("code token")]
    CodeToken,

    [Description("id_token token")]
    IdTokenToken,

    [Description("code id_token token")]
    CodeIdTokenToken
}
