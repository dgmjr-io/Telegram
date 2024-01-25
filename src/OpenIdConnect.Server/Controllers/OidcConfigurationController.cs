namespace Telegram.OpenIdConnect.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

using Telegram.OpenIdConnect.Endpoints;
using Telegram.OpenIdConnect.Enums;
using Telegram.OpenIdConnect.Models.Responses;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Telegram.OpenIdConnect.Extensions;
using static Telegram.OpenIdConnect.Constants.MimeTypes;
using static Microsoft.AspNetCore.Http.StatusCodes;
using System.IdentityModel.Tokens.Jwt;

using ClaimType = Telegram.OpenIdConnect.Enums.ClaimType;
using Telegram.OpenIdConnect.Constants;
using System.Text.Json.Nodes;

public class OidcConfigurationController(LinkGenerator links) : Controller
{
    // .well-known/openid-configuration
    [HttpGet("~/.well-known/openid-configuration")]
    [ProducesResponseType<OpenIdConnectConfiguration>(
        Status200OK,
        OpenIdConnectConfigurationMimeType
    )]
    public async Task<IActionResult> GetConfiguration()
    {
        var response = new OpenIdConnectConfiguration
        {
            Issuer = new UriBuilder(Request.Scheme, Request.Host.Host).ToString(),
            AuthorizationEndpoint = CreateUri(
                nameof(OidcController),
                nameof(OidcController.Authorize)
            ),
            TokenEndpoint = CreateUri(nameof(OidcController.Token), nameof(OidcController)),
            JwksUri = CreateUri(nameof(JwksController.Get), nameof(JwksController)),
            UserInfoEndpoint = CreateUri(nameof(OidcController.UserInfo), nameof(OidcController))
        };

        response.ResponseModesSupported.Add(ResponseMode.Query.GetDescription());
        response.ResponseModesSupported.Add(ResponseMode.Fragment.GetDescription());
        response.ResponseModesSupported.Add(ResponseMode.FormPost.GetDescription());

        response.ResponseTypesSupported.Add(OidcResponseType.Code.GetDescription());
        response.ResponseTypesSupported.Add(OidcResponseType.CodeIdToken.GetDescription());
        response.ResponseTypesSupported.Add(OidcResponseType.IdToken.GetDescription());
        response.ResponseTypesSupported.Add(OidcResponseType.IdTokenToken.GetDescription());

        response.TokenEndpointAuthMethodsSupported.Add(
            TokenEndpointAuthenticationMethod.BasicAuthentication.GetDescription()
        );
        response.TokenEndpointAuthMethodsSupported.Add(
            TokenEndpointAuthenticationMethod.PrivateKeyJwt.GetDescription()
        );

        response.AcrValuesSupported.Add(new("https://t.me/identity/verify#telegram"));
        response.AcrValuesSupported.Add(new("urn:org:telegram:identity:verify"));
        response.AcrValuesSupported.Add(new("urn:mace:incommon:iap:silver"));
        response.AcrValuesSupported.Add(new("urn:mace:incommon:iap:bronze"));

        response.SubjectTypesSupported.Add(SubjectType.Pairwise.GetDescription());

        response.UserInfoEndpointEncryptionEncValuesSupported.Add(
            Algorithm.A128CBC_HS256.GetDescription()
        );
        response.UserInfoEndpointEncryptionEncValuesSupported.Add(
            Algorithm.A128GCM.GetDescription()
        );

        response.IdTokenSigningAlgValuesSupported.Add(Algorithm.RS256.GetDescription());
        response.IdTokenSigningAlgValuesSupported.Add(Algorithm.ES256.GetDescription());
        response.IdTokenSigningAlgValuesSupported.Add(Algorithm.HS256.GetDescription());

        response.IdTokenEncryptionAlgValuesSupported.Add(Algorithm.RSA1_5.GetDescription());
        response.IdTokenEncryptionAlgValuesSupported.Add(Algorithm.A128KW.GetDescription());

        response.IdTokenEncryptionEncValuesSupported.Add(
            Algorithm.A128CBC_HS256.GetDescription()
        );
        response.IdTokenEncryptionEncValuesSupported.Add(Algorithm.A128GCM.GetDescription());

        response.RequestObjectSigningAlgValuesSupported.Add(Algorithm.None.GetDescription());
        response.RequestObjectSigningAlgValuesSupported.Add(Algorithm.RS256.GetDescription());
        response.RequestObjectSigningAlgValuesSupported.Add(Algorithm.ES256.GetDescription());

        response.DisplayValuesSupported.Add(DisplayMode.Page.GetDescription());
        response.DisplayValuesSupported.Add(DisplayMode.Popup.GetDescription());

        response.ClaimTypesSupported.Add(ClaimType.Normal.GetDescription());
        response.ClaimTypesSupported.Add(ClaimType.Distributed.GetDescription());

        response.ScopesSupported.Add(StandardScope.OpenId.GetDescription());
        response.ScopesSupported.Add(StandardScope.Profile.GetDescription());
        response.ScopesSupported.Add(StandardScope.Email.GetDescription());
        response.ScopesSupported.Add(StandardScope.Address.GetDescription());
        response.ScopesSupported.Add(StandardScope.Phone.GetDescription());
        response.ScopesSupported.Add(StandardScope.OfflineAccess.GetDescription());

        ForEach(SupportedClaimTypes, (claim) => response.ClaimsSupported.Add(claim));
        // response.ClaimsSupported.AddRange(SupportedClaimTypes);
        // response.ClaimsSupported.Add("sub");
        // response.ClaimsSupported.Add("iss");
        // response.ClaimsSupported.Add("auth_time");
        // response.ClaimsSupported.Add("acr");
        // response.ClaimsSupported.Add("name");
        // response.ClaimsSupported.Add("given_name");
        // response.ClaimsSupported.Add("family_name");
        // response.ClaimsSupported.Add("nickname");
        // response.ClaimsSupported.Add("profile");
        // response.ClaimsSupported.Add("picture");
        // response.ClaimsSupported.Add("website");
        // response.ClaimsSupported.Add("email");
        // response.ClaimsSupported.Add("email_verified");
        // response.ClaimsSupported.Add("locale");
        // response.ClaimsSupported.Add("zoneinfo");

        response.ClaimsParameterSupported = true;

        response.ServiceDocumentation = CreateUri(
            nameof(ServiceDocumentationController).Replace(nameof(Controller), ""),
            nameof(ServiceDocumentationController.Get)
        );

        response.UILocalesSupported.Add(new("en-US"));
        // response.UILocalesSupported.Add(new("en-GB"));
        // response.UILocalesSupported.Add(new("en-CA"));
        // response.UILocalesSupported.Add(new("fr-FR"));
        // response.UILocalesSupported.Add(new("fr-CA"));

#region oldstuff
        // ResponseModesSupported = [ResponseMode.Query, ResponseMode.Fragment, ResponseMode.FormPost],
        // TokenEndpointAuthMethodsSupported = [
        //     TokenEndpointAuthenticationMethod.BasicAuthentication,
        //     TokenEndpointAuthenticationMethod.PrivateKeyJwt
        // ],
        // TokenEndpointAuthSigningAlgValuesSupported = [Algorithms.RS256, Algorithms.ES256],
        // AcrValuesSupported =
        // [
        //     new("https://t.me/identity/verify#telegram"),
        //     new("urn:org:telegram:identity:verify"),
        //     new("urn:mace:incommon:iap:silver"),
        //     new("urn:mace:incommon:iap:bronze"),
        // ],
        // ResponseTypesSupported = new List<OidcResponseType>
        // {
        //     OidcResponseType.Code,
        //     OidcResponseType.CodeIdToken,
        //     OidcResponseType.IdToken,
        //     OidcResponseType.IdTokenToken
        // },
        // SubjectTypesSupported = [SubjectType.Pairwise],
        // UserInfoEncryptionEncValuesSupported = [Algorithms.A128CBC_HS256, Algorithms.A128GCM],
        // IdTokenSigningAlgValuesSupported = [Algorithms.RS256, Algorithms.ES256, Algorithms.HS256],
        // IdTokenEncryptionAlgValuesSupported = [Algorithms.RSA1_5, Algorithms.A128KW],
        // IdTokenEncryptionEncValuesSupported = [Algorithms.A128CBC_HS256, Algorithms.A128GCM],
        // RequestObjectSigningAlgValuesSupported = [Algorithms.None, Algorithms.RS256, Algorithms.ES256],
        // DisplayValuesSupported = [ DisplayMode.Page, DisplayMode.Popup ],
        // ClaimTypesSupported = [ClaimType.Normal, ClaimType.Distributed],
        // ScopesSupported = [StandardScope.OpenId, StandardScope.Profile, StandardScope.Email, StandardScope.Address, StandardScope.Phone, StandardScope.OfflineAccess],
        // ClaimsSupported = new List<string>
        // {
        //     "sub",
        //     "iss",
        //     "auth_time",
        //     "acr",
        //     "name",
        //     "given_name",
        //     "family_name",
        //     "nickname",
        //     "profile",
        //     "picture",
        //     "website",
        //     "email",
        //     "email_verified",
        //     "locale",
        //     "zoneinfo"
        // },
        // ClaimsParameterSupported = true,
        // ServiceDocumentation = CreateUri(nameof(ServiceDocumentationController).Replace(nameof(Controller), ""), nameof(ServiceDocumentationController.ServiceDocumentation)),
        // UiLocalesSupported = [new ("en-US"), new("en-GB"), new("en-CA"), new("fr-FR"), new("fr-CA")]
        #endregion

        var json = OpenIdConnectConfigurationSerializerWrite(response);
        using var jsonStream = new MemoryStream(UTF8.GetBytes(json));
        var jDoc = await JDoc.ParseAsync(jsonStream);
        var jObj = JNode.Parse(json, default).AsObject()!;
        jObj["issuer"] = JValue.Create(response.Issuer);
        jObj["jwks_uri"] = JValue.Create(response.JwksUri);

        return Content(Serialize(jObj), OpenIdConnectConfigurationMimeType);
    }

