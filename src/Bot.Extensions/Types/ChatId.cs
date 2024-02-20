namespace Telegram.Bot.Types.Extensions;

using System.Net.Http.Headers;

using OneOf;

public readonly record struct ChatId
{
    public ChatId(string username) => Username = username;

    public ChatId(long id) => Id = id;

    public long? Id { get; }
    public string? Username { get; }

    public static implicit operator ChatId(long value) => new (value);
    public static implicit operator ChatId(string value) => new (value);
}
