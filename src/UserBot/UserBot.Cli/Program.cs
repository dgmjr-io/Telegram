using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Telegram.UserBot;
using TL;

var builder = Host.CreateApplicationBuilder(
    new HostApplicationBuilderSettings
    {
        ApplicationName = "Telegram UserBot CLI",
        EnvironmentName = Environments.Development,
        ContentRootPath = Path.GetDirectoryName(typeof(Program).Assembly.Location),
        Args = args
    }
);
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddLogging(builder => builder.AddConsole());
builder.AddUserBot(_ => { });
builder.Services.AddSingleton<ProgramRunner>();

var app = builder.Build();
await app.StartAsync();
await app.Services.GetRequiredService<ProgramRunner>().RunAsync();
// var bot = app.Services.GetRequiredService<IUserBot>();
// await bot.LoginUserIfNeeded();

// Console.WriteLine("Chats:");
// var chats = await bot.Messages_GetAllChats();

// chats.chats.ForEach(chat => Console.WriteLine(chat.Value.ID + " " + chat.Value.Title));

// var backroomChatIds = app.Services
//     .GetRequiredService<IConfiguration>()
//     .GetSection("BackroomChatIds")
//     .Get<List<long>>();

// var backroomChats = chats.chats.Where(chat => backroomChatIds.Contains(chat.Value.ID));
// Console.WriteLine("Backroom Chats:");
// var distinctUsers = new HashSet<long>();
// backroomChats.ForEach(async kvp =>
// {
//     try
//     {
//         var chat = kvp.Value;
//         if(chat.IsGroup || chat.IsChannel)
//         {
//             if(chat is Chat chat1)
//             {
//                 var participants = await ((WTelegram.Client)bot).Messages_GetFullChat(chat.ID);
//                 foreach(var participant in participants.users)
//                 {
//                     var user = participants.users[participant.Value.ID];
//                     if(!user.IsBot)
//                     {
//                         distinctUsers.Add(participant.Value.ID);
//                     }
//                     Console.WriteLine($"{participant.Value} is in the chat");
//                 }
//             }
//             else if(chat is Channel channel)
//             {
//                 var participants = await bot.Channels_GetAllParticipants(channel);
//                 Logger?.($"{channel.ID} {channel.Title} {participants.participants.Length}");
//                 foreach(var participant in participants.participants)
//                 {
//                     var user = participants.users[participant.UserId];
//                     if(!user.IsBot)
//                     {
//                         distinctUsers.Add(participant.UserId);
//                     }
//                     if (participant is ChannelParticipantCreator cpc) Console.WriteLine($"{user} is the owner '{cpc.rank}'");
//                     else if (participant is ChannelParticipantAdmin cpa) Console.WriteLine($"{user} is admin '{cpa.rank}'");
//                     else if(user.IsBot) Console.WriteLine($"{user} is bot");
//                     else Console.WriteLine(user);
//                 }
//             }
//         }
//     }
//     catch(Exception ex)
//     {
//         Console.WriteLine($"Error: {ex.Message}\n{ex.StackTrace}");
//     }
// });
// Console.WriteLine($"Distinct Users ({distinctUsers.Count}):");
// distinctUsers.ForEach(userId =>
// {
//     var user = bot.Users[userId];
//     Console.WriteLine(user);
// });


// Console.WriteLine("Dialogs:");
// var dialogs = await bot.Messages_GetAllDialogs();
// dialogs.dialogs.ForEach(dialog => Console.WriteLine(dialog.Peer.ID + " " + dialog.TopMessage));

// Console.WriteLine("Chats:");
// bot.Chats.ForEach(chat => Console.WriteLine(chat.Value.ID + " " + chat.Value.Title));

// Console.WriteLine("Users:");
// bot.Users.ForEach(user => Console.WriteLine(user.Value.ID + " " + user.Value.first_name + " " + user.Value.last_name));
