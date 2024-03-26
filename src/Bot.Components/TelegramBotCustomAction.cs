namespace Telegram.Bot.Components;

using System.Threading;
using System.Threading.Tasks;

using AdaptiveExpressions.Properties;

using Microsoft.Bot.Builder.Dialogs;

using NetTopologySuite.Utilities;

using Telegram.Bot.Components.Expressions;

public delegate Task<T> CallBot<T>(ITelegramBotClient bot);

public abstract class TelegramBotCustomAction(string kind): Dialog
{
    protected TelegramBotCustomAction() : this(DeclarativeTypeConst) {}

    public const string DeclarativeTypeConst = $"{Constants.Namespace}.{nameof(TelegramBotCustomAction)}";

    /// <summary>
    /// Gets the declarative type of the custom action.
    /// </summary>
    [JsonProperty("$kind")]
    [JProp("$kind")]
    public virtual string Kind => kind;

    /// <summary>Gets or sets the bot API token.</summary>
    [JsonProperty("botApiToken")]
    [JProp("botApiToken")]
    public virtual StrExp BotApiToken { get; set; }

    [JsonProperty("recipientId")]
    [JProp("recipientId")]
    public virtual ChatIdExpression RecipientId { get; set; }

    [JsonProperty("parseMode")]
    [JProp("parseMode")]
    public virtual EnumExpression<ParseModeEnum> ParseMode { get; set; } = ParseModeEnum.MarkdownV2;

    [JsonProperty("disableNotification")]
    [JProp("disableNotification")]
    public virtual BoolExpression DisableNotification { get; set; } = false;

    [JsonProperty("replyToMessageId")]
    [JProp("replyToMessageId")]
    public virtual IntExp? ReplyToMessageId { get; set; } = null;

    [JsonProperty("messageThreadId")]
    [JProp("messageThreadId")]
    public virtual IntExp? MessageThreadId { get; set; } = null;

    [JsonProperty("protectContent")]
    [JProp("protectContent")]
    public virtual BoolExpression? ProtectContent { get; set; } = false;

    [JsonProperty("disabled")]
    [JProp("disabled")]
    public virtual BoolExpression? IsDisabled { get; set; } = false;

    /// <summary>
    /// Gets or sets the path to which we store the result from the output of this custom action.
    /// </summary>
    [JsonProperty("resultProperty")]
    public StrExp ResultProperty { get; set; }

    protected virtual ChatId GetChatId(DialogContext dc) =>
        RecipientId.GetChatIdValue(dc.State);

    protected virtual async Task<T> CallBotAsync<T>(DialogContext dc, CallBot<T> action)
    {
        var botApiToken = BotApiToken.GetValue(dc.State);
        var bot = new TelegramBotClient(botApiToken);
        bot.OnMakingApiRequest += OnMakingApiRequest;
        bot.OnApiResponseReceived += OnApiResponseReceived;
        Assert.IsTrue(bot != null, "The bot API token is not valid.");
        var result = await action(bot);

        if (ResultProperty != null)
        {
            dc.State.SetValue(ResultProperty.GetValue(dc.State), result);
        }

        return result;
    }

    protected virtual ValueTask OnApiResponseReceived(ITelegramBotClient bot, ApiResponseEventArgs args, CancellationToken cancellationToken)
    {
        Trace.TraceInformation($"Received response from {args.ApiRequestEventArgs.Request.MethodName}");
        return new ValueTask();
    }

