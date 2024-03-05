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
}
