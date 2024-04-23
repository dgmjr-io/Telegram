using Azure.Storage.Blobs.Models;

namespace Telegram.Bot.Components;

public static class Constants
{
    public const string ConfigurationKey =
        nameof(Telegram) + "." + nameof(Bot) + "." + nameof(Components);
    public const string ApiBaseUri = "https://api.telegram.org/bot{0}";
    public const string Namespace = "Telegram";
    public const string TelegramBot = nameof(TelegramBot);
    public const string Token = nameof(Token);
    public const string ForwardMessages = nameof(ForwardMessages);
    public const string TranscriptGroupId = nameof(TranscriptGroupId);
    public const string TokenKey = $"{ConfigurationKey}:{Token}";
    public const string ForwardMessagesKey = $"{ConfigurationKey}:{ForwardMessages}";
    public const string TranscriptRecipientKey = $"{ConfigurationKey}:{TranscriptGroupId}";
    public const string Expression = "expression";
    public const string MessageForwardingMiddleware = "MessageForwardedToTelegram";
    public const string MessageId = nameof(MessageId);
    public const string MessageThreadId = nameof(MessageThreadId);
    public const string ChatId = nameof(ChatId);
    public const string ParseMode = nameof(ParseMode);
    public const string DisableNotification = nameof(DisableNotification);
    public const string ProtectContent = nameof(ProtectContent);
    public const string Message = nameof(Message);
    public const string RecipientId = nameof(RecipientId);
    public const string BotApiToken = nameof(BotApiToken);
    public const string TelegramApiCall = nameof(TelegramApiCall);
    public const string ForwardMessage = nameof(ForwardMessage);
    public const string GetUser = nameof(GetUser);
    public const string SendDocument = nameof(SendDocument);
    public const string SendTextMessage = nameof(SendTextMessage);
    public const string SendImage = nameof(SendImage);
    public const string SendVideo = nameof(SendVideo);

    public static class Events
    {
        public const string ContinueConversation = nameof(ContinueConversation);
    }
}
