namespace Telegram.OpenIdConnect.Models.Responses;

using Extensions;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Telegram.OpenIdConnect.Enums;

public static class GrantTypes
{
    public static readonly IList<string> Code = new[]
    {
        AuthorizationGrantType.Code.GetDescription()
    };

    public static readonly IList<string> Implicit = new[]
    {
        AuthorizationGrantType.Implicit.GetDescription()
    };
    public static readonly IList<string> ClientCredentials = new[]
    {
        AuthorizationGrantType.ClientCredentials.GetDescription()
    };
    public static readonly IList<string> ResourceOwnerPassword = new[]
    {
        AuthorizationGrantType.ResourceOwnerPassword.GetDescription()
    };
}
