namespace Telegram.OpenIdConnect.Extensions;

public static class DateTimeOffsetExtensions
{
    public static string ToUnixTimeSecondsString(this DateTimeOffset dto) =>
        dto.ToUnixTimeSeconds().ToString();
}
