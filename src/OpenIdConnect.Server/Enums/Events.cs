namespace Telegram.OpenIdConnect.Enums;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public enum Events
{
    [Description("http://schemas.openid.net/event/backchannel-logout")]
    BackChannelLogout
}
