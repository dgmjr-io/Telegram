using OneOf;

using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions;

public static partial class LoggingExtensions
{
    [LoggerMessage(1, LogLevel.Information, "Received {updateType} from {ChatId}", EventName = nameof(ReceivedUpdateFrom))]
    public static partial void ReceivedUpdateFrom(this ILogger logger, UpdateType updateType, OneOf<long, string> chatId);

    [LoggerMessage(1, LogLevel.Trace, "Received update {UpdateType}\n{Update}", EventName = nameof(ReceivedUpdate))]
    public static partial void ReceivedUpdate(this ILogger logger, Telegram.Bot.Types.Enums.UpdateType updateType, Telegram.Bot.Types.Update update);

    public static void ReceivedUpdate(this ILogger logger, Telegram.Bot.Types.Update update)
    {
        logger.ReceivedUpdate(update.Type, update);
        switch(update.Type)
        {
            case Telegram.Bot.Types.Enums.UpdateType.Message:
                logger.LogInformation("Received message from {ChatId}", update.Message.Chat.Id);
                break;
            case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:
                logger.LogInformation("Received callback query from {ChatId}", update.CallbackQuery.Message.Chat.Id);
                break;
            case Telegram.Bot.Types.Enums.UpdateType.InlineQuery:
                logger.LogInformation("Received inline query from {ChatId}", update.InlineQuery.From.Id);
                break;
            case Telegram.Bot.Types.Enums.UpdateType.ChosenInlineResult:
                logger.LogInformation("Received chosen inline result from {ChatId}", update.ChosenInlineResult.From.Id);
                break;
            case Telegram.Bot.Types.Enums.UpdateType.EditedMessage:
                logger.LogInformation("Received edited message from {ChatId}", update.EditedMessage.Chat.Id);
                break;
            case Telegram.Bot.Types.Enums.UpdateType.ChannelPost:
                logger.LogInformation("Received channel post from {ChatId}", update.ChannelPost.Chat.Id);
                break;
            case Telegram.Bot.Types.Enums.UpdateType.EditedChannelPost:
                logger.LogInformation("Received edited channel post from {ChatId}", update.EditedChannelPost.Chat.Id);
                break;
            case Telegram.Bot.Types.Enums.UpdateType.ShippingQuery:
                logger.LogInformation("Received shipping query from {ChatId}", update.ShippingQuery.From.Id);
                break;
            case Telegram.Bot.Types.Enums.UpdateType.PreCheckoutQuery:
                logger.LogInformation("Received pre-checkout query from {ChatId}", update.PreCheckoutQuery.From.Id);
                break;
            case Telegram.Bot.Types.Enums.UpdateType.Poll:
                logger.LogInformation("Received poll {PollId} from chat {ChatId}", update.Poll.Id, update.Message.Chat.Id);
                break;
            case Telegram.Bot.Types.Enums.UpdateType.PollAnswer:
                logger.LogInformation("Received poll answer from {ChatId}", update.PollAnswer.User.Id);
                break;
            case Telegram.Bot.Types.Enums.UpdateType.MyChatMember:
                logger.LogInformation("Received chat member update from {ChatId}", update.MyChatMember.Chat.Id);
                break;
            case Telegram.Bot.Types.Enums.UpdateType.ChatMember:
                logger.LogInformation("Received chat member update from {ChatId}", update.ChatMember.Chat.Id);
                break;
            default:
                logger.LogInformation("Received unknown update from {ChatId}", update.Message.Chat.Id);
                break;
        }
    }
}
