namespace Telegram.Bot.Components.Actions;
using AdaptiveExpressions.Properties;

using Microsoft.Bot.Builder.Dialogs;

using Newtonsoft.Json;

using Telegram.Bot;
using Telegram.Bot.Types.Enums;

using Constants = Telegram.Bot.Components.Constants;

[CustomAction(DeclarativeTypeConst)]
public class SendTextMessage() : TelegramBotCustomAction(DeclarativeTypeConst)
{
    public new const string DeclarativeTypeConst = $"{Constants.Namespace}.{nameof(SendTextMessage)}";

    public override string Kind => DeclarativeTypeConst;

    [JsonProperty("text")]
    [JProp("text")]
    public virtual StrExp Message { get; set; }

    public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object? options = default, CancellationToken cancellationToken = default)
    {
        Debug.Assert(Message != null, "The message is null.");
        Debug.Assert(RecipientId != null, "The RecipientId is null.");
        Debug.Assert(BotApiToken != null, "The BotApiToken is null.");
        var recipientId = RecipientId.GetValue(dc.State);
        var token = BotApiToken.GetValue(dc.State);
        Console.WriteLine($"Sending text message to {recipientId} with token {token}");
        var message = await CallBotAsync(dc, bot => bot.SendTextMessageAsync(
            chatId: GetChatId(dc),
            text: ParseMode.GetValue(dc.State) is ParseModeEnum.Markdown or ParseModeEnum.MarkdownV2 ? Message.GetValue(dc.State).EscapeMarkdown() : Message.GetValue(dc.State),
            parseMode: ParseMode.GetValue(dc.State),
            messageThreadId: MessageThreadId?.GetValue(dc.State),
            disableNotification: DisableNotification?.GetValue(dc.State) ?? false,
            protectContent: ProtectContent?.GetValue(dc.State) ?? false,
            cancellationToken: cancellationToken
        ));

        return await dc.EndDialogAsync(message, cancellationToken: cancellationToken);
    }
}
