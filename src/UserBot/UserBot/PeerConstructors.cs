namespace Telegram.UserBot;

public static class PeerConstructors
{
    public static User? GetUser(this IUserBot bot, long id) => bot.Users.TryGetValue(id, out var user) ? user : default;
    public static ChatBase? GetChat(this IUserBot bot, long id) => bot.Chats.TryGetValue(id, out var chat) ? chat : default;
    public static InputChannelBase? GetChannel(this IUserBot bot, long id) => bot.Chats.TryGetValue(id, out var chat) && chat is Channel channel ? channel : default;
    public static string? GetUserName(this IUserBot bot, long id) => bot.Users.TryGetValue(id, out var user) ? user.ToString() : $"User {id}";
    public static string? GetChatName(this IUserBot bot, long id) => bot.Chats.TryGetValue(id, out var chat) ? chat.ToString() : $"Chat {id}";
    public static string? GetPeerName(this IUserBot bot, Peer peer) => peer is null ? null : peer is PeerUser user ? bot.GetUserName(user.user_id)
        : peer is PeerChat or PeerChannel ? bot.GetChatName(peer.ID) : $"Peer {peer.ID}";
}
