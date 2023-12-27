namespace Telegram.Bot.Configuration;

#pragma warning disable CA1050 // Declare types in namespaces
#pragma warning disable RCS1110 // Declare type inside namespace.
#pragma warning disable S3903 // Types should be defined in named namespaces
public class BotConfiguration(string token)  : TelegramBotClientOptions(token)
#pragma warning restore S3903 // Types should be defined in named namespaces
#pragma warning restore RCS1110 // Declare type inside namespace.
#pragma warning restore CA1050 // Declare types in namespaces
{
    public static readonly string Key = nameof(BotConfiguration);

    public BotConfiguration(TelegramBotClientOptions options) : this(options.Token) { }

    [BotApiToken]
    public BotApiToken BotApiToken => BotApiToken.Parse(Token);
    public Uri HostAddress { get; set; } = default!;
    public string Route { get; set; } = default!;
    public string SecretToken { get; init; } = guid.NewGuid().ToString();
}
