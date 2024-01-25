namespace Telegram.OpenIdConnect.Enums;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public enum HttpHeaders
{
    [Description("DPoP")]
    DPoP,

    [Description("DPoP-Nonce")]
    DPoPNonce
}