    private static readonly type OpenIdConnectConfigurationSerializerType = typeof(
        OpenIdConnectConfiguration
    ).Assembly.GetType("Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectConfigurationSerializer", true)!;

    private static readonly MethodInfo OpenIdConnectConfigurationSerializerWriteMethod = OpenIdConnectConfigurationSerializerType.GetMethod(
        "Write",
        BindingFlags.Static | BindingFlags.Public,
        null,
        [ typeof(OpenIdConnectConfiguration) ],
        null
    )!;

    private static readonly Func<OpenIdConnectConfiguration, string> OpenIdConnectConfigurationSerializerWrite = (Func<OpenIdConnectConfiguration, string>)Delegate.CreateDelegate(
        typeof(Func<OpenIdConnectConfiguration, string>),
        OpenIdConnectConfigurationSerializerWriteMethod
    )!;

    private static readonly string[] SupportedClaimTypes =[
        Claims.FirstName,
        Claims.LastName,
        Claims.PhotoUrl,
        Claims.Uri,
        Claims.UserId,
        Claims.Username,
        Ct.AuthenticationInstant,
        Ct.AuthenticationMethod,
        Ct.Expiration,
        Ct.GivenName,
        Ct.Hash,
        Ct.Name,
        Ct.NameIdentifier,
        Ct.Surname,
        Ct.Upn,
        Ct.Uri,
        Ct.Webpage,
        FirstName.UriString,
        JwtRegisteredClaimNames.Acr,
        JwtRegisteredClaimNames.Exp,
        JwtRegisteredClaimNames.Iss,
        JwtRegisteredClaimNames.Nbf,
        JwtRegisteredClaimNames.FamilyName,
        JwtRegisteredClaimNames.GivenName,
        JwtRegisteredClaimNames.AuthTime,
        JwtRegisteredClaimNames.Sub,
        JwtRegisteredClaimNames.UniqueName,
        JwtRegisteredClaimNames.Website,
    ];

    private string CreateUri(string action, string controller) =>
        links.GetUriByAction(
            Request.HttpContext,
            action,
            controller.Replace(nameof(Controller), ""),
            null,
            Request.Scheme,
            Request.Host
        )!;
    // new UriBuilder(
    //     Request.Scheme,
    //     Request.Host.Host,
    //     Request.Host.Port.Value,
    //     path
    // ).Uri.ToString();
}
