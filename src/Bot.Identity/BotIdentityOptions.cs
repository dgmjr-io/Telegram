using System;

using Telegram.Bot.Types;
using BotApiToken = Telegram.Bot.Types.BotApiToken;
using Telegram.Bot.Configuration;
namespace Telegram.Bot.Identity;

public class BotIdentityOptions : Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions, IBotConfiguration
{
    public BotApiToken BotApiToken => BotApiToken.From(Token);

    public string Token { get; init; } = default!;

    public Uri? HostAddress { get; set; }
    public string Route { get; set; }
    public string SecretToken  { get; init; }
    public string Domain { get; init; }
}
