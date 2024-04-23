namespace Telegram.Bot.Components.Middleware;

using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Bot.Schema;

using Newtonsoft.Json;

using NuGet.Configuration;

using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

using Constants = Constants;
using Convert = System.Convert;
using IMiddleware = Microsoft.Bot.Builder.IMiddleware;

public partial class MessageForwardingMiddleware(
    ITelegramBotClient bot,
    IConfiguration configuration,
    IBotTelemetryClient telemetryClient,
    MsBotUserState userState
) : IBotMiddleware
{
    private const string ConfigurationKey =
        nameof(Telegram) + "." + nameof(Bot) + "." + nameof(Components);
    private TelegramBotComponentSettings Settings =>
        configuration.GetSection(ConfigurationKey).Get<TelegramBotComponentSettings>() ?? new();
    private long TranscriptRecipient => Settings.TranscriptGroupId;
    private bool ForwardMessages => Settings.ForwardMessages;
    private const string MessageIdRegexString = @"^(?<MessageId>(\d+))-(?<ChatId>(\d+))-.*$";
    private const string ChatId = nameof(ChatId);
    private const string MessageId = nameof(MessageId);
    private const string TranscriptForumId = nameof(TranscriptForumId);
    private IStatePropertyAccessor<int> _forumIdAccessor;

#if NET7_0_OR_GREATER
    [GeneratedRegex(MessageIdRegexString)]
    private partial Regx MessageIdRegex();
#else
    private readonly Regx _messageIdRegex = new(MessageIdRegexString);

    private Regx MessageIdRegex() => _messageIdRegex;
#endif

    public virtual async Task OnTurnAsync(
        ITurnContext turnContext,
        NextDelegate next,
        CancellationToken cancellationToken = default
    )
    {
        var telegramChannelData = DeserializeObject<TelegramChannelData>(
            SerializeObject(turnContext.Activity.GetChannelData<TelegramChannelData>())
        );
        _forumIdAccessor = userState.CreateProperty<int>(TranscriptForumId);
        var forumId = await _forumIdAccessor.GetAsync(turnContext, () => -1, cancellationToken);
        if (forumId == -1 && ForwardMessages)
        {
            var forumTopic = await CreateForumTopicAsync(
                TranscriptRecipient,
                name: telegramChannelData.Message.From.Username
            );
            forumId = forumTopic.MessageThreadId;
            await _forumIdAccessor.SetAsync(turnContext, forumId, cancellationToken);
            await userState.SaveChangesAsync(turnContext, force: true, cancellationToken);
        }

        turnContext.OnSendActivities(OnSendActivities);
        await next(cancellationToken);
    }

    protected virtual async Task<ForumTopic> CreateForumTopicAsync(ChatId chatId, string name)
    {
        return await bot.MakeRequestAsync(
                new CreateForumTopicRequest(chatId, name) { IconColor = new Color() }
            )
            .ConfigureAwait(continueOnCapturedContext: false);
    }

    protected virtual async Task<ResourceResponse[]> OnSendActivities(
        ITurnContext turnContext,
        List<Activity> activities,
        Func<Task<ResourceResponse[]>> next
    )
    {
        var telegramChannelData = DeserializeObject<TelegramChannelData>(
            SerializeObject(turnContext.Activity.GetChannelData<TelegramChannelData>())
        );
        var forumId = await _forumIdAccessor.GetAsync(turnContext, () => -1);
        var responses = await next();

        var matchingResponses = responses
            .Where(r => r.Id != null && MessageIdRegex().IsMatch(r.Id))
            .ToList();
        if (ForwardMessages && !activities.Exists(a => a.Type == ActivityTypes.Trace))
        {
            if (
                turnContext.Activity.Type == ActivityTypes.Message
                && turnContext.Activity.ChannelData is not null
            )
            {
                telemetryClient.TrackEvent(
                    Constants.MessageForwardingMiddleware,
                    new StringDictionary
                    {
                        { Constants.MessageId, telegramChannelData.Message.MessageId.ToString() },
                        { Constants.ChatId, telegramChannelData.Message.Chat.Id.ToString() },
                        {
                            Constants.MessageThreadId,
                            telegramChannelData.Message.Chat.Id.ToString()
                        },
                        { Constants.TranscriptGroupId, TranscriptRecipient.ToString() }
                    }
                );
                await ForwardMessageAsync(
                    TranscriptRecipient,
                    telegramChannelData.Message.Chat.Id,
                    messageId: Convert.ToInt32(telegramChannelData.Message.MessageId),
                    messageThreadId: forumId
                );
            }
            foreach (var matchingResponse in matchingResponses)
            {
                var match = MessageIdRegex().Match(matchingResponse.Id);
                var messageId = Convert.ToInt32(match.Groups[MessageId].Value);
                var chatId = Convert.ToInt64(match.Groups[ChatId].Value);

                telemetryClient.TrackEvent(
                    Constants.MessageForwardingMiddleware,
                    new StringDictionary
                    {
                        { Constants.MessageId, messageId.ToString() },
                        { Constants.ChatId, chatId.ToString() },
                        {
                            Constants.MessageThreadId,
                            telegramChannelData.Message.Chat.Id.ToString()
                        },
                        { Constants.TranscriptGroupId, TranscriptRecipient.ToString() }
                    }
                );
                await ForwardMessageAsync(
                    TranscriptRecipient,
                    chatId,
                    messageId: messageId,
                    messageThreadId: forumId
                );
            }
        }
        // if(_forwardMessages)
        // {
        //     foreach(var activity in activities)
        //     {
        //         switch(activity)
        //         {
        //             case Activity a when a.Type == ActivityTypes.Message && a.ChannelData is not null:
        //                 var telegramChannelData = JsonConvert.DeserializeObject<TelegramChannelData>(JsonConvert.SerializeObject(turnContext.Activity.ChannelData));
        //                 await bot.ForwardMessageAsync(
        //                     _transcriptRecipient,
        //                     new ChatId(telegramChannelData.Message.Chat.Id),
        //                     Convert.ToInt32(telegramChannelData.Message.MessageId)
        //                 );
        //                 break;
        //             case Activity f when f.Type == ActivityTypes.Message && f.ChannelData is null:
        //                 await bot.SendTextMessageAsync(
        //                     _transcriptRecipient,
        //                     $"Bot to user: {f.Text}"
        //                 );
        //                 break;
        //             case Activity b when b.Attachments?.Any(att => att.ContentType.Contains("image", OrdinalIgnoreCase) || att.ContentType.Contains("video", OrdinalIgnoreCase)) ?? false:
        //                 var attachment = b.Attachments.First(att => att.ContentType.Contains("image", OrdinalIgnoreCase) || att.ContentType.Contains("video", OrdinalIgnoreCase));
        //                 if(attachment.ContentType.StartsWith("image", OrdinalIgnoreCase))
        //                 {
        //                     await bot.SendPhotoAsync(
        //                         _transcriptRecipient,
        //                         new InputFileUrl(attachment.ContentUrl),
        //                         caption: b.Text
        //                     );
        //                 }
        //                 else if(attachment.ContentType.StartsWith("video", OrdinalIgnoreCase))
        //                 {
        //                     await bot.SendVideoAsync(
        //                         _transcriptRecipient,
        //                         new InputFileUrl(attachment.ContentUrl),
        //                         caption: b.Text
        //                     );
        //                 }
        //                 break;
        //             case Activity c when c.Attachments?.Any(att => att.Content is HeroCard) ?? false:
        //                 var hc = c.Attachments.First(att => att.Content is HeroCard).Content as HeroCard;
        //                 var img = hc.Images.FirstOrDefault();
        //                 if(img is not null)
        //                 {
        //                     await bot.SendPhotoAsync(
        //                         _transcriptRecipient,
        //                         photo: new InputFileUrl(img?.Url),
        //                         caption: $"<b>{hc.Title}</b>\n{hc.Text}",
        //                         parseMode: ParseModeEnum.Html,
        //                         replyMarkup: new InlineKeyboardMarkup(
        //                             hc.Buttons.Select(btn => new InlineKeyboardButton(btn.Title) { Url = btn.Value?.ToString() }).ToArray()
        //                         )
        //                     );
        //                 }
        //                 else
        //                 {
        //                     await bot.SendTextMessageAsync(
        //                         _transcriptRecipient,
        //                         $"<b>{hc.Title}</b>\n{hc.Text}",
        //                         parseMode: ParseModeEnum.Html,
        //                         replyMarkup: new InlineKeyboardMarkup(
        //                             hc.Buttons.Select(btn => new InlineKeyboardButton(btn.Title) { Url = btn.Value?.ToString() }).ToArray()
        //                         )
        //                     );
        //                 }
        //                 break;
        //             case Activity d when d.Attachments?.Any(att => att.Content is VideoCard) ?? false:
        //                 var vc = d.Attachments.First(att => att.Content is VideoCard).Content as VideoCard;
        //                 await bot.SendMediaGroupAsync(
        //                     _transcriptRecipient,
        //                     media: vc.Media.Select(vid => new InputMediaVideo(new InputFileUrl(vid.Url)) { Caption = $"{vc.Title}\n{vc.Text}" })
        //                 );
        //                 break;
        //             case Activity e when e.Attachments?.Any(att => att.Content is MediaCard) ?? false:
        //                 var mc = e.Attachments.First(att => att.Content is MediaCard).Content as MediaCard;
        //                 await bot.SendMediaGroupAsync(
        //                     _transcriptRecipient,
        //                     media: mc.Media.Select(vid => new InputMediaVideo(new InputFileUrl(vid.Url)) { Caption = $"{mc.Title}\n{mc.Text}"})
        //                 );
        //                 break;
        //         }
        // if(activity.Type == ActivityTypes.Message)
        // {
        //     var telegramChannelData = JsonConvert.DeserializeObject<TelegramChannelData>(JsonConvert.SerializeObject(turnContext.Activity.ChannelData));
        //     await bot.ForwardMessageAsync(
        //         _transcriptRecipient,
        //         new ChatId(telegramChannelData.Message.Chat.Id),
        //         Convert.ToInt32(telegramChannelData.Message.MessageId)
        //     );
        // }
        // }
        // }
        return responses;
    }

    private async Task ForwardMessageAsync(
        ChatId chatId,
        long fromChatId,
        int messageId,
        int messageThreadId
    )
    {
        try
        {
            await bot.ForwardMessageAsync(chatId, fromChatId, messageId, messageThreadId);
        }
        // swallow the exception and log it; don't let it bork the entire thing
        catch (Exception ex)
        {
            telemetryClient.TrackException(
                ex,
                new StringDictionary
                {
                    [nameof(chatId)] = chatId.ToString(),
                    [nameof(fromChatId)] = fromChatId.ToString(),
                    [nameof(messageId)] = messageId.ToString(),
                    [nameof(messageThreadId)] = messageThreadId.ToString()
                }
            );
        }
    }
}
