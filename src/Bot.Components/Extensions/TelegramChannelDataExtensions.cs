namespace Telegram.Bot.Components.Extensions;

using Telegram.Bot.Components;

public static class TelegramChannelDataExtensions
{
    public static TelegramChannelData? GetTelegramChannelData(this ITurnContext turnContext)
    {
        return DeserializeObject<TelegramChannelData>(turnContext.Activity.ChannelData.ToString());
    }

    public static UserData AssignTo(this TelegramChannelDataFrom from, UserData userData)
    {
        if(from is not null)
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
        if(from is not null)
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

    public static UserData AssignTo(this TelegramChannelData channelData, UserData userData)
    {
        channelData?.Message.From.AssignTo(userData);
        return userData;
    }
}
