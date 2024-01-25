namespace Telegram.OpenIdConnect.Enums;

using System.ComponentModel.DataAnnotations;

public enum ClientAssertionType
{
    [Display(Description = "urn:ietf:params:oauth:client-assertion-type:jwt-bearer")]
    JwtBearer,

    [Display(Description = "urn:ietf:params:oauth:client-assertion-type:saml2-bearer")]
    SamlBearer
}
