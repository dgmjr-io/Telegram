namespace Telegram.Bot.Components.Extensions;

using Telegram.Bot.Components;

public static class TelegramChannelDataExtensions
{
    public const string NotATelegramChannel = "Not a Telegram channel";
    public const string NotATelegramUser = "Not a Telegram user";

    private static readonly TelegramChannelData NotATelegramChannelData =
        new()
        {
            Message = new Message
            {
                From = new MessageFrom
                {
                    Id = -1,
                    FirstName = NotATelegramUser,
                    Username = NotATelegramUser,
                    IsBot = false
                },
                Chat = new Chat { Id = -1, Type = ChatType.Private.ToString() }
            },
            From = new TelegramChannelDataFrom
            {
                Id = -1,
                FirstName = NotATelegramUser,
                Username = NotATelegramUser,
                IsBot = false
            }
        };

    public static TelegramChannelData GetTelegramChannelData(this ITurnContext turnContext)
    {
        return DeserializeObject<TelegramChannelData>(turnContext.Activity.ChannelData.ToString())
            ?? NotATelegramChannelData;
    }

    public static UserData AssignTo(this TelegramChannelDataFrom from, UserData userData)
    {
        if (from is not null)
        {
            userData.Id = from.Id;
            userData.FirstName = from.FirstName;
            userData.LastName = from.LastName;
            userData.Username = from.Username;
            userData.LanguageCode = from.LanguageCode;
            userData.IsBot = from.IsBot;
        }
        return userData;
    }

    public static UserData AssignTo(this MessageFrom from, UserData userData)
    {
        if (from is not null)
        {
            userData.Id = from.Id;
            userData.FirstName = from.FirstName;
            userData.LastName = from.LastName;
            userData.Username = from.Username;
            userData.IsBot = from.IsBot;
            userData.LanguageCode = from.LanguageCode;
            userData.IsPremium = from.IsPremium;
        }
        return userData;
    }

    public static UserData AssignTo(
        this TelegramChannelData channelData,
        UserData userData,
        IBotTelemetryClient? telemetryClient = null
    )
    {
        try
        {
            channelData?.Message.From.AssignTo(userData);
        }
        catch (Exception ex)
        {
            // don't let this bork the whole thing if it fails
            telemetryClient?.TrackException(
                ex,
                new StringDictionary
                {
                    [nameof(channelData)] = channelData?.ToString(),
                    [nameof(userData)] = userData?.ToString()
                }
            );
        }
        return userData!;
    }
}
