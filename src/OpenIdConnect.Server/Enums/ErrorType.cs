using System.ComponentModel;

namespace Telegram.OpenIdConnect.Enums;

[GenerateEnumerationRecordStruct("ErrorType", "Telegram.OpenIdConnect.Errors")]
public enum ErrorType : byte
{
    [Description("invalid_request")]
    [Display(
        Name = "Invalid Request",
        ShortName = "invalid_request",
        Description = "The request is missing a required parameter, includes an invalid parameter value, includes a parameter more than once, or is otherwise malformed."
    )]
    [Uri("https://tools.ietf.org/html/rfc6749#/section-5.2/invalid_request")]
    InvalidRequest,

    [Description("unauthorized_client")]
    [Display(
        Name = "Unauthorized Client",
        ShortName = "unauthorized_client",
        Description = "The client is not authorized to request an authorization code using this method."
    )]
    [Uri("https://tools.ietf.org/html/rfc6749#/section-5.2/unauthorized_client")]
    UnauthorizedClient,

    [Description("access_denied")]
    [Display(
        Name = "Access Denied",
        ShortName = "access_denied",
        Description = "The resource owner or authorization server denied the request"
    )]
    [Uri("https://tools.ietf.org/html/rfc6749#/section-5.2/access_denied")]
    AccessDenied,

    [Description("unsupported_response_type")]
    [Display(
        Name = "Unsupported Response Type",
        ShortName = "unsupported_response_type",
        Description = "The authorization server does not support obtaining an authorization code using this method."
    )]
    [Uri("https://tools.ietf.org/html/rfc6749#/section-5.2/unsupported_response_type")]
    UnsupportedResponseType,

    [Description("invalid_scope")]
    [Display(
        Name = "Invalid Scope",
        ShortName = "invalid_scope",
        Description = "The requested scope is invalid, unknown, or malformed."
    )]
    [Uri("https://tools.ietf.org/html/rfc6749#/section-5.2/invalid_scope")]
    InvalidScope,

    [Description("server_error")]
    [Display(
        Name = "Server Error",
        ShortName = "server_error",
        Description = "The authorization server encountered an unexpected condition that prevented it from fulfilling the request."
    )]
    [Uri("https://tools.ietf.org/html/rfc6749#/section-5.2/server_error")]
    ServerError,

    [Description("temporarily_unavailable")]
    [Display(
        Name = "Temporarily Unavailable",
        ShortName = "temporarily_unavailable",
        Description = "The authorization server is currently unable to handle the request due to a temporary overloading or maintenance of the server."
    )]
    [Uri("https://tools.ietf.org/html/rfc6749#/section-5.2/temporarily_unavailable")]
    TemporarilyUnavailable,

    [Description("invalid_client")]
    [Display(
        Name = "Invalid Client",
        ShortName = "invalid_client",
        Description = "Client authentication failed (e.g., unknown client, no client authentication included, or unsupported authentication method)."
    )]
    [Uri("https://tools.ietf.org/html/rfc6749#/section-5.2/invalid_client")]
    InvalidClient,

    [Description("invalid_grant")]
    [Display(
        Name = "Invalid Grant",
        ShortName = "invalid_grant",
        Description = "The provided authorization grant (e.g., authorization code, resource owner credentials) or refresh token is invalid, expired, revoked, does not match the redirection URI used in the authorization request, or was issued to another client."
    )]
    [Uri("https://tools.ietf.org/html/rfc6749#/section-5.2/invalid_grant")]
    InvalidGrant,

    [Description("unsupported_grant_type")]
    [Display(
        Name = "Unsupported Grant Type",
        ShortName = "unsupported_grant_type",
        Description = "The authorization grant type is not supported by the authorization server."
    )]
    [Uri("https://tools.ietf.org/html/rfc6749#/section-5.2/unsupported_grant_type")]
    UnsupportedGrantType,

    [Description("invalid_token")]
    [Display(
        Name = "Invalid Token",
        ShortName = "invalid_token",
        Description = "The access token provided is expired, revoked, malformed, or invalid for other reasons."
    )]
    [Uri("https://tools.ietf.org/html/rfc6750#/section-3.1/invalid_token")]
    InvalidToken,

