namespace Telegram.UserBot;
using Microsoft.Extensions.Configuration;
using WTelegram;
using Telegram.UserBot;
using Microsoft.Extensions.Logging;
using Dgmjr.Abstractions;

public class ProgramRunner(IUserBot bot, IConfiguration config, Logger<ProgramRunner> logger) : ILog
{
    public ILogger Logger => logger;
    public IConfiguration Configuration => config;
    public IUserBot Bot => bot;

    public async Task RunAsync()
    {
        await Bot.LoginUserIfNeeded();
        Console.WriteLine("Chats:");
        var chats = await ((WT.Client)Bot).Messages_GetAllChats();
        chats.CollectChats(Bot.Chats);

        var backroomChatIds = Configuration
            .GetSection("BackroomChatIds")
            .Get<List<long>>();
        var backroomChats = chats.chats.Where(chat => backroomChatIds.Contains(chat.Value.ID));

        var distinctUsers = new HashSet<long>();
        backroomChats.ForEach(async kvp =>
        {
            try
            {
                var chat = kvp.Value;
                if(chat.IsGroup || chat.IsChannel)
                {
                    if(chat is Chat)
                    {
                        var participants = await ((WTelegram.Client)Bot).Messages_GetFullChat(chat.ID);
                        participants.CollectUsersChats(Bot.Users, Bot.Chats);
                        Logger?.ChatParticipants2(chat.ID, participants.users.Count);
                        foreach(var participantId in participants.users.Select(p => p.Value.ID))
                        {
                            var user = participants.users[participantId];
                            if(!user.IsBot)
                            {
                                distinctUsers.Add(participantId);
                            }
                            Logger?.UserInChat(participantId);
                        }
                    }
                    else if(chat is Channel channel)
                    {
                        Logger?.LogInformation("Processing channel \"{Title}\" {ChatId}, access_hash: {AccessHash}", channel.Title, channel.id, channel.access_hash);
                        var participants = await (Bot as WT.Client).Channels_GetAllParticipants(channel);
                        Logger?.ChannelParticipants(channel.ID, channel.Title, participants.participants.Length);
                        foreach(var participant in participants.participants)
                        {
                            var user = participants.users[participant.UserId];
                            if(!user.IsBot)
                            {
                                distinctUsers.Add(participant.UserId);
                            }
                            if (participant is ChannelParticipantCreator cpc) Logger?.CreatorInChat(participant.UserId, cpc.rank);
                            else if (participant is ChannelParticipantAdmin cpa) Logger?.AdminInChat(participant.UserId, cpa.rank);
                            else if(user.IsBot) Logger?.BotInChat(participant.UserId);
                            else Logger?.UserInChat(participant.UserId);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Logger?.LogError(ex, "There was an error processing chat {ChatId}", kvp.Value.ID);
            }
        });
        Console.WriteLine("Distinct Users:");
        distinctUsers.ForEach(id => Console.WriteLine(id));
    }
}
