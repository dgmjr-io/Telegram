namespace Telegram.OpenIdConnect.Extensions;

using Duende.IdentityServer.Validation;

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

    [LoggerMessage(
        4,
        Information,
        "Request requires interaction; redirecting to the Telegram login page...",
        EventName = nameof(RequestRequiresInteraction)
    )]
    public static partial void RequestRequiresInteraction(this ILogger logger);

    [LoggerMessage(
        4,
        Information,
        "Authorize request is valid.",
        EventName = nameof(ValidAuthorizeRequest)
    )]
    public static partial void ValidAuthorizeRequest(this ILogger logger);

    public static void InvalidGrantTypeForClient(
        this ILogger logger,
        string grantType,
        IEnumerable<string> validGrantTypes
    ) =>
        logger.InvalidThingForClient(
            "grant type",
            grantType,
            validGrantTypes.ToSpaceSeparatedString()
        );

    [LoggerMessage(
        5,
        Error,
        """
        Invalid {ThingType} "{Thing}" for client; valid grant types: {ValidThings}
        """,
        EventName = nameof(InvalidThingForClient)
    )]
    public static partial void InvalidThingForClient(
        this ILogger logger,
        string thingType,
        string thing,
        string validThings
    );

    public static void InvalidThingForClient(
        this ILogger logger,
        string thingType,
        string thing,
        IEnumerable<string> validThings
    ) => logger.InvalidThingForClient(thingType, thing, validThings.ToSpaceSeparatedString());

    [LoggerMessage(
        6,
        Error,
        """
        {Message}: {Detail}
        {RequestDetails}
        """,
        EventName = nameof(InvalidRequest)
    )]
    public static partial void InvalidRequest(
        this ILogger logger,
        string message,
        string detail,
        ValidatedAuthorizeRequest requestDetails
    );

    [LoggerMessage(
        7,
        Error,
        """
        {Message}
        {RequestDetails}
        """,
        EventName = nameof(InvalidRequest)
    )]
    public static partial void InvalidRequest(
        this ILogger logger,
        string message,
        ValidatedAuthorizeRequest requestDetails
    );

    [LoggerMessage(
        8,
        Information,
        """
        Received validated authorize request:
        {RequestDetails}
        """,
        EventName = nameof(ReceivedValidatedAuthorizedRequest)
    )]
    public static partial void ReceivedValidatedAuthorizedRequest(
        this ILogger logger,
        ValidatedAuthorizeRequest requestDetails
    );

    [LoggerMessage(
        8,
        Error,
        """
        Invalid Telegram tokens received:
        {RequestDetails}
        """,
        EventName = nameof(ReceivedValidatedAuthorizedRequest)
    )]
    public static partial void InvalidTelegramTokens(
        this ILogger logger,
        ValidatedAuthorizeRequest requestDetails
    );

    [LoggerMessage(
        Error,
        """
        {Message} ({Code}):
        {RequestDetails}
        """
    )]
    public static partial void TelegramAuthenticationError(
        this ILogger logger,
        string code,
        string message,
        ValidatedAuthorizeRequest requestDetails
    );

    public static void TelegramAuthenticationError(
        this ILogger logger,
        Constants.Abstractions.IErrorMessage errorMessage,
        ValidatedAuthorizeRequest requestDetails
    )
    {
        logger.TelegramAuthenticationError(
            errorMessage.ShortName,
            errorMessage.Description,
            requestDetails
        );
    }

    [LoggerMessage(9, Debug, "Starting access token creation...")]
    public static partial void StartingAccessTokenCreation(this ILogger logger);

    [LoggerMessage(13, Debug, "Finished creating access token.")]
    public static partial void FinishedCreatingAccessToken(this ILogger logger);

    [LoggerMessage(10, Debug, "Starting ID token creation...")]
    public static partial void StartingIdTokenCreation(this ILogger logger);

    [LoggerMessage(12, Debug, "Finished creating access token.")]
    public static partial void FinishedCreatingIdToken(this ILogger logger);

    [LoggerMessage(11, Debug, "Retrieving claims from {principal}")]
    public static partial void RetrievingClaimsFromPrincipal(this ILogger logger, string principal);

    public static void RetrievingClaimsFromPrincipal(
        this ILogger logger,
        ClaimsPrincipal principal
    ) =>
        logger.RetrievingClaimsFromPrincipal(
            Serialize(
                principal.Claims
                    .Select(c => c.Type)
                    .Distinct()
                    .ToDictionary(t => t, t => principal.FindFirstValue(t))
            )
        );

    [LoggerMessage(14, Debug, "Creating JWT access token...")]
    public static partial void CreatingJwtAccessToken(this ILogger logger);

    [LoggerMessage(15, Debug, "Creating reference access token...")]
    public static partial void CreatingReferenceAccessToken(this ILogger logger);

    [LoggerMessage(15, Debug, "Creating JWT identity token...")]
    public static partial void CreatingJwtIdentityToken(this ILogger logger);

    // {
    //     logger.AuthorizationRequestReceived(request.ToString());
    //     logger.AuthorizationRequestValidated(request...CorrelationId, request.ClientId);
    //     logger.ValidAuthorizeRequest();
    // }
}
