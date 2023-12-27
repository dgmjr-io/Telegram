namespace Telegram.UserBot.Store.EntityFrameworkCore.Abstractions;

using Microsoft.EntityFrameworkCore.Abstractions;

// [GenerateInterfaceAttribute(typeof(UserBotDbContext))]
public partial interface IUserBotDbContext : IDbContext<IUserBotDbContext>
{
    DbSet<UserBot.Models.UserTelegramSession> UserTelegramSessions { get; set; }
}
