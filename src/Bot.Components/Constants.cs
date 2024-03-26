namespace Telegram.Bot.Components;

public static class Constants
{
    public const string Namespace = "Telegram";
    public const string TelegramBot = nameof(TelegramBot);
    public const string Token = nameof(Token);
    public const string ForwardMessages = nameof(ForwardMessages);
    public const string TranscriptRecipientId = nameof(TranscriptRecipientId);
    public const string TokenKey = $"{TelegramBot}:{Token}";
    public const string ForwardMessagesKey = $"{TelegramBot}:{ForwardMessages}";
    public const string TranscriptRecipientKey = $"{TelegramBot}:{TranscriptRecipientId}";
    public const string Expression = "expression";
}
