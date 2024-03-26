namespace Telegram.UserBot;

public static class Extensions
{
    public static void CollectChats(this Messages_Chats chats, IDictionary<long, ChatBase> chatsDict)
    {
        foreach(var chat in chats.chats.Select(chat => chat.Value))
        {
            chatsDict[chat.ID] = chat;
        }
    }
}
