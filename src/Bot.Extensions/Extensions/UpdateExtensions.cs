using System.Data;
using System.Text.RegularExpressions;

namespace Telegram.Bot.Extensions;

using System.Linq;

public static partial class UpdateExtensions
{
    private const string BotCommandPattern = @"^\/(?<BotCommand>[a-zA-Z0-9_]+)";
    private const string BotCommandArgs = @"""[^""]*""|'[^']*'|\S+";
    private const string BotCommandArgsWithBotCommand =
        $@"{BotCommandPattern} (?<Args>(?:{BotCommandArgs}[ ]*)*)$";
    private const string QuotedBotCommandArg = @"^[""'](?<BotCommandArg>[^""']*)""$";

#if !NET7_0_OR_GREATER
    private static readonly Regex _BotCommandRegex = new(BotCommandPattern, Rxo.Compiled);

    private static Regex BotCommandRegex() => _BotCommandRegex;

    private static readonly Regex _BotCommandArgsRegex = new(BotCommandArgs, Rxo.Compiled);

    private static Regex BotCommandArgsRegex() => _BotCommandArgsRegex;

    private static readonly Regex _BotCommandArgsWithBotCommandRegex =
        new(BotCommandArgsWithBotCommand, Rxo.Compiled);

    private static Regex BotCommandArgsWithBotCommandRegex() => _BotCommandArgsWithBotCommandRegex;

    private static readonly Regex _QuotedBotCommandArgRegex =
        new(QuotedBotCommandArg, Rxo.Compiled);

    private static Regex QuotedBotCommandArgRegex() => _QuotedBotCommandArgRegex;
#else
    [GeneratedRegex(BotCommandPattern, Rxo.Compiled | Rxo.IgnoreCase | Rxo.ExplicitCapture)]
    private static partial Regex BotCommandRegex();

    [GeneratedRegex(BotCommandArgs, Rxo.Compiled | Rxo.IgnoreCase | Rxo.ExplicitCapture)]
    private static partial Regex BotCommandArgsRegex();

    [GeneratedRegex(QuotedBotCommandArg, Rxo.Compiled | Rxo.IgnoreCase | Rxo.ExplicitCapture)]
    private static partial Regex QuotedBotCommandArgRegex();

    [GeneratedRegex(
        BotCommandArgsWithBotCommand,
        Rxo.Compiled | Rxo.IgnoreCase | Rxo.ExplicitCapture
    )]
    private static partial Regex BotCommandArgsWithBotCommandRegex();
#endif

    public static bool IsBotCommand(this Update update) => update.Message.IsBotCommand();

    public static bool IsBotCommand(this Message message) => message.Text.IsBotCommand();

    private static bool IsBotCommand(this string message) =>
        !IsNullOrEmpty(message.GetBotCommand());

    public static string GetBotCommand(this Update update) => update.Message.GetBotCommand();

    public static string GetBotCommand(this Message message) => message.Text.GetBotCommand();

    private static string GetBotCommand(this string message) =>
        BotCommandRegex().Match(message).Groups["BotCommand"]?.Value ?? string.Empty;

    public static string[] GetArgs(this string message)
    {
        var args = BotCommandArgsWithBotCommandRegex().Match(message).Groups["Args"].Value;
        return BotCommandArgsRegex()
            .Matches(args)
            .OfType<Match>()
            .Select(
                m =>
                    (m.Value.StartsWith("\"") && m.Value.EndsWith("\""))
                    || (m.Value.StartsWith("'") && m.Value.EndsWith("'"))
                        ? m.Value.Substring(1, m.Value.Length - 2)
                        : m.Value
            )
            .WhereNotNull()
            .ToArray();
    }

    public static string[] GetArgs(this Update update) => update.Message.GetArgs();

    public static string[] GetArgs(this Message message) => message.Text.GetArgs();
}
