namespace Telegram.Bot.Configuration;

public interface IBotConfiguration
{
    string? Token { get; }
    BotApiToken BotApiToken { get; }
    Uri? HostAddress { get; set; }
    string Route { get; set; }
    string SecretToken { get; init; }
}