    [Description("insufficient_scope")]
    [Display(
        Name = "Insufficient Scope",
        ShortName = "insufficient_scope",
        Description = "The request requires higher privileges than provided by the access token."
    )]
    [Uri("https://tools.ietf.org/html/rfc6750#/section-3.1/insufficient_scope")]
    InsufficientScope,

    [Description("invalid_request_object")]
    [Display(
        Name = "Invalid Request Object",
        ShortName = "invalid_request_object",
        Description = "The request object is not valid."
    )]
    [Uri(
        "https://openid.net/specs/openid-connect-core-1_0.html#/JWTRequests/invalid_request_object"
    )]
    InvalidRequestObject,

    [Description("request_not_supported")]
    [Display(
        Name = "Request Not Supported",
        ShortName = "request_not_supported",
        Description = "The OP does not support use of the request parameter."
    )]
    [Uri(
        "https://openid.net/specs/openid-connect-core-1_0.html#/JWTRequests/request_not_supported"
    )]
    RequestNotSupported,

    [Description("request_uri_not_supported")]
    [Display(
        Name = "Request Uri Not Supported",
        ShortName = "request_uri_not_supported",
        Description = "The OP does not support use of the request_uri parameter."
    )]
    [Uri(
        "https://openid.net/specs/openid-connect-core-1_0.html#/JWTRequests/request_uri_not_supported"
    )]
    RequestUriNotSupported,

    [Description("registration_not_supported")]
    [Display(
        Name = "Registration Not Supported",
        ShortName = "registration_not_supported",
        Description = "The OP does not support use of the registration parameter."
    )]
    [Uri(
        "https://openid.net/specs/openid-connect-core-1_0.html#/JWTRequests/registration_not_supported"
    )]
    RegistrationNotSupported,

    [Description("invalid_redirect_uri")]
    [Display(
        Name = "Invalid Redirect Uri",
        ShortName = "invalid_redirect_uri",
        Description = "The value of one or more redirect_uris is invalid."
    )]
    [Uri("https://openid.net/specs/openid-connect-core-1_0.html#/AuthError/invalid_redirect_uri")]
    InvalidRedirectUri,

    [Description("invalid_client_metadata")]
    [Display(
        Name = "Invalid Client Metadata",
        ShortName = "invalid_client_metadata",
        Description = "The value of one of the Client Metadata fields is invalid and the server has rejected this request. Note that an Authorization Server MAY choose to substitute a valid value for any requested parameter of a Client's Metadata."
    )]
    [Uri(
        "https://openid.net/specs/openid-connect-registration-1_0.html#/RegistrationError/invalid_client_metadata"
    )]
    InvalidClientMetadata,

    [Description("invalid_client_secret")]
    [Display(
        Name = "Invalid Client Secret",
        ShortName = "invalid_client_secret",
        Description = "The value of the client_secret parameter is invalid for the given client_id."
    )]
    [Uri(
        "https://openid.net/specs/openid-connect-registration-1_0.html#/RegistrationError/invalid_client_metadata"
    )]
    InvalidClientSecret,

    [Description("invalid_software_statement")]
    [Display(
        Name = "Invalid Software Statement",
        ShortName = "invalid_software_statement",
        Description = "The value of the software_statement parameter is invalid."
    )]
    [Uri(
        "https://openid.net/specs/openid-connect-registration-1_0.html#/RegistrationError/invalid_software_statement"
    )]
    InvalidSoftwareStatement,

    [Description("invalid_request_uri")]
    [Display(
        Name = "Invalid Request Uri",
        ShortName = "invalid_request_uri",
        Description = "The value of the request_uri is invalid."
    )]
    [Uri("https://openid.net/specs/openid-connect-core-1_0.html#/AuthError/invalid_request_uri")]
    InvalidRequestUri,

    [Description("invalid_data")]
    [Display(
        Name = "Invalid Data",
        ShortName = "invalid_data",
        Description = "Verification failed; invalid data were passed from \\\"Telegram\\\""
    )]
    [Uri("https://core.telegram.org/widgets/login#/receiving-authorization-data")]
    InvalidData,

