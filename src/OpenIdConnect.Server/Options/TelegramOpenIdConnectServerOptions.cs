namespace Telegram.OpenIdConnect.Options;

using System.Security.Cryptography.X509Certificates;

using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;

using MimeKit.Cryptography;

using Telegram.OpenIdConnect.Models.Responses;

using BotApiToken = Bot.Types.BotApiToken;
using File = System.IO.File;

public partial class TelegramOpenIdConnectServerOptions
{
    public const string AuthenticationScheme = "TelegramOpenIdConnectServer";
    public const string DisplayName = "Telegram OpenId Connect Server";
    public const string ConfigurationSectionKey = "TelegramOpenIdConnectServer";

    [Required]
    public string BotName { get; set; }

    [Required]
    public BotApiToken BotApiToken { get; set; }

    public CertificateDescription? ServerCertificate { get; set; }

    private byte[] ServerCertificateBytes
    {
        get
        {
            switch (ServerCertificate?.SourceType)
            {
                case CertificateSource.Path:
                    return File.ReadAllBytes(ServerCertificate.CertificateDiskPath);
                case CertificateSource.Base64Encoded:
                    return Convert.FromBase64String(ServerCertificate.Base64EncodedValue);
                case CertificateSource.KeyVault:
                    new DefaultCertificateLoader().LoadIfNeeded(ServerCertificate);
                    return ServerCertificate?.Certificate.Export(X509ContentType.Cert)
                        ?? ""u8.ToArray();
                case CertificateSource.StoreWithDistinguishedName:
                    return new X509Store(StoreName.My, StoreLocation.LocalMachine).Certificates
                            .Find(
                                X509FindType.FindBySubjectDistinguishedName,
                                ServerCertificate.CertificateDistinguishedName,
                                false
                            )
                            .OfType<X509Certificate2>()
                            .FirstOrDefault()
                            ?.Export(X509ContentType.Cert) ?? ""u8.ToArray();
                case CertificateSource.StoreWithThumbprint:
                    return new X509Store(StoreName.My, StoreLocation.LocalMachine).Certificates
                            .Find(
                                X509FindType.FindByThumbprint,
                                ServerCertificate.CertificateThumbprint,
                                false
                            )
                            .OfType<X509Certificate2>()
                            .FirstOrDefault()
                            ?.Export(X509ContentType.Cert) ?? ""u8.ToArray();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    [JIgnore]
    public X509Certificate2 X509Certificate => new(ServerCertificateBytes);

    [JIgnore]
    public X509SecurityKey X509SecurityKey => new(X509Certificate);

    [JIgnore]
    public SigningCredentials SigningCredentials => new (X509SecurityKey, SecurityAlgorithms.RsaSha256);

    /// <summary>This derives a <see cref="JsonWebKeySet" /> from the certificate located at <see cref="ServerCertificate" /></summary>
    [JIgnore]
    public JsonWebKeySet JsonWebKeySet
    {
        get
        {
            var x509 = X509Certificate;
            var nbf = datetime.Parse(x509.GetEffectiveDateString()).ToFileTimeUtc();
            var exp = datetime.Parse(x509.GetExpirationDateString()).ToFileTimeUtc();
            var securityKey = new X509SecurityKey(x509);
            var kty = x509.GetKeyAlgorithm() ?? "RSA";
            var kid = securityKey.KeyId ?? guid.NewGuid().ToString();
            var use = "sig";
            var alg = new Oid(x509.GetKeyAlgorithm()).FriendlyName;
            var n = Base64UrlEncoder.Encode(
                x509.GetRSAPublicKey()?.ExportParameters(false).Modulus
            );
            var e = Base64UrlEncoder.Encode(
                x509.GetRSAPublicKey()?.ExportParameters(false).Exponent
            );
            var x5t = Base64UrlEncoder.Encode(x509.GetCertHash());
            var x5u = x509.GetNameInfo(X509NameType.UrlName, false);
            var jwk = new JsonWebKey
            {
                Kty = alg,
                Kid = kid,
                Use = use,
                Alg = alg,
                N = n,
                E = e,
                // X5t = x5t,
                // X5u = x5u
            };
            var jwks = new JsonWebKeySet();
            jwks.Keys.Add(jwk);
            return jwks;
        }
    }

    public ClientStore Clients { get; set; } = [];
}
