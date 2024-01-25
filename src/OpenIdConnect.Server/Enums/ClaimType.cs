namespace Telegram.OpenIdConnect.Enums;

public enum ClaimType
{
    [Description("normal")]
    Normal,

    // [Description("essential")]
    // Essential,

    // [Description("id_token")]
    // IdToken,

    // [Description("userinfo")]
    // UserInfo,,

    [Description("aggregated")]
    Aggregated,

    [Description("distributed")]
    Distributed
}
