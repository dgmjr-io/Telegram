namespace Telegram.OpenIdConnect.Models.Responses;

using CorrelationId;

using Telegram.OpenIdConnect.Errors.Abstractions;

public class ErrorResponse() : Message
{
    [JIgnore]
    public IErrorType ErrorType { get; init; }

    [JProp("error")]
    public string Error => ErrorType.DisplayName;
    [JProp("error_uri")]
    public string ErrorUri => ErrorType.UriString;
    [JProp("error_description")]
    public string ErrorDescription => ErrorType.Description;
    [JProp("error_codes")]
    public string[] ErrorCodes => [ErrorType.ShortName];
    [JProp("timestamp")]
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
    [JProp("has_error")]
    public bool HasError => true;
    [JProp("is_success")]
    public bool IsSuccess => false;

    public ErrorResponse(IErrorType? errorType = default, string correlationId = null) : this()
    {
        ErrorType = errorType ?? Errors.ErrorType.ServerError.Instance;
        CorrelationId = correlationId;
    }
}
