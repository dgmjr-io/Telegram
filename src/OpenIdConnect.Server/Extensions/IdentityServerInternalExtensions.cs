using System.Collections.Specialized;
using System.Text.Encodings.Web;

using Duende.IdentityServer.Extensions;

using Microsoft.AspNetCore.WebUtilities;
using ClaimsPrincipal = System.Security.Claims.ClaimsPrincipal;
using Uri = System.Uri;

namespace Telegram.OpenIdConnect.Extensions;

internal static class IdentityServerInternalStringExtensions
{
    [DebuggerStepThrough]
    public static string ToSpaceSeparatedString(this IEnumerable<string> list)
    {
        if (list == null)
        {
            return string.Empty;
        }

        var sb = new StringBuilder(100);

        foreach (var element in list)
        {
            sb.Append(element + " ");
        }

        return sb.ToString().Trim();
    }

    [DebuggerStepThrough]
    public static IEnumerable<string> FromSpaceSeparatedString(this string input)
    {
        input = input.Trim();
        return input.Split(Separator, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    internal static readonly char[] Separator = [' '];

    public static List<string>? ParseScopesString(this string scopes)
    {
        if (scopes.IsMissing())
        {
            return null;
        }

        scopes = scopes.Trim();
        var parsedScopes = scopes
            .Split(Separator, StringSplitOptions.RemoveEmptyEntries)
            .Distinct()
            .ToList();

        if (parsedScopes.Any())
        {
            parsedScopes.Sort();
            return parsedScopes;
        }

        return null;
    }

    // [DebuggerStepThrough]
    // public static bool IsMissing(this string value)
    // {
    //     return IsNullOrWhiteSpace(value);
    // }

    // [DebuggerStepThrough]
    // public static bool IsMissingOrTooLong(this string value, int maxLength)
    // {
    //     return IsNullOrWhiteSpace(value) || value.Length > maxLength;
    // }

    // [DebuggerStepThrough]
    // public static bool IsPresent(this string value)
    // {
    //     return !IsNullOrWhiteSpace(value);
    // }

    // [DebuggerStepThrough]
    // public static string? EnsureLeadingSlash(this string url)
    // {
    //     return url?.StartsWith('/') == false ? "/" + url : url;
    // }

    // [DebuggerStepThrough]
    // public static string? EnsureTrailingSlash(this string url)
    // {
    //     return url?.EndsWith('/') == false ? url + "/" : url;
    // }

    // [DebuggerStepThrough]
    // public static string? RemoveLeadingSlash(this string url)
    // {
    //     if (url?.StartsWith('/') == true)
    //     {
    //         url = url[1..];
    //     }

    //     return url;
    // }

    // [DebuggerStepThrough]
    // public static string? RemoveTrailingSlash(this string url)
    // {
    //     if (url?.EndsWith('/') == true)
    //     {
    //         url = url[..^1];
    //     }

    //     return url;
    // }

    // [DebuggerStepThrough]
    // public static string CleanUrlPath(this string url)
    // {
    //     if (IsNullOrWhiteSpace(url))
    //         url = "/";

    //     if (url != "/" && url.EndsWith('/'))
    //     {
    //         url = url[..^1];
    //     }

    //     return url;
    // }

    // [DebuggerStepThrough]
    // public static bool IsLocalUrl(this string url)
    // {
    //     if (IsNullOrEmpty(url))
    //     {
    //         return false;
    //     }

    //     // Allows "/" or "/foo" but not "//" or "/\".
    //     if (url[0] == '/')
    //     {
    //         // url is exactly "/"
    //         if (url.Length == 1)
    //         {
    //             return true;
    //         }

    //         // url doesn't start with "//" or "/\"
    //         return url[1] != '/' && url[1] != '\\';
    //     }

    //     // Allows "~/" or "~/foo" but not "~//" or "~/\".
    //     if (url[0] == '~' && url.Length > 1 && url[1] == '/')
    //     {
    //         // url is exactly "~/"
    //         if (url.Length == 2)
    //         {
    //             return true;
    //         }

    //         // url doesn't start with "~//" or "~/\"
    //         return url[2] != '/' && url[2] != '\\';
    //     }

    //     return false;
    // }

    // [DebuggerStepThrough]
    // public static string AddQueryString(this string url, string query)
    // {
    //     if (!url.Contains('?'))
    //     {
    //         url += "?";
    //     }
    //     else if (!url.EndsWith('&'))
    //     {
    //         url += "&";
    //     }

    //     return url + query;
    // }

    // [DebuggerStepThrough]
    // public static string AddQueryString(this string url, string name, string value)
    // {
    //     return url.AddQueryString(name + "=" + UrlEncoder.Default.Encode(value));
    // }

    // [DebuggerStepThrough]
    // public static string AddHashFragment(this string url, string query)
    // {
    //     if (!url.Contains('#'))
    //     {
    //         url += "#";
    //     }

    //     return url + query;
    // }

    // [DebuggerStepThrough]
    // public static NameValueCollection ReadQueryStringAsNameValueCollection(this string url)
    // {
    //     if (url != null)
    //     {
    //         var idx = url.IndexOf('?');
    //         if (idx >= 0)
    //         {
    //             url = url[(idx + 1)..];
    //         }
    //         var query = QueryHelpers.ParseNullableQuery(url);
    //         if (query != null)
    //         {
    //             return query.AsNameValueCollection();
    //         }
    //     }

    //     return [];
    // }

    // public static string? GetOrigin(this string url)
    // {
    //     if (url != null)
    //     {
    //         Uri uri;
    //         try
    //         {
    //             uri = new Uri(url);
    //         }
    //         catch (Exception)
    //         {
    //             return null;
    //         }

    //         if (uri.Scheme == "http" || uri.Scheme == "https")
    //         {
    //             return $"{uri.Scheme}://{uri.Authority}";
    //         }
    //     }

    //     return null;
    // }

    // public static string Obfuscate(this string value)
    // {
    //     var last4Chars = "****";
    //     if (value.IsPresent() && value.Length > 4)
    //     {
    //         last4Chars = value[^4..];
    //     }

    //     return "****" + last4Chars;
    // }

    // public static bool IsUri(this string value)
    // {
    //     return Uri.TryCreate(value, Absolute, out _);
    // }
}
