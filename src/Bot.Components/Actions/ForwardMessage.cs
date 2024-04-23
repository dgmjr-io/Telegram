namespace Telegram.Bot.Components.Actions;

using AdaptiveExpressions.Properties;

using Microsoft.Bot.Builder.Dialogs;

using Newtonsoft.Json;

using Telegram.Bot;
using Telegram.Bot.Components.Expressions;
using Telegram.Bot.Types.Enums;

using Constants = Telegram.Bot.Components.Constants;

[CustomAction(DeclarativeTypeConst)]
public class ForwardMessage(
    IBotTelemetryClient telemetryClient,
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0
)
    : TelegramBotCustomAction<ForwardMessage>(
        telemetryClient,
        DeclarativeTypeConst,
        sourceFilePath,
        sourceLineNumber
    )
{
    public new const string DeclarativeTypeConst =
        $"{Constants.Namespace}.{nameof(ForwardMessage)}";

    public override string Kind => DeclarativeTypeConst;

    [JsonProperty("fromChatId")]
    [JProp("fromChatId")]
    public virtual ChatIdExpression FromChatId { get; set; }

    [JsonProperty("messageId")]
    [JProp("messageId")]
    public virtual IntExp MessageId { get; set; }

    public override async Task<DialogTurnResult> BeginDialogAsync(
        DialogContext dc,
        object? options = default,
        CancellationToken cancellationToken = default
    )
    {
        Debug.Assert(RecipientId != null, "The RecipientId is null.");
        Debug.Assert(BotApiToken != null, "The BotApiToken is null.");
        var recipientId = RecipientId.GetValue(dc.State);
        var messageId = MessageId.GetValue(dc.State);
        var fromChatId = FromChatId.GetValue(dc.State);
        var token = BotApiToken.GetValue(dc.State);
        Console.WriteLine($"Sending text message to {recipientId} with token {token}");
        var message = await CallBotAsync(
            dc,
            async bot =>
                await bot.ForwardMessageAsync(
                    chatId: GetChatId(dc),
                    fromChatId: fromChatId,
                    messageId: messageId,
                    messageThreadId: MessageThreadId?.GetValue(dc.State),
                    disableNotification: DisableNotification?.GetValue(dc.State) ?? false,
                    protectContent: ProtectContent?.GetValue(dc.State) ?? false,
                    cancellationToken: cancellationToken
                )
        );

        return await dc.EndDialogAsync(message, cancellationToken: cancellationToken);
    }
}
