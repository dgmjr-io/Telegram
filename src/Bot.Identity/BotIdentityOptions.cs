namespace Telegram.Bot.Identity;
using System;

using Telegram.Bot.Types;

using BotApiToken = Types.BotApiToken;
using Telegram.Bot.Configuration;

public class BotIdentityOptions : AuthenticationSchemeOptions, IBotConfiguration
{
    public BotApiToken BotApiToken => BotApiToken.From(Token);

    public string Token { get; init; } = default!;

    public Uri? HostAddress { get; set; }
    public string Route { get; set; }
    public string SecretToken  { get; init; }
    public string Domain { get; init; }
}
