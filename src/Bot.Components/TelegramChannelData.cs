namespace Telegram.Bot.Components;

using Newtonsoft.Json;

public partial class TelegramChannelData
{
    [JsonProperty("id")]
    [JProp("id")]
    public string Id { get; set; }

    [JsonProperty("from")]
    [JProp("from")]
    public TelegramChannelDataFrom From { get; set; }

    [JsonProperty("message")]
    [JProp("message")]
    public Message Message { get; set; }

    [JsonProperty("chat_instance")]
    [JProp("chat_instance")]
    public string ChatInstance { get; set; }

    [JsonProperty("data")]
    [JProp("data")]
    public string Data { get; set; }
}

public partial class TelegramChannelDataFrom
{
    [JsonProperty("id")]
    [JProp("id")]
    public long Id { get; set; }

    [JsonProperty("is_bot")]
    [JProp("is_bot")]
    public bool IsBot { get; set; }

    [JsonProperty("first_name")]
    [JProp("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    [JProp("last_name")]
    public string LastName { get; set; }

    [JsonProperty("username")]
    [JProp("username")]
    public string Username { get; set; }

    [JsonProperty("language_code")]
    [JProp("language_code")]
    public string LanguageCode { get; set; }
}

public partial class Message
{
    [JsonProperty("message_id")]
    [JProp("message_id")]
    public long MessageId { get; set; }

    [JsonProperty("from")]
    [JProp("from")]
    public MessageFrom From { get; set; }

    [JsonProperty("chat")]
    [JProp("chat")]
    public Chat Chat { get; set; }

    [JsonProperty("date")]
    [JProp("date")]
    public long Date { get; set; }

    [JsonProperty("text")]
    [JProp("text")]
    public string Text { get; set; }
}

public partial class Chat
{
    [JsonProperty("id")]
    [JProp("id")]
    public long Id { get; set; }

    [JsonProperty("is_bot")]
    [JProp("is_bot")]
    public bool IsBot { get; set; }

    [JsonProperty("is_premium")]
    [JProp("is_premium")]
    public bool IsPremium { get; set; }

    [JsonProperty("language")]
    [JProp("language")]
    public bool LanguageCode { get; set; }

    [JsonProperty("first_name")]
    [JProp("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    [JProp("last_name")]
    public string LastName { get; set; }

    [JsonProperty("username")]
    [JProp("username")]
    public string Username { get; set; }

    [JsonProperty("type")]
    [JProp("type")]
    public string Type { get; set; }
}

public partial class MessageFrom
{
    [JsonProperty("id")]
    [JProp("id")]
    public long Id { get; set; }

    [JsonProperty("is_bot")]
    [JProp("is_bot")]
    public bool IsBot { get; set; }

    [JsonProperty("is_premium")]
    [JProp("is_premium")]
    public bool IsPremium { get; set; }

    [JsonProperty("language_code")]
    [JProp("language_code")]
    public string LanguageCode { get; set; }

    [JsonProperty("first_name")]
    [JProp("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    [JProp("last_name")]
    public string LastName { get; set; }

    [JsonProperty("username")]
    [JProp("username")]
    public string Username { get; set; }
}
