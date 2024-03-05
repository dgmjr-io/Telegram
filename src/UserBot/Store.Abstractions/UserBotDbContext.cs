// namespace Telegram.UserBot.Store;

// using Telegram.UserBot.Store.Abstractions;
// using Telegram.UserBot.Models;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Abstractions;

// public class UserBotDbContext : DbContext, IUserBotDbContext
// {
//     public const string Schema = "userbots";
//     public const string TableName = "tbl_TelegramSession";
//     public const string VarBinaryMax = "varbinary(max)";

//     public UserBotDbContext(DbContextOptions<UserBotDbContext> options)
//         : base(options) { }

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<UserTelegramSession>(entity =>
//         {
//             entity.ToTable(TableName, Schema);
//             entity.Property(e => e.Id);
//             entity.HasKey(e => e.SessionName);
//             entity.Property(e => e.SessionName).IsRequired();
//             entity.Property(e => e.Session).HasColumnType(VarBinaryMax).IsRequired();
//             entity.Property(e => e.IsActive).HasDefaultValue(true).IsRequired();
//         });
//     }

//     public virtual DbSet<Telegram.UserBot.Models.UserTelegramSession> UserTelegramSessions { get; set; }
// }
