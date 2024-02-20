namespace Telegram.Bot.Identity;

using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using static Newtonsoft.Json.JsonConvert;
using ClaimsIdentity = System.Security.Claims.ClaimsIdentity;
using ClaimsPrincipal = System.Security.Claims.ClaimsPrincipal;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

using Telegram.Identity.ClaimTypes;

using User = Microsoft.Graph.User;
using Dgmjr.AzureAdB2C.Identity;

using Uris = Constants.Uris;
using Telegram.Bot.Types.Enums;

public class BotIdentityHandler(IOptionsMonitor<BotIdentityOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, GraphServiceClient graph, Dgmjr.Graph.Abstractions.IDirectoryObjectsService directoryObjectsService, Dgmjr.Graph.Abstractions.IUsersService usersService, TelegramB2CDbContext<TelegramB2CUser> db)  : AuthenticationHandler<BotIdentityOptions>(options, logger, encoder, clock)
{
    private IActionContextAccessor ActionContextAccessor => Request.HttpContext.RequestServices.GetRequiredService<IActionContextAccessor>();
    private IUrlHelperFactory UrlHelperFactory => Request.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
    private IUrlHelper UrlHelper => UrlHelperFactory.GetUrlHelper(ActionContextAccessor.ActionContext);
    private DGraphExtensionProperty TelegramIdProperty => new(TelegramIdPropertyName);
    private string TelegramIdPropertyName => Format(DGraphExtensionProperty.FormatString, directoryObjectsService.ExtensionsAppClientId.ToString("N"), Constants.ExtensionProperties.TelegramId);
    private DGraphExtensionProperty TelegramUsernameProperty => new (TelegramUsernamePropertyName);
    private string TelegramUsernamePropertyName => Format(DGraphExtensionProperty.FormatString, directoryObjectsService.ExtensionsAppClientId.ToString("N"), Constants.ExtensionProperties.TelegramUsername);
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if(Request.Path != "/" + Options.Route)
        {
            return await Task.FromResult(AuthenticateResult.NoResult());
        }
        Request.EnableBuffering();
        Request.Body.Seek(0, SeekOrigin.Begin);
        var body = await new StreamReader(Request.Body).ReadToEndAsync();
        Request.Body.Seek(0, SeekOrigin.Begin);
        var update = DeserializeObject<Update>(body);
        if(update.Type is not UpdateType.Message and not UpdateType.EditedMessage and not UpdateType.ChannelPost and not UpdateType.EditedChannelPost)
        {
            return await Task.FromResult(AuthenticateResult.NoResult());
        }
        var telegramUser = (update.Message ?? update.EditedMessage ?? update.ChannelPost ?? update.EditedChannelPost).From;
        var b2cUser = await db.Users.FirstOrDefaultAsync(u => u.TelegramId == telegramUser.Id);
        List<Claim> claims =
        [
            new(Ct.Name, telegramUser.Username ?? telegramUser.Id.ToString()),
            new(Ct.NameIdentifier, telegramUser.Id.ToString()),
            new(UserId.Name, telegramUser.Id.ToString()),
            new(UserId.UriString, telegramUser.Id.ToString()),
            new(FirstName.Name, telegramUser.FirstName ?? telegramUser.Id.ToString()),
            new(FirstName.UriString, telegramUser.FirstName ?? telegramUser.Id.ToString()),
            new(LastName.Name, telegramUser.LastName ?? telegramUser.Id.ToString()),
            new(LastName.UriString, telegramUser.Id.ToString()),
            new(Language.Name, telegramUser.LanguageCode ?? "en"),
            new(Language.UriString, telegramUser.LanguageCode ?? "en"),
            new(Username.Name, telegramUser.Username ?? telegramUser.Id.ToString()),
            new(Username.UriString, telegramUser.Username ?? telegramUser.Id.ToString())
        ];
        var graphUser = (await graph.Users.Request().Filter($"{TelegramIdPropertyName} eq '{telegramUser.Id}'").GetAsync()).FirstOrDefault();
        if(graphUser is null)
        {
            graphUser = new User
            {
                DisplayName = telegramUser.Username,
                UserPrincipalName = $"{telegramUser.Id}@{options.CurrentValue.Domain}",
                OnPremisesImmutableId = telegramUser.Id.ToString(),
                Identities = new List<ObjectIdentity>
                {
                    new ()
                    {
                        SignInType = nameof(SignInType.Federated).ToLower(),
                        Issuer = Uris.TelegramOidcServer,
                        IssuerAssignedId = telegramUser.Id.ToString()
                    },
                    new()
                    {
                        SignInType = nameof(SignInType.UserName).ToLower(),
                        Issuer = options.CurrentValue.Domain,
                        IssuerAssignedId = telegramUser.Username ?? telegramUser.Id.ToString()
                    }
                },
                PasswordProfile = new PasswordProfile
                {
                    ForceChangePasswordNextSignIn = true,
                    Password = guid.NewGuid().ToString("N") + "!"
                }
            };
            await graph.Users.Request().AddAsync(graphUser);
        }
        if(b2cUser is null)
        {
            b2cUser = new TelegramB2CUser
            {
                Id = new(graphUser.Id),
                TelegramId = telegramUser.Id,
                TelegramUsername = telegramUser.Username
            };
            await db.Users.AddAsync(b2cUser);
            await db.SaveChangesAsync();
        }
        claims.Add(new(Ct.Upn, graphUser.UserPrincipalName));
        claims.Add(new(Ct.NameIdentifier, graphUser.Id));
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return await Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
