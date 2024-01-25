namespace Telegram.OpenIdConnect.Extensions;

using System.Web;

public static class UriBuilderExtensions
{
    public static UriBuilder AddQuery(this UriBuilder uriBuilder, string name, string value)
    {
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query.Add(name, value);
        uriBuilder.Query = query.ToString();
        return uriBuilder;
    }
}
