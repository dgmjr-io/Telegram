namespace Telegram.Bot.Components.Actions;

using Telegram.Bot.Components.Extensions;

[CustomAction(ConstKind)]
public class CreateForumTopic(
    IBotTelemetryClient telemetryClient,
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0
)
    : TelegramBotCustomAction<CreateForumTopic>(
        telemetryClient,
        ConstKind,
        sourceFilePath,
        sourceLineNumber
    )
{
    public const string ConstKind = $"{Constants.Namespace}.{nameof(CreateForumTopic)}";

    [JsonProperty("iconColor")]
    [JProp("iconColor")]
    public virtual ColorExpression Color { get; set; }

    [JsonProperty("title")]
    [JProp("title")]
    public virtual StrExp Title { get; set; }

    [JsonProperty("customEmojiIcon")]
    [JProp("customEmojiIcon")]
    public virtual StrExp CustomEmojiIcon { get; set; }

    public override async Task<DialogTurnResult> BeginDialogAsync(
        DialogContext dc,
        object? options = default,
        CancellationToken cancellationToken = default
    )
    {
        var chatId = RecipientId.GetValue(dc.State);
        var topicName = Title.GetValue(dc.State);
        var iconColor = Color.GetValue(dc.State);
        var customEmojiIcon = CustomEmojiIcon.GetValue(dc.State);
        var user = await CallBotAsync(
            dc,
            async bot =>
                await bot.CreateForumTopicAsync(
                    chatId: chatId,
                    topicName: topicName,
                    iconColor: iconColor,
                    customEmojiIcon: customEmojiIcon,
                    cancellationToken
                )
        );
        return await dc.EndDialogAsync(user, cancellationToken);
    }
}
