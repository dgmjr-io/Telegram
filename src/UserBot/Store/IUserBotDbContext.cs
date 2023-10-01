namespace Telegram.UserBot.Store.Abstractions;

using Microsoft.EntityFrameworkCore.Abstractions;

[GenerateInterfaceAttribute(typeof(UserBotDbContext))]
public partial interface IUserBotDbContext : IDbContext<IUserBotDbContext> { }
