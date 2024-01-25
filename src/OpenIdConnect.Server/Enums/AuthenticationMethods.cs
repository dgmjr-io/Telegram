namespace Telegram.OpenIdConnect.Enums;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public enum AuthenticationMethods
{
    [Description("face")]
    FacialRecognition,

    [Description("fpt")]
    FingerprintBiometric,

    [Description("geo")]
    Geolocation,

    [Description("hwk")]
    ProofOfPossessionHardwareSecuredKey,

    [Description("iris")]
    IrisScanBiometric,

    [Description("kba")]
    KnowledgeBasedAuthentication,

    [Description("mca")]
    MultipleChannelAuthentication,

    [Description("mfa")]
    MultiFactorAuthentication,

    [Description("otp")]
    OneTimePassword,

    [Description("pin")]
    PersonalIdentificationOrPattern,

    [Description("pwd")]
    Password,

    [Description("rba")]
    RiskBasedAuthentication,

    [Description("retina")]
    RetinaScanBiometric,

    [Description("sc")]
    SmartCard,

    [Description("sms")]
    ConfirmationBySms,

    [Description("swk")]
    ProofOfPossessionSoftwareSecuredKey,

    [Description("tel")]
    ConfirmationByTelephone,

    [Description("user")]
    UserPresenceTest,

    [Description("vbm")]
    VoiceBiometric,

    [Description("wia")]
    WindowsIntegratedAuthentication
}