    [Description("invalid_hash")]
    [Display(
        Name = "Invalid Hash",
        ShortName = "invalid_hash",
        Description = "Verification failed; hash is not valid."
    )]
    [Uri("https://core.telegram.org/widgets/login#/receiving-authorization-data/invalid-hash")]
    InvalidHash,

    [Description("invalid_timestamp")]
    [Display(
        Name = "Invalid Timestamp",
        ShortName = "invalid_timestamp",
        Description = "Verification failed; timestamp is not within an acceptable range."
    )]
    [Uri("https://core.telegram.org/widgets/login#/receiving-authorization-data/invalid-timestamp")]
    InvalidTimestamp,

    [Description("invalid_nonce")]
    [Display(
        Name = "Invalid Nonce",
        ShortName = "invalid_nonce",
        Description = "Verification failed; nonce is not valid."
    )]
    [Uri("https://core.telegram.org/widgets/login#/receiving-authorization-data/invalid-nonce")]
    InvalidNonce,

    [Description("invalid_user")]
    [Display(
        Name = "Invalid User",
        ShortName = "invalid_user",
        Description = "Verification failed; user is not valid."
    )]
    [Uri("https://core.telegram.org/widgets/login#/receiving-authorization-data/invalid-user")]
    InvalidUser,

    [Description("invalid_id")]
    [Display(
        Name = "Invalid Id",
        ShortName = "invalid_id",
        Description = "Verification failed; id is not valid."
    )]
    [Uri("https://core.telegram.org/widgets/login#/receiving-authorization-data/invalid-id")]
    InvalidId,

    [Description("invalid_first_name")]
    [Display(
        Name = "Invalid First Name",
        ShortName = "invalid_first_name",
        Description = "Verification failed; first name is not valid."
    )]
    [Uri(
        "https://core.telegram.org/widgets/login#/receiving-authorization-data/invalid-first-name"
    )]
    InvalidFirstName,

    [Description("invalid_last_name")]
    [Display(
        Name = "Invalid Last Name",
        ShortName = "invalid_last_name",
        Description = "Verification failed; last name is not valid."
    )]
    [Uri("https://core.telegram.org/widgets/login#/receiving-authorization-data/invalid-last-name")]
    InvalidLastName,

    [Description("invalid_photo_url")]
    [Display(
        Name = "Invalid Photo Url",
        ShortName = "invalid_photo_url",
        Description = "Verification failed; photo url is not valid."
    )]
    [Uri("https://core.telegram.org/widgets/login#/receiving-authorization-data/invalid-photo-url")]
    InvalidPhotoUrl,

    [Description("invalid_auth_date")]
    [Display(
        Name = "Invalid Auth Date",
        ShortName = "invalid_auth_date",
        Description = "Verification failed; auth date is not valid."
    )]
    [Uri("https://core.telegram.org/widgets/login#/receiving-authorization-data/invalid-auth-date")]
    InvalidAuthDate,

    [Description("invalid_hash_algorithm")]
    [Display(
        Name = "Invalid Hash Algorithm",
        ShortName = "invalid_hash_algorithm",
        Description = "Verification failed; hash algorithm is not valid."
    )]
    [Uri(
        "https://core.telegram.org/widgets/login#/receiving-authorization-data/invalid-hash-algorithm"
    )]
    InvalidHashAlgorithm,

    [Description("invalid_payload")]
    [Display(
        Name = "Invalid Payload",
        ShortName = "invalid_payload",
        Description = "Verification failed; payload is not valid."
    )]
    [Uri("https://core.telegram.org/widgets/login#/receiving-authorization-data/invalid-payload")]
    InvalidPayload,

    [Description("invalid_signature")]
    [Display(
        Name = "Invalid Signature",
        ShortName = "invalid_signature",
        Description = "Verification failed; signature is not valid."
    )]
    [Uri("https://core.telegram.org/widgets/login#/receiving-authorization-data/invalid-signature")]
    InvalidSignature,

    [Description("invalid_user_data")]
    [Display(
        Name = "Invalid User Data",
        ShortName = "invalid_user_data",
        Description = "Verification failed; user data is not valid."
    )]
    [Uri("https://core.telegram.org/widgets/login#/receiving-authorization-data/invalid-user-data")]
    InvalidUserData,
}
