namespace Telegram.OpenIdConnect.Enums;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public enum CodeChallengeMethod
{
    [Description("plain")]
    Plain,

    [Description("S256")]
    Sha256
}
