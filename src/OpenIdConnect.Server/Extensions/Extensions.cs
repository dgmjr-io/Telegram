using System.ComponentModel;

namespace Telegram.OpenIdConnect.Extensions;

public static class ExtensionMethods
{
    public static string GetDescription(this Enum en)
    {
        if (en == null)
            return null;

        var type = en.GetType();

        var memberInfo = type.GetMember(en.ToString());
        var description = (
            memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault()
            as DescriptionAttribute
        )?.Description;

        return description;
    }

    public static bool IsRedirectUriStartWithHttps(this string redirectUri)
    {
        return redirectUri?.StartsWith("https") == true;
    }
}
