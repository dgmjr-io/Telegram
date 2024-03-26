using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.Logging;
using TL;

namespace Telegram.UserBot;

public static partial class UserBotLoggerExtensions
{
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Information,
        Message = "New message from {User}: {Message}"
    )]
    public static partial void NewMessageFromUser(
        this ILogger logger,
        User user,
        TL.Message message
    );

    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Information,
        Message = "{Me} logged in with phone number {PhoneNumber}"
    )]
    public static partial void LoggedIn(this ILogger logger, string phoneNumber, User me);

    [LoggerMessage(
        EventId = 2,
        Level = LogLevel.Information,
        Message = "Chat {ChatId} has {Count} participants"
    )]
    public static partial void ChatParticipants(this ILogger logger, long chatId, int count);

    [LoggerMessage(
        EventId = 3,
        Level = LogLevel.Information,
        Message = "Chat {Chat} has {Count} messages deleted"
    )]
    public static partial void ChatMessagesDeleted(this ILogger logger, ChatBase chat, int count);

    [LoggerMessage(
        EventId = 4,
        Level = LogLevel.Information,
        Message = "{User} is {Action}"
    )]
    public static partial void UserAction(this ILogger logger, User user, string action);

    [LoggerMessage(
        EventId = 5,
        Level = LogLevel.Information,
        Message = "{User} is typing in {Chat}"
    )]
    public static partial void ChatUserTyping(this ILogger logger, User user, ChatBase chat);

    [LoggerMessage(
        EventId = 6,
        Level = LogLevel.Information,
        Message = "{Number} message(s) deleted"
    )]
    public static partial void MessagesDeleted(this ILogger logger, int number);

    [LoggerMessage(
        EventId = 7,
        Level = LogLevel.Information,
        Message = "{User} has changed profile name: {FirstName} {LastName}"
    )]
    public static partial void UserChangedProfileName(
        this ILogger logger,
        User user,
        string firstName,
        string lastName
    );

    [LoggerMessage(
        EventId = 8,
        Level = LogLevel.Information,
        Message = "{User} is now {Status}"
    )]
    public static partial void UserStatus(this ILogger logger, User user, UserStatus status);

    [LoggerMessage(
        EventId = 9,
        Level = LogLevel.Information,
        Message = "{User} has changed infos/photo"
    )]
    public static partial void UserChangedInfos(this ILogger logger, User user);

    [LoggerMessage(
        EventId = 10,
        Level = LogLevel.Information,
        Message = "Unhandled update: {Update}"
    )]
    public static partial void UnhandledUpdate(this ILogger logger, IObject update);

    [LoggerMessage(EventId = 11, Level = LogLevel.Information, Message = "{Length} participants in {Chat}")]
    public static partial void ChatParticipants(this ILogger logger, int length, Chat chat);

    [LoggerMessage(
        EventId = 12,
        Level = LogLevel.Information,
        Message = "{From} in {Where}> {Message}"
    )]
    public static partial void MessageReceived(this ILogger logger, Message message, string from, string where);

    [LoggerMessage(
        EventId = 13,
        Level = LogLevel.Information,
        Message = "{From} in {Where} [{Action}]"
    )]
    public static partial void UserAction(this ILogger logger, MessageBase message, string from, string where, string action);

    [LoggerMessage(
        EventId = 14,
        Level = LogLevel.Information,
        Message = "Group call {CallId} has {ParticipantsCount} participants"
    )]
    public static partial void GroupCallParticipants(this ILogger logger, long callId, int participantsCount);
}
