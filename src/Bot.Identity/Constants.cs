namespace Telegram.Bot.Identity.Constants;

public static class Schemas
{
    /// <value>tele</value>
    public const string TeleSchema = "tele";
}

public static class TableNames
{
    /// <value><inheritdoc cref="tbl_" path="/value" />Role</value>
    public const string TelegramB2CRole = tbl_ + "Role";
    /// <value><inheritdoc cref="tbl_" path="/value" />Bot</value>
    public const string Bot = tbl_ + nameof(Bot);
    /// <value><inheritdoc cref="tbl_" path="/value" />User</value>
    public const string TelegramB2CUser = tbl_ + "User";
}

public static class ExtensionProperties
{
    /// <value>telegram_id</value>
    public const string TelegramId = "telegram_id";
    /// <value>telegram_username</value>
    public const string TelegramUsername = "telegram_username";
}

public static class Uris
{
    /// <value>https://oidc.telegram.technology</value>
    public const string TelegramOidcServer = "https://oidc.telegram.technology";
}
