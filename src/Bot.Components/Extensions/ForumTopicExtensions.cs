namespace Telegram.Bot.Components.Extensions;

public static class ForumTopicExtensions
{
    public static async Task<ForumTopic> CreateForumTopicAsync(
        this ITelegramBotClient bot,
        ChatId chatId,
        string topicName,
        Color iconColor,
        string? customEmojiIcon = null,
        CancellationToken cancellationToken = default
    )
    {
        return await bot.MakeRequestAsync(
            new CreateForumTopicRequest(chatId, topicName)
            {
                IconColor = iconColor /*,
                IconCustomEmojiId = customEmojiIcon?.ToString()*/
            },
            cancellationToken
        );
    }
}
