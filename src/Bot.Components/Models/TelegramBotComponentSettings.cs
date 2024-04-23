using Telegram.Bot.Components.Expressions;

namespace Telegram.Bot.Components.Models;

public class TelegramBotComponentSettings
{
    public string Token { get; set; }
    public long TranscriptGroupId { get; set; }
    public string RecipientId { get; set; }
    public bool ForwardMessages { get; set; }
    public Letter2CustomEmojiMapExpression Letter2CustomEmojiMap { get; set; }
}
