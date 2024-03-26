namespace Telegram.Bot.Components.Middleware;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Bot.Schema;

using Newtonsoft.Json;

using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Text.RegularExpressions;

using Constants = Constants;
using Convert = System.Convert;

public partial class MessageForwardingMiddleware(ITelegramBotClient bot, IConfiguration configuration) : IMiddleware
{
    private readonly string _transcriptRecipient = configuration[Constants.TranscriptRecipientKey];
    private readonly bool _forwardMessages = bool.TryParse(configuration[Constants.ForwardMessagesKey], out var fwd) && fwd;
    private const string MessageIdRegexString = @"^(?<MessageId>(\d+))-(?<ChatId>(\d+))-.*$";
    private const string ChatId = nameof(ChatId);
    private const string MessageId = nameof(MessageId);
    [GeneratedRegex(MessageIdRegexString)]
    private partial Regx MessageIdRegex();

    public virtual async Task OnTurnAsync(ITurnContext turnContext, NextDelegate next, CancellationToken cancellationToken = default)
    {
        turnContext.OnSendActivities(OnSendActivities);
        await next(cancellationToken);
    }

    protected virtual async Task<ResourceResponse[]> OnSendActivities(ITurnContext turnContext, List<Activity> activities, Func<Task<ResourceResponse[]>> next)
    {
        var responses = await next();
        var matchingResponses = responses.Where(r => r.Id != null && MessageIdRegex().IsMatch(r.Id)).ToList();
        if(_forwardMessages && !activities.Exists(a => a.Type == ActivityTypes.Trace))
        {
            if(turnContext.Activity.Type == ActivityTypes.Message && turnContext.Activity.ChannelData is not null)
            {
                var telegramChannelData = DeserializeObject<TelegramChannelData>(SerializeObject(turnContext.Activity.ChannelData));
                await bot.ForwardMessageAsync(
                    _transcriptRecipient,
                    telegramChannelData.Message.Chat.Id,
                    Convert.ToInt32(telegramChannelData.Message.MessageId)
                );
            }
            foreach(var matchingResponse in matchingResponses)
            {
                var match = MessageIdRegex().Match(matchingResponse.Id);
                var messageId = Convert.ToInt32(match.Groups[MessageId].Value);
                var chatId = Convert.ToInt64(match.Groups[ChatId].Value);
                await bot.ForwardMessageAsync(
                    _transcriptRecipient,
                    chatId,
                    messageId
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

}
