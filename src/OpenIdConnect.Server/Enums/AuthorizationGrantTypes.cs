using System.ComponentModel;

namespace Telegram.OpenIdConnect.Enums;

internal enum AuthorizationGrantType : byte
{
    [Description("code")]
    Code,

    [Description("Implicit")]
    Implicit,

    [Description("ClientCredentials")]
    ClientCredentials,

    [Description("ResourceOwnerPassword")]
    ResourceOwnerPassword
}
