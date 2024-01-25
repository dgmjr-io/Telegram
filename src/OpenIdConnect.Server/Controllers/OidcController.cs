using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using static Microsoft.AspNetCore.Http.StatusCodes;

using static Telegram.OpenIdConnect.Constants.MimeTypes;
using Telegram.OpenIdConnect.Extensions;
using Telegram.OpenIdConnect.Options;
using Telegram.OpenIdConnect.Services;
using Telegram.OpenIdConnect.Services.CodeService;
using Telegram.OpenIdConnect.Models.Requests;
using Telegram.OpenIdConnect.Models.Responses;

namespace Telegram.OpenIdConnect.Controllers;

public partial class OidcController(
    IHttpContextAccessor httpContextAccessor,
    IAuthorizationService authorizeResultService,
    ICodeStoreService codeStoreService,
    IOptionsMonitor<TelegramOpenIdConnectServerOptions> options,
    ILogger<OidcController> logger
) : Controller, ILog
{
    public ILogger Logger => logger;
    private IHttpContextAccessor HttpContextAccessor => httpContextAccessor;
    private IAuthorizationService AuthorizationService => authorizeResultService;
    private ICodeStoreService CodeStoreService => codeStoreService;
    private TelegramOpenIdConnectServerOptions Options => options.CurrentValue;

    [HttpGet("/error")]
    public IActionResult Error(string error)
    {
        return View(error);
    }

    [HttpGet("oauth2/login")]
    public IActionResult Login()
    {
        return View(Options);
    }

    // [HttpPost("oauth2/login")]
    // public async Task<IActionResult> Login(TelegramOpenIdConnectLoginRequest loginRequest)
    // {
    //     // var result = CodeStoreService.UpdatedClientDataByCode(
    //     //     loginRequest.Code,
    //     //     loginRequest.RequestedScopes,
    //     //     loginRequest.UserName,
    //     //     nonce: loginRequest.Nonce
    //     // );
    //     // if (result != null)
    //     // {
    //     //     loginRequest.RedirectUri = loginRequest.RedirectUri + "&code=" + loginRequest.Code;
    //     //     return Redirect(loginRequest.RedirectUri);
    //     // }
    //     // return RedirectToAction("Error", new { error = "invalid_request" });
    // }


    [HttpGet("oauth2/userinfo")]
    public IActionResult UserInfo() => throw new NotImplementedException();
}
