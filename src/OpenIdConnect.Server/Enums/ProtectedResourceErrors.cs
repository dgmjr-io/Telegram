namespace Telegram.OpenIdConnect.Enums;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public enum ProtectedResourceError
{
    [Description("invalid_token")]
    InvalidToken,

    [Description("expired_token")]
    ExpiredToken,

    [Description("invalid_request")]
    InvalidRequest,

    [Description("insufficient_scope")]
    InsufficientScope
}
