namespace Telegram.Bot.Extensions;

using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

using Telegram.Bot.Types.Enums;

public static partial class UpdateExtensions
{
    private const string BotCommandPattern = @"^\/(?<BotCommand>[a-zA-Z0-9_]+)";
    private const string BotCommandArgs = @"[""\u201c][^""”\u201d]*[""\u201d]|'[^']*'|\S+";
    private const string BotCommandArgsWithBotCommand =
        $@"{BotCommandPattern} (?<Args>(?:{BotCommandArgs}[ ]*)*)$";
    private const string QuotedBotCommandArg =
        @"^[""'”\u201c](?<BotCommandArg>[^""'”\u201d]*)[""\u201d]$";

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

    public static bool IsBotCommand(this Message message) => Exists(message?.Entities, e => e.Type == MessageEntityType.BotCommand);

    private static bool IsBotCommand(this string message) =>
        !IsNullOrEmpty(message.GetBotCommand());

    public static string GetBotCommand(this Update update) => update.Message?.GetBotCommand();

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
                    || (m.Value.StartsWith("”") && m.Value.EndsWith("”"))
                    || (m.Value.StartsWith("\u201c") && m.Value.EndsWith("\u201d"))
                        ? m.Value.Substring(1, m.Value.Length - 2)
                        : m.Value
            )
            .WhereNotNull()
            .ToArray();
    }

    public static string[] GetArgs(this Update update) => update.Message.GetArgs();

    public static string[] GetArgs(this Message message) => message.Text.GetArgs();

    public static long? GetChatId(this Update update) =>
        update.ChatMember?.From?.Id
        ?? update.ChatMember?.Chat?.Id
        ?? update.InlineQuery?.From?.Id
        ?? update.CallbackQuery?.From?.Id
        ?? update.ChosenInlineResult?.From?.Id
        ?? update.PollAnswer?.User?.Id
        ?? update.ShippingQuery?.From?.Id
        ?? update.ChatJoinRequest?.From?.Id
        ?? update.Message?.Chat?.Id
        ?? update.Message?.From?.Id
        ?? update.EditedMessage?.From?.Id
        ?? update.ChannelPost?.From?.Id
        ?? update.EditedChannelPost?.From?.Id;
}
