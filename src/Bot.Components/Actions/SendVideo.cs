namespace Telegram.Bot.Components.Actions;

using System.Threading;
using System.Threading.Tasks;

using AdaptiveExpressions.Properties;

using Microsoft.Bot.Builder.Dialogs;

using Newtonsoft.Json;

using Telegram.Bot.Types;

using JObject = Newtonsoft.Json.Linq.JObject;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Components.Expressions;

[CustomAction(DeclarativeTypeConst)]
public class SendVideo(
    IBotTelemetryClient telemetryClient,
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0
)
    : TelegramBotCustomAction<SendVideo>(
        telemetryClient,
        DeclarativeTypeConst,
        sourceFilePath,
        sourceLineNumber
    )
{
    public new const string DeclarativeTypeConst = $"{Constants.Namespace}.{nameof(SendVideo)}";

    public override string Kind => DeclarativeTypeConst;

    [JsonProperty("video")]
    [JProp("video")]
    public virtual FileExpression Video { get; set; }

    [JsonProperty("caption")]
    [JProp("caption")]
    public virtual StrExp Caption { get; set; }

    public override async Task<DialogTurnResult> BeginDialogAsync(
        DialogContext dc,
        object? options = default,
        CancellationToken cancellationToken = default
    )
    {
        Console.WriteLine($"Sending video: {Video.GetValue(dc.State)}");
        var recipientId = RecipientId.GetValue(dc.State);
        var token = BotApiToken.GetValue(dc.State);
        Console.WriteLine($"Sending video to {recipientId} with token {token}");
        var message = await CallBotAsync(
            dc,
            bot =>
                bot.SendVideoAsync(
                    chatId: RecipientId.GetValue(dc.State),
                    video: Video.AsInputFile(dc),
                    caption: ParseMode.GetValue(dc.State)
                        is ParseModeEnum.Markdown
                            or ParseModeEnum.MarkdownV2
                        ? Caption.GetValue(dc.State).EscapeMarkdown()
                        : Caption.GetValue(dc.State),
                    parseMode: ParseMode.GetValue(dc.State),
                    supportsStreaming: true,
                    messageThreadId: MessageThreadId?.GetValue(dc.State),
                    disableNotification: DisableNotification?.GetValue(dc.State) ?? false,
                    protectContent: ProtectContent?.GetValue(dc.State) ?? false,
                    cancellationToken: cancellationToken
                )
        );

        return await dc.EndDialogAsync(message, cancellationToken: cancellationToken);
    }
}
