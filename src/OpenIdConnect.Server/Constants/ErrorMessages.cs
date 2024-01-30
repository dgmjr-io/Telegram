namespace Telegram.OpenIdConnect.Constants;

[GenerateEnumerationRecordStruct("ErrorMessage", "Telegram.OpenIdConnect.Constants")]
public enum ErrorMessages
{
    [Display(
        Name = nameof(HashValidationFailed),
        ShortName = "hash_validation_failed",
        Description = "Verification failed; the value of the hash is invalid."
    )]
    HashValidationFailed = -3,

    [Display(
        Name = nameof(AuthTimeOutOfRange),
        ShortName = "auth_time_out_of_range",
        Description = "Verification failed; auth_time is not within an acceptable range."
    )]
    AuthTimeOutOfRange = -2,

    [Display(
        Name = nameof(AuthTimeMissing),
        ShortName = "auth_time_missing",
        Description = "Verification failed; auth_time is missing."
    )]
    AuthTimeMissing = -1,
}
