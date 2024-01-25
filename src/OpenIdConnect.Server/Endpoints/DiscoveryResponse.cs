using System.Globalization;
using System.Text.Json.Serialization;

using Telegram.OpenIdConnect.Enums;
using Telegram.OpenIdConnect.Models.Responses;

using ClaimType = Telegram.OpenIdConnect.Enums.ClaimType;

namespace Telegram.OpenIdConnect.Endpoints;

public class DiscoveryResponse
{
    [JsonPropertyName("issuer")]
    public string Issuer { get; set; }

    [JsonPropertyName("authorization_endpoint")]
    public string AuthorizationEndpoint { get; set; }

    [JsonPropertyName("token_endpoint")]
    public string TokenEndpoint { get; set; }

    [JsonPropertyName("token_endpoint_auth_methods_supported")]
    public IList<TokenEndpointAuthenticationMethod> TokenEndpointAuthMethodsSupported { get; set; }

    [JsonPropertyName("token_endpoint_auth_signing_alg_values_supported")]
    public IList<Algorithm> TokenEndpointAuthSigningAlgValuesSupported { get; set; }

    [JsonPropertyName("userinfo_endpoint")]
    public string UserInfoEndpoint { get; set; }

    [JsonPropertyName("check_session_iframe")]
    public string CheckSessionIFrame { get; set; }

    [JsonPropertyName("end_session_endpoint")]
    public string EndSessionEndpoint { get; set; }

    [JsonPropertyName("jwks_uri")]
    public string JwksUri { get; set; }

    [JsonPropertyName("registration_endpoint")]
    public string RegistrationEndpoint { get; set; }

    [JsonPropertyName("scopes_supported")]
    public IList<StandardScope> ScopesSupported { get; set; }

    [JsonPropertyName("response_types_supported")]
    public IList<OidcResponseType> ResponseTypesSupported { get; set; }

    [JsonPropertyName("acr_values_supported")]
    public IList<Uri> AcrValuesSupported { get; set; }

    [JsonPropertyName("subject_types_supported")]
    public IList<SubjectType> SubjectTypesSupported { get; set; }

    [JsonPropertyName("userinfo_signing_alg_values_supported")]
    public IList<string> UserinfoSigningAlgValuesSupported { get; set; }

    [JsonPropertyName("userinfo_encryption_alg_values_supported")]
    public IList<string> UserinfoEncryptionAlgValuesSupported { get; set; }

    [JsonPropertyName("userinfo_encryption_enc_values_supported")]
    public IList<Algorithm> UserInfoEncryptionEncValuesSupported { get; set; }

    [JsonPropertyName("id_token_signing_alg_values_supported")]
    public IList<Algorithm> IdTokenSigningAlgValuesSupported { get; set; }

    [JsonPropertyName("id_token_encryption_alg_values_supported")]
    public IList<Algorithm> IdTokenEncryptionAlgValuesSupported { get; set; }

    [JsonPropertyName("id_token_encryption_enc_values_supported")]
    public IList<Algorithm> IdTokenEncryptionEncValuesSupported { get; set; }

    [JsonPropertyName("request_object_signing_alg_values_supported")]
    public IList<Algorithm> RequestObjectSigningAlgValuesSupported { get; set; }

    [JsonPropertyName("display_values_supported")]
    public IList<DisplayMode> DisplayValuesSupported { get; set; }

    [JsonPropertyName("response_modes_supported")]
    public IList<ResponseMode> ResponseModesSupported { get; set; }

    [JsonPropertyName("claim_types_supported")]
    public IList<ClaimType> ClaimTypesSupported { get; set; }

    [JsonPropertyName("claims_supported")]
    public IList<string> ClaimsSupported { get; set; }

    [JsonPropertyName("claims_parameter_supported")]
    public bool ClaimsParameterSupported { get; set; }

    [JsonPropertyName("service_documentation")]
    public string ServiceDocumentation { get; set; }

    [JsonPropertyName("ui_locales_supported")]
    public IList<CultureInfo> UiLocalesSupported { get; set; }
}