    protected virtual ValueTask OnMakingApiRequest(ITelegramBotClient bot, ApiRequestEventArgs args, CancellationToken cancellationToken)
    {
        Trace.TraceInformation($"Making request to {args.Request.MethodName}");
        switch(args.Request)
        {
            case SendMessageRequest sendMessageRequest:
                Trace.TraceInformation($"Sending message to {sendMessageRequest.ChatId}");
                break;
            case ForwardMessageRequest forwardMessageRequest:
                Trace.TraceInformation($"Forwarding message to {forwardMessageRequest.ChatId}");
                break;
            case SendPhotoRequest sendPhotoRequest:
                Trace.TraceInformation($"Sending photo to {sendPhotoRequest.ChatId}");
                break;
            case SendDocumentRequest sendDocumentRequest:
                Trace.TraceInformation($"Sending document to {sendDocumentRequest.ChatId}");
                break;
            case SendVideoRequest sendVideoRequest:
                Trace.TraceInformation($"Sending video to {sendVideoRequest.ChatId}");
                break;
            case SendAnimationRequest sendAnimationRequest:
                Trace.TraceInformation($"Sending animation to {sendAnimationRequest.ChatId}");
                break;
            case SendVoiceRequest sendVoiceRequest:
                Trace.TraceInformation($"Sending voice to {sendVoiceRequest.ChatId}");
                break;
            case SendVideoNoteRequest sendVideoNoteRequest:
                Trace.TraceInformation($"Sending video note to {sendVideoNoteRequest.ChatId}");
                break;
            case SendMediaGroupRequest sendMediaGroupRequest:
                Trace.TraceInformation($"Sending media group to {sendMediaGroupRequest.ChatId}");
                break;
            case SendLocationRequest sendLocationRequest:
                Trace.TraceInformation($"Sending location to {sendLocationRequest.ChatId}");
                break;
            case SendVenueRequest sendVenueRequest:
                Trace.TraceInformation($"Sending venue to {sendVenueRequest.ChatId}");
                break;
            case SendContactRequest sendContactRequest:
                Trace.TraceInformation($"Sending contact to {sendContactRequest.ChatId}");
                break;
            case SendPollRequest sendPollRequest:
                Trace.TraceInformation($"Sending poll to {sendPollRequest.ChatId}");
                break;
            case SendDiceRequest sendDiceRequest:
                Trace.TraceInformation($"Sending dice to {sendDiceRequest.ChatId}");
                break;
            case SendChatActionRequest sendChatActionRequest:
                Trace.TraceInformation($"Sending chat action to {sendChatActionRequest.ChatId}");
                break;
            case GetUserProfilePhotosRequest getUserProfilePhotosRequest:
                Trace.TraceInformation($"Getting user profile photos for {getUserProfilePhotosRequest.UserId}");
                break;
            case GetFileRequest getFileRequest:
                Trace.TraceInformation($"Getting file {getFileRequest.FileId}");
                break;
            case BanChatMemberRequest banChatMemberRequest:
                Trace.TraceInformation($"Kicking chat member {banChatMemberRequest.ChatId}");
                break;
            case UnbanChatMemberRequest unbanChatMemberRequest:
                Trace.TraceInformation($"Unbanning chat member {unbanChatMemberRequest.ChatId}");
                break;
            case RestrictChatMemberRequest restrictChatMemberRequest:
                Trace.TraceInformation($"Restricting chat member {restrictChatMemberRequest.ChatId}");
                break;
            case PromoteChatMemberRequest promoteChatMemberRequest:
                Trace.TraceInformation($"Promoting chat member {promoteChatMemberRequest.ChatId}");
                break;
            case SetChatAdministratorCustomTitleRequest setChatAdministratorCustomTitleRequest:
                Trace.TraceInformation($"Setting chat administrator custom title {setChatAdministratorCustomTitleRequest.ChatId}");
                break;
            case SetChatPermissionsRequest setChatPermissionsRequest:
                Trace.TraceInformation($"Setting chat permissions {setChatPermissionsRequest.ChatId}");
                break;
            case ExportChatInviteLinkRequest exportChatInviteLinkRequest:
                Trace.TraceInformation($"Exporting chat invite link {exportChatInviteLinkRequest.ChatId}");
                break;
            case SetChatPhotoRequest setChatPhotoRequest:
                Trace.TraceInformation($"Setting chat photo {setChatPhotoRequest.ChatId}");
                break;
            case DeleteChatPhotoRequest deleteChatPhotoRequest:
                Trace.TraceInformation($"Deleting chat photo {deleteChatPhotoRequest.ChatId}");
                break;
            case SetChatTitleRequest setChatTitleRequest:
                Trace.TraceInformation($"Setting chat title {setChatTitleRequest.ChatId}");
                break;
            case SetChatDescriptionRequest setChatDescriptionRequest:
                Trace.TraceInformation($"Setting chat description {setChatDescriptionRequest.ChatId}");
                break;
            case PinChatMessageRequest pinChatMessageRequest:
                Trace.TraceInformation($"Pinning chat message {pinChatMessageRequest.ChatId}");
                break;
            case UnpinChatMessageRequest unpinChatMessageRequest:
                Trace.TraceInformation($"Unpinning chat message {unpinChatMessageRequest.ChatId}");
                break;
            case LeaveChatRequest leaveChatRequest:
                Trace.TraceInformation($"Leaving chat {leaveChatRequest.ChatId}");
                break;
            case GetChatRequest getChatRequest:
                Trace.TraceInformation($"Getting chat {getChatRequest.ChatId}");
                break;
            case GetChatAdministratorsRequest getChatAdministratorsRequest:
                Trace.TraceInformation($"Getting chat administrators {getChatAdministratorsRequest.ChatId}");
                break;
            case GetChatMemberCountRequest getChatMembersCountRequest:
                Trace.TraceInformation($"Getting chat members count {getChatMembersCountRequest.ChatId}");
                break;
            case GetChatMemberRequest getChatMemberRequest:
                Trace.TraceInformation($"Getting chat member {getChatMemberRequest.ChatId}");
                break;
            case SetChatStickerSetRequest setChatStickerSetRequest:
                Trace.TraceInformation($"Setting chat sticker set {setChatStickerSetRequest.ChatId}");
                break;
            case DeleteChatStickerSetRequest deleteChatStickerSetRequest:
                Trace.TraceInformation($"Deleting chat sticker set {deleteChatStickerSetRequest.ChatId}");
                break;
            case AnswerCallbackQueryRequest answerCallbackQueryRequest:
                Trace.TraceInformation($"Answering callback query {answerCallbackQueryRequest.CallbackQueryId}");
                break;
            case SetMyCommandsRequest setMyCommandsRequest:
                Trace.TraceInformation($"Setting my commands: {setMyCommandsRequest.Commands}");
                break;
            case EditMessageTextRequest editMessageTextRequest:
                Trace.TraceInformation($"Editing message text {editMessageTextRequest.ChatId}");
                break;
            case EditMessageCaptionRequest editMessageCaptionRequest:
                Trace.TraceInformation($"Editing message caption {editMessageCaptionRequest.ChatId}");
                break;
            case EditMessageMediaRequest editMessageMediaRequest:
                Trace.TraceInformation($"Editing message media {editMessageMediaRequest.ChatId}");
                break;
            case EditMessageReplyMarkupRequest editMessageReplyMarkupRequest:
                Trace.TraceInformation($"Editing message reply markup {editMessageReplyMarkupRequest.ChatId}");
                break;
            case StopPollRequest stopPollRequest:
                Trace.TraceInformation($"Stopping poll {stopPollRequest.ChatId}");
                break;
            case DeleteMessageRequest deleteMessageRequest:
                Trace.TraceInformation($"Deleting message {deleteMessageRequest.ChatId}");
                break;
            case SendStickerRequest sendStickerRequest:
                Trace.TraceInformation($"Sending sticker {sendStickerRequest.ChatId}");
                break;
            case GetStickerSetRequest getStickerSetRequest:
                Trace.TraceInformation($"Getting sticker set {getStickerSetRequest.Name}");
                break;
            case UploadStickerFileRequest uploadStickerFileRequest:
                Trace.TraceInformation($"Uploading sticker file {uploadStickerFileRequest.UserId}");
                break;
            case CreateNewStickerSetRequest createNewStickerSetRequest:
                Trace.TraceInformation($"Creating new sticker set {createNewStickerSetRequest.UserId}");
                break;
            case AddStickerToSetRequest addStickerToSetRequest:
                Trace.TraceInformation($"Adding sticker to set {addStickerToSetRequest.UserId}");
                break;
            case SetStickerPositionInSetRequest setStickerPositionInSetRequest:
                Trace.TraceInformation($"Setting sticker position in set {setStickerPositionInSetRequest}");
                break;
            case DeleteStickerFromSetRequest deleteStickerFromSetRequest:
                Trace.TraceInformation($"Deleting sticker from set {deleteStickerFromSetRequest}");
                break;
            case SetStickerSetThumbRequest setStickerSetThumbRequest:
                Trace.TraceInformation($"Setting sticker set thumb {setStickerSetThumbRequest.UserId}");
                break;
            case AnswerInlineQueryRequest answerInlineQueryRequest:
                Trace.TraceInformation($"Answering inline query {answerInlineQueryRequest.InlineQueryId}");
                break;
            case SendInvoiceRequest sendInvoiceRequest:
                Trace.TraceInformation($"Sending invoice {sendInvoiceRequest.ChatId}");
                break;
            case AnswerShippingQueryRequest answerShippingQueryRequest:
                Trace.TraceInformation($"Answering shipping query {answerShippingQueryRequest.ShippingQueryId}");
                break;
            case AnswerPreCheckoutQueryRequest answerPreCheckoutQueryRequest:
                Trace.TraceInformation($"Answering pre checkout query {answerPreCheckoutQueryRequest.PreCheckoutQueryId}");
                break;
            case SendGameRequest sendGameRequest:
                Trace.TraceInformation($"Sending game {sendGameRequest.ChatId}");
                break;
            case SetGameScoreRequest setGameScoreRequest:
                Trace.TraceInformation($"Setting game score {setGameScoreRequest.ChatId}");
                break;
            case GetGameHighScoresRequest getGameHighScoresRequest:
                Trace.TraceInformation($"Getting game high scores {getGameHighScoresRequest.ChatId}");
                break;
        }
        return new ValueTask();
    }
}
