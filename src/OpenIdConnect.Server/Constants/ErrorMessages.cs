namespace Telegram.OpenIdConnect.Constants;

[GenerateEnumerationRecordStruct("ErrorMessage", "Telegram.OpenIdConnect.Constants")]
public enum ErrorMessages
{
    [Display(
        Name = nameof(HashValidationFailed),
        ShortName = "hash_validation_failed",
        Description = "Hash validation failed"
    )]
    HashValidationFailed = -3,

    [Display(
        Name = nameof(AuthTimeOutOfRange),
        ShortName = "auth_time_out_of_range",
        Description = "Authorization time is not without an acceptable range"
    )]
    AuthTimeOutOfRange = -2,

    [Display(
        Name = nameof(AuthTimeMissing),
        ShortName = "auth_time_missing",
        Description = "Authorization time is missing"
    )]
    AuthTimeMissing = -1,
}
