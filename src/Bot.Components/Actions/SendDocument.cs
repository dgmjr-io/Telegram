using Telegram.Bot.Components.Expressions;

namespace Telegram.Bot.Components.Actions;

[CustomAction(DeclarativeTypeConst)]
public class SendDocument() : TelegramBotCustomAction(DeclarativeTypeConst)
{
    public new const string DeclarativeTypeConst = $"{Constants.Namespace}.{nameof(SendDocument)}";

    public override string Kind => DeclarativeTypeConst;

    [JsonProperty("caption")]
    public virtual StrExp Caption { get; set; }

    [JsonProperty("document")]
    [JProp("document")]
    public FileExpression Document { get; set; }

    public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object? options = default, CancellationToken cancellationToken = default)
    {
        Debug.Assert(Document != null, "The document is not valid.");
        var recipientId = RecipientId.GetValue(dc.State);
        var token = BotApiToken.GetValue(dc);
        Console.WriteLine($"Sending document to {recipientId} with token {token}");
        var message = await CallBotAsync(dc, bot => bot.SendDocumentAsync(
            chatId: recipientId,
            document: Document.AsInputFile(dc),
            messageThreadId: MessageThreadId?.GetValue(dc.State),
            caption: ParseMode.GetValue(dc.State) is ParseModeEnum.Markdown or ParseModeEnum.MarkdownV2 ? Caption.GetValue(dc.State).EscapeMarkdown() : Caption.GetValue(dc.State),
            parseMode: ParseMode.GetValue(dc.State),
            protectContent: ProtectContent?.GetValue(dc.State) ?? false,
            cancellationToken: cancellationToken
        ));

        return await dc.EndDialogAsync(message, cancellationToken: cancellationToken);
    }
}
