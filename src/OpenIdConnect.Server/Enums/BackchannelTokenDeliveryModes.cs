namespace Telegram.OpenIdConnect.Enums;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public enum BackchannelTokenDeliveryMode
{
    [Description("poll")]
    Poll,

    [Description("ping")]
    Ping,

    [Description("push")]
    Push
}
