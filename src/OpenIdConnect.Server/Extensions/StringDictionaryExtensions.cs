namespace Telegram.OpenIdConnect.Extensions;

public static class StringDictionaryExtensions
{
    public static string? Get(this IStringDictionary dictionary, string key) =>
        dictionary.TryGetValue(key, out var value) ? value : null;
}
