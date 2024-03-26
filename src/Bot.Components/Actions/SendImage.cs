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
public class SendImage() : TelegramBotCustomAction(DeclarativeTypeConst)
{
    public new const string DeclarativeTypeConst = $"{Constants.Namespace}.{nameof(SendImage)}";

    public override string Kind => DeclarativeTypeConst;

    [JsonProperty("image")]
    [JProp("image")]
    public virtual FileExpression Image { get; set; }

    [JsonProperty("caption")]
    [JProp("caption")]
    public virtual StrExp Caption { get; set; }

    public override async Task<DialogTurnResult> BeginDialogAsync(
        DialogContext dc,
        object? options = default,
        CancellationToken cancellationToken = default
    )
    {
        Debug.Assert(Image != null, "The image is not valid.");
        var recipientId = RecipientId.GetValue(dc.State);
        var token = BotApiToken.GetValue(dc);
        Console.WriteLine($"Sending image to {recipientId} with token {token}");
        var message = await CallBotAsync(
            dc,
            bot =>
                bot.SendPhotoAsync(
                    chatId: recipientId,
                    photo: Image.AsInputFile(dc),
                    caption: ParseMode.GetValue(dc.State)
                        is ParseModeEnum.Markdown
                            or ParseModeEnum.MarkdownV2
                        ? Caption.GetValue(dc.State).EscapeMarkdown()
                        : Caption.GetValue(dc.State),
                    parseMode: ParseMode.GetValue(dc.State),
                    messageThreadId: MessageThreadId?.GetValue(dc.State),
                    disableNotification: DisableNotification?.GetValue(dc.State) ?? false,
                    protectContent: ProtectContent?.GetValue(dc.State) ?? false,
                    cancellationToken: cancellationToken
                )
        );

        return await dc.EndDialogAsync(message, cancellationToken: cancellationToken);
    }
}
