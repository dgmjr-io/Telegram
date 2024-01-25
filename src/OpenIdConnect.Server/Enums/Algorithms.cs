namespace Telegram.OpenIdConnect.Enums;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public enum Algorithm
{
    [Description("none")]
    None = 0,

    [Description("HS256")]
    HS256 = 1,

    [Description("HS384")]
    HS384 = 2,

    [Description("HS512")]
    HS512 = 3,

    [Description("RS256")]
    RS256 = 4,

    [Description("RS384")]
    RS38 = 5,

    [Description("RS512")]
    RS512 = 6,

    [Description("ES256")]
    ES256 = 7,

    [Description("ES384")]
    ES384 = 8,

    [Description("ES512")]
    ES512 = 9,

    [Description("PS256")]
    PS256 = 10,

    [Description("PS384")]
    PS384 = 11,

    [Description("PS512")]
    PS512 = 12,

    [Description("RSA1_5")]
    RSA1_5 = 13,

    [Description("A128KW")]
    A128KW = 14,

    [Description("A128CBC-HS256")]
    A128CBC_HS256 = 15,

    [Description("A128GCM")]
    A128GCM = 16,
}
