/*
 * UserBot.cs
 *
 *   Created: 2023-04-25-06:59:03
 *   Modified: 2023-04-25-06:59:03
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022 - 2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.UserBot;

using System.Runtime.Serialization;
using System.Threading.Tasks;

using Dgmjr.Abstractions;

using Telegram.UserBot.Config;
using Telegram.UserBot.Store.Abstractions;
using WTelegram;

public class UserBot : WTelegram.Client, IUserBot
{
    public virtual ILogger? Logger { get; }
    public virtual User Me { get; protected set; }
    public static IDictionary<long, TL.User> Users { get; } = new Dictionary<long, User>();
    public static IDictionary<long, TL.ChatBase> Chats { get; } = new Dictionary<long, ChatBase>();

    public UserBot(IOptions<IUserBotConfig> cfg, ILogger<UserBot> logger)
        : this(cfg.Value, logger) { }

    public UserBot(IUserBotConfig cfg, ILogger<UserBot> logger)
        : this(cfg.GetConfigVariable, cfg.GetSessionStore().GetStream(), logger) { }

    public UserBot(Func<string, string?> config, Stream? store = null, ILogger<UserBot>? logger = null)
        : base(config, store)
    {
        Logger = logger;
        OnUpdate += Client_OnUpdate;
        OnOther += Client_OnOther;
    }

    protected virtual Task Client_OnOther(IObject obj) => Task.CompletedTask;

    protected virtual async Task Client_OnUpdate(UpdatesBase updates)
    {
        updates.CollectUsersChats(Users, Chats);
        foreach (var update in updates.UpdateList)
        {
            Console.WriteLine(update.GetType().Name);
            if (
                update is UpdateNewMessage
                {
                    message: Message { peer_id: PeerUser { user_id: var user_id } } msg
                }
            ) // private message
                if (!msg.flags.HasFlag(Message.Flags.out_)) // ignore our own outgoing messages
                    if (Users.TryGetValue(user_id, out var user))
                    {
                        Logger?.NewMessageFromUser(user, msg);
                        if (msg.message.Equals("Ping", OrdinalIgnoreCase))
                            await SendMessageAsync(user, "Pong");
                    }
        }
    }

    protected virtual async Task Client_OnOwnUpdate(UpdatesBase updates)
    {
        updates.CollectUsersChats(Users, Chats);
        foreach (var update in updates.UpdateList)
        {
            Console.WriteLine(update.GetType().Name);
            if (
                update is UpdateNewMessage
                {
                    message: Message { peer_id: PeerUser { user_id: var user_id } } msg
                }
            ) // private message
                if (msg.flags.HasFlag(Message.Flags.out_)) // our own outgoing messages
                    if (Users.TryGetValue(user_id, out var user))
                    {
                        Logger?.NewMessageFromUser(user, msg);
                        if (msg.message.Equals("Ping", OrdinalIgnoreCase))
                            await SendMessageAsync(user, "Pong");
                    }
        }
    }
}
