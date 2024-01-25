namespace Telegram.OpenIdConnect.Enums;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public enum Discovery
{
    [Description("issuer")]
    Issuer,

    // endpoints
    [Description("authorization_endpoint")]
    AuthorizationEndpoint,

    [Description("device_authorization_endpoint")]
    DeviceAuthorizationEndpoint,

    [Description("token_endpoint")]
    TokenEndpoint,

    [Description("userinfo_endpoint")]
    UserInfoEndpoint,

    [Description("introspection_endpoint")]
    IntrospectionEndpoint,

    [Description("revocation_endpoint")]
    RevocationEndpoint,

    [Description(".well-known/openid-configuration")]
    DiscoveryEndpoint,

    [Description("jwks_uri")]
    JwksUri,

    [Description("end_session_endpoint")]
    EndSessionEndpoint,

    [Description("check_session_iframe")]
    CheckSessionIframe,

    [Description("registration_endpoint")]
    RegistrationEndpoint,

    [Description("mtls_endpoint_aliases")]
    MtlsEndpointAliases,

    // common capabilities
    [Description("frontchannel_logout_supported")]
    FrontChannelLogoutSupported,

    [Description("frontchannel_logout_session_supported")]
    FrontChannelLogoutSessionSupported,

    [Description("backchannel_logout_supported")]
    BackChannelLogoutSupported,

    [Description("backchannel_logout_session_supported")]
    BackChannelLogoutSessionSupported,

    [Description("grant_types_supported")]
    GrantTypesSupported,

    [Description("code_challenge_methods_supported")]
    CodeChallengeMethodsSupported,

    [Description("scopes_supported")]
    ScopesSupported,

    [Description("subject_types_supported")]
    SubjectTypesSupported,

    [Description("response_modes_supported")]
    ResponseModesSupported,

    [Description("response_types_supported")]
    ResponseTypesSupported,

    [Description("claims_supported")]
    ClaimsSupported,

    [Description("token_endpoint_auth_methods_supported")]
    TokenEndpointAuthenticationMethodsSupported,

    // more capabilities
    [Description("claims_locales_supported")]
    ClaimsLocalesSupported,

    [Description("claims_parameter_supported")]
    ClaimsParameterSupported,

    [Description("claim_types_supported")]
    ClaimTypesSupported,

    [Description("display_values_supported")]
    DisplayValuesSupported,

    [Description("acr_values_supported")]
    AcrValuesSupported,

    [Description("id_token_encryption_alg_values_supported")]
    IdTokenEncryptionAlgorithmsSupported,

    [Description("id_token_encryption_enc_values_supported")]
    IdTokenEncryptionEncValuesSupported,

    [Description("id_token_signing_alg_values_supported")]
    IdTokenSigningAlgorithmsSupported,

    [Description("op_policy_uri")]
    OpPolicyUri,

    [Description("op_tos_uri")]
    OpTosUri,

    [Description("request_object_encryption_alg_values_supported")]
    RequestObjectEncryptionAlgorithmsSupported,

    [Description("request_object_encryption_enc_values_supported")]
    RequestObjectEncryptionEncValuesSupported,

    [Description("request_object_signing_alg_values_supported")]
    RequestObjectSigningAlgorithmsSupported,

    [Description("request_parameter_supported")]
    RequestParameterSupported,

    [Description("request_uri_parameter_supported")]
    RequestUriParameterSupported,

    [Description("require_request_uri_registration")]
    RequireRequestUriRegistration,

    [Description("service_documentation")]
    ServiceDocumentation,

    [Description("token_endpoint_auth_signing_alg_values_supported")]
    TokenEndpointAuthSigningAlgorithmsSupported,

    [Description("ui_locales_supported")]
    UILocalesSupported,

    [Description("userinfo_encryption_alg_values_supported")]
    UserInfoEncryptionAlgorithmsSupported,

    [Description("userinfo_encryption_enc_values_supported")]
    UserInfoEncryptionEncValuesSupported,

    [Description("userinfo_signing_alg_values_supported")]
    UserInfoSigningAlgorithmsSupported,

    [Description("tls_client_certificate_bound_access_tokens")]
    TlsClientCertificateBoundAccessTokens,

    [Description("authorization_response_iss_parameter_supported")]
    AuthorizationResponseIssParameterSupported,

    [Description("prompt_values_supported")]
    PromptValuesSupported,

    [Description("backchannel_token_delivery_modes_supported")]
    BackchannelTokenDeliveryModesSupported,

    [Description("backchannel_authentication_endpoint")]
    BackchannelAuthenticationEndpoint,

    [Description("backchannel_authentication_request_signing_alg_values_supported")]
    BackchannelAuthenticationRequestSigningAlgValuesSupported,

    [Description("backchannel_user_code_parameter_supported")]
    BackchannelUserCodeParameterSupported,

    [Description("dpop_signing_alg_values_supported")]
    DPoPSigningAlgorithmsSupported
}
