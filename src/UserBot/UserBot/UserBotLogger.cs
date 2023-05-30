using Microsoft.Extensions.Logging;
using TL;

namespace Telegram.UserBot;

public static partial class UserBotLoggerExtensions
{
    [LoggerMessage(EventId = 0, Level = LogLevel.Information, Message = "New message from {user}: {message}")]
    public static partial void LogNewMessageFromUser(this ILogger logger, User user, string message);

    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "{me} logged in with phone number {phoneNumber}")]
    public static partial void LogLoggedIn(this ILogger logger, string phoneNumber, User me);
}
