namespace Telegram.OpenIdConnect.Extensions;

using Telegram.OpenIdConnect.Models.Requests;

using static Microsoft.Extensions.Logging.LogLevel;

public static partial class LoggingExtensions
{
    [LoggerMessage(1, Information, "Authorization request received: {Request}")]
    public static partial void AuthorizationRequestReceived(this ILogger logger, string request);

    [LoggerMessage(
        2,
        Information,
        "Authorization request validated: \nCorrelationId: {CorrelationId}\nClient ID: {ClientId}"
    )]
    public static partial void AuthorizationRequestValidated(
        this ILogger logger,
        string correlationId,
        string clientId
    );

    [LoggerMessage(
        3,
        Warning,
        "Authorization request rejected: \n{CorrelationId}\nClient ID: {ClientId}\nReason:{Reason}"
    )]
    public static partial void AuthorizationRequestRejected(
        this ILogger logger,
        string correlationId,
        string clientId,
        string reason
    );
}
