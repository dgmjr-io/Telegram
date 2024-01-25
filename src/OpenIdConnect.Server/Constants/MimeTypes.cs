namespace Telegram.OpenIdConnect.Constants;

public static class MimeTypes
{
    public const string OpenIdConnectConfigurationMimeType =
        "application/.well-known-openid-configuration+json; charset=UTF-8";

    public const string JsonWebKeySetMimeType = "application/jwk-set+json; charset=UTF-8";

    public const string TokenResponseMimeType = "application/jwt+json; charset=UTF-8";
}
