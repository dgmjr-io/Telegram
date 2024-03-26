namespace Telegram.UserBot;
using Microsoft.Extensions.Logging;

public static partial class LoggingExtensions
{
    [LoggerMessage(0, LogLevel.Information, "Chat {ChatId} has {ParticipantsCount} participants")]
    public static partial void ChatParticipants2(this ILogger logger, long chatId, int participantsCount);

    [LoggerMessage(1, LogLevel.Information, "Channel {ChannelId} {ChannelTitle} has {ParticipantsCount} participants")]
    public static partial void ChannelParticipants(this ILogger logger, long channelId, string channelTitle, int participantsCount);

    [LoggerMessage(3, LogLevel.Information, "User {UserId} is in the chat")]
    public static partial void UserInChat(this ILogger logger, long userId);

    [LoggerMessage(4, LogLevel.Information, "User {UserId} is the owner '{Rank}'")]
    public static partial void CreatorInChat(this ILogger logger, long userId, string rank);

    [LoggerMessage(5, LogLevel.Information, "User {UserId} is admin '{Rank}'")]
    public static partial void AdminInChat(this ILogger logger, long userId, string rank);

    [LoggerMessage(6, LogLevel.Information, "User {UserId} is bot")]
    public static partial void BotInChat(this ILogger logger, long userId);
}
