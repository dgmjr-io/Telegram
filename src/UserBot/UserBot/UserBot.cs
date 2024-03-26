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

using Microsoft.AspNetCore.Session;

using Telegram.UserBot.Config;
using Telegram.UserBot.Store.Abstractions;
using WTelegram;
using TL;

public class UserBot : Client, IUserBot
{
    public virtual ILogger? Logger { get; }
    public virtual User Me { get; protected set; }
    public virtual IDictionary<long, User> Users { get; } = new Dictionary<long, User>();
    public virtual IDictionary<long, ChatBase> Chats { get; } = new Dictionary<long, ChatBase>();
    public event MessageHandler OnMessageReceived;

    public UserBot(IOptions<UserBotConfig> cfg, ILogger<UserBot> logger, IUserBotStore sessionStore)
        : this(cfg.Value, logger, sessionStore) { }

    public UserBot(IUserBotConfig cfg, ILogger<UserBot> logger, IUserBotStore sessionStore)
        : this(cfg.GetConfigVariable, sessionStore.GetStream(), logger) { }

    public UserBot(
        Func<string, string?> config,
        Stream? store = null,
        ILogger<UserBot>? logger = null
    )
        : base(config, store)
    {
        Logger = logger;
        OnUpdate += Client_OnUpdate;
        OnOther += Client_OnOther;
        OnMessageReceived += Client_OnMessageReceived;
        OnUpdateDeleteChannelMessages += async udcm =>
            Logger?.ChatMessagesDeleted(Chats[udcm.channel_id], udcm.messages.Length);
        OnUpdateDeleteMessages += async udm =>
            Logger?.MessagesDeleted(udm.messages.Length);
        OnUpdateUserTyping += async uut =>
            Logger?.UserAction(Users[uut.user_id], uut.action.GetType().Name);
        OnUpdateChatUserTyping += async ucut =>
            Logger?.ChatUserTyping(Users[ucut.from_id.ID], Chats[ucut.chat_id]);
        OnUpdateChannelUserTyping += async ucut2 =>
            Logger?.ChatUserTyping(Users[ucut2.from_id.ID], Chats[ucut2.channel_id]);
        OnUpdateChatParticipants += async ucp =>
            Logger?.ChatParticipants(ucp.participants.ChatId, ucp.participants.Participants.Length);
        OnUpdateUserStatus += async uus =>
            Logger?.UserStatus(Users[uus.user_id], uus.status);
        OnUpdateUserName += async uun =>
            Logger?.UserChangedProfileName(Users[uun.user_id], uun.first_name, uun.last_name);
        OnUpdateUser += async uu =>
            Logger?.UserChangedInfos(Users[uu.user_id]);
        OnOtherUpdate += async u =>
            Logger?.UnhandledUpdate(u);
        OnUpdateGroupCallParticipants += async ugcp =>
            Logger?.GroupCallParticipants(ugcp.call.id, ugcp.participants.Length);
    }

    protected virtual Task Client_OnOther(IObject obj) => Task.CompletedTask;

    protected virtual async Task Client_OnUpdate(UpdatesBase updates)
    {
        updates.CollectUsersChats(Users, Chats);
        if (updates is UpdateShortMessage usm && !Users.ContainsKey(usm.user_id))
        {
            (
                await this.Updates_GetDifference(usm.pts - usm.pts_count, usm.date, 0)
            ).CollectUsersChats(Users, Chats);
        }
        else if (
            updates is UpdateShortChatMessage uscm
            && (!Users.ContainsKey(uscm.from_id) || !Chats.ContainsKey(uscm.chat_id))
        )
        {
            (
                await this.Updates_GetDifference(uscm.pts - uscm.pts_count, uscm.date, 0)
            ).CollectUsersChats(Users, Chats);
        }

        foreach (var update in updates.UpdateList)
        {
            switch (update)
            {
                case UpdateNewMessage unm:
                    await OnMessageReceived(unm.message);
                    break;
                case UpdateEditMessage uem:
                    await OnMessageReceived(uem.message, true);
                    break;
                // Note: UpdateNewChannelMessage and UpdateEditChannelMessage are also handled by above cases
                case UpdateDeleteChannelMessages udcm:
                    await OnUpdateDeleteChannelMessages(udcm);
                    break;
                case UpdateDeleteMessages udm:
                    await OnUpdateDeleteMessages(udm);
                    break;
                case UpdateUserTyping uut:
                    await OnUpdateUserTyping(uut);
                    break;
                case UpdateChatUserTyping ucut:
                    await OnUpdateChatUserTyping(ucut);
                    break;
                case UpdateChannelUserTyping ucut2:
                    await OnUpdateChannelUserTyping(ucut2);
                    break;
                case UpdateChatParticipants { participants: ChatParticipants cp } ucp:
                    await OnUpdateChatParticipants(ucp);
                    break;
                case UpdateUserStatus uus:
                    await OnUpdateUserStatus(uus);
                    break;
                case UpdateUserName uun:
                    await OnUpdateUserName(uun);
                    break;
                case UpdateUser uu:
                    await OnUpdateUser(uu);
                    break;
                case UpdateGroupCallParticipants ugcp:
                    await OnUpdateGroupCallParticipants(ugcp);
                    break;
                default:
                    await OnOtherUpdate(update);
                    break; // there are much more update types than the above example cases
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
                update
                    is UpdateNewMessage
                    {
                        message: Message { peer_id: PeerUser { user_id: var user_id } } msg
                    }
                && msg.flags.HasFlag(Message.Flags.out_)
                && Users.TryGetValue(user_id, out var user)
            )
            {
                Logger?.NewMessageFromUser(user, msg);
                if (msg.message.Equals("Ping", OrdinalIgnoreCase))
                    await SendMessageAsync(user, "Pong");
            }
        }
    }

    protected virtual async Task Client_OnMessageReceived(
        MessageBase messageBase,
        bool edit = false
    )
    {
        // if (edit)
        //     Console.Write("(Edit): ");

        switch (messageBase)
        {
            case Message m:
                Logger?.MessageReceived(m, this.GetPeerName(m.from_id), this.GetPeerName(m.peer_id));
                break;
            case MessageService ms:
                Logger?.UserAction(ms, this.GetPeerName(ms.from_id), this.GetPeerName(ms.peer_id), ms.action.GetType().Name[13..]);
                break;
        }
    }

    public event UpdateHandler<UpdateNewMessage> OnUpdateNewMessage;
    public event UpdateHandler<UpdateEditMessage> OnUpdateEditMessage;
    public event UpdateHandler<UpdateDeleteChannelMessages> OnUpdateDeleteChannelMessages;
    public event UpdateHandler<UpdateDeleteMessages> OnUpdateDeleteMessages;
    public event UpdateHandler<UpdateUserTyping> OnUpdateUserTyping;
    public event UpdateHandler<UpdateChatUserTyping> OnUpdateChatUserTyping;
    public event UpdateHandler<UpdateChannelUserTyping> OnUpdateChannelUserTyping;
    public event UpdateHandler<UpdateChatParticipants> OnUpdateChatParticipants;
    public event UpdateHandler<UpdateUserStatus> OnUpdateUserStatus;
    public event UpdateHandler<UpdateUserName> OnUpdateUserName;
    public event UpdateHandler<UpdateUser> OnUpdateUser;
    public event UpdateHandler<UpdateGroupCallParticipants> OnUpdateGroupCallParticipants;
    public event UpdateHandler<Update> OnOtherUpdate;
}

public delegate Task MessageHandler(MessageBase messageBase, bool edit = false);
public delegate Task UpdateHandler<in T>(T update)
    where T : Update;
