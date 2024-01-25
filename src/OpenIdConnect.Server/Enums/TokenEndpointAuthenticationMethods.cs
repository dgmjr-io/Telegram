namespace Telegram.OpenIdConnect.Enums;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public enum TokenEndpointAuthenticationMethod
{
    [Description("client_secret_post")]
    PostBody,

    [Description("client_secret_basic")]
    BasicAuthentication,

    [Description("private_key_jwt")]
    PrivateKeyJwt,

    [Description("tls_client_auth")]
    TlsClientAuth,

    [Description("self_signed_tls_client_auth")]
    SelfSignedTlsClientAuth
}
