namespace Telegram.OpenIdConnect.Enums;

public enum GrantType
{
    [Description("password")]
    Password,

    [Description("authorization_code")]
    AuthorizationCode,

    [Description("client_credentials")]
    ClientCredentials,

    [Description("refresh_token")]
    RefreshToken,

    [Description("implicit")]
    Implicit,

    [Description("urn:ietf:params:oauth:grant-type:saml2-bearer")]
    Saml2Bearer,

    [Description("urn:ietf:params:oauth:grant-type:jwt-bearer")]
    JwtBearer,

    [Description("urn:ietf:params:oauth:grant-type:device_code")]
    DeviceCode,

    [Description("urn:ietf:params:oauth:grant-type:token-exchange")]
    TokenExchange,

    [Description("urn:openid:params:grant-type:ciba")]
    Ciba
}
