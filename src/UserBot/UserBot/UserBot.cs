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

using Dgmjr.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.UserBot.Config;
using Telegram.UserBot.Store.Abstractions;
using TL;

public class UserBot : WT.Client, ILog, IUserBot
{
    public virtual ILogger Logger { get; protected set; }
    public virtual User Me { get; protected set; }
    public IDictionary<long, User> Users { get; } = new Dictionary<long, User>();
    static IDictionary<long, ChatBase> Chats { get; } = new Dictionary<long, ChatBase>();

    public UserBot(IOptions<IUserBotConfig> cfg, ILogger<UserBot> logger) : this(cfg.Value, logger)
    {
        
    }
    public UserBot(IUserBotConfig cfg, ILogger<UserBot> logger) : this(cfg.GetConfigVariable, cfg.GetSessionStore().GetStream(), logger)
    {
        
    }

    public UserBot(Func<string, string?> config, Stream store, ILogger<UserBot> logger) : base(config, store)
    {
        Logger = logger;
        OnUpdate += Client_OnUpdate;
    }

    protected virtual async Task Client_OnUpdate(IObject arg)
    {
        if (arg is not UpdatesBase updates) return;
        updates.CollectUsersChats(Users, Chats);
        foreach (var update in updates.UpdateList)
        {
            WriteLine(update.GetType().Name);
            if (update is UpdateNewMessage { message: Message { peer_id: PeerUser { user_id: var user_id } } msg }) // private message
                if (!msg.flags.HasFlag(Message.Flags.out_)) // ignore our own outgoing messages
                    if (Users.TryGetValue(user_id, out var user))
                    {
                        WriteLine($"New message from {user}: {msg.message}");
                        if (msg.message.Equals("Ping", OrdinalIgnoreCase))
                            await SendMessageAsync(user, "Pong");
                    }
        }
    }
}
