namespace Telegram.Bot.Identity.Constants;

public static class Schemas
{
    public const string TeleSchema = "tele";
}

public static class TableNames
{
    public const string TelegramB2CRole = tbl_ + "Role";
    public const string Bot = tbl_ + nameof(Bot);
    public const string TelegramB2CUser = tbl_ + "User";
}

public static class ExtensionProperties
{
    public const string TelegramId = "telegram_id";
    public const string TelegramUsername = "telegram_username";
}

public static class Uris
{
    public const string TelegramOidcServer = "https://oidc.telegram.technology";
}
