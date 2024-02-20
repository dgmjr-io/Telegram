namespace Telegram.Bot.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Dgmjr.AzureAdB2C.Identity;


public class TelegramB2CDbContext<TUser>(DbContextOptions options) : AzureAdB2CDbContext<TUser>(options)
    where TUser : TelegramB2CUser
{
    // public override DbSet<TUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<TelegramB2CUser>(builder =>
        {
            builder.HasOne<AzureAdB2CUser>().WithOne().HasForeignKey<TelegramB2CUser>(u => u.Id).HasPrincipalKey<AzureAdB2CUser>(u => u.Id);
            builder.ToTable(Constants.TableNames.TelegramB2CUser, Constants.Schemas.TeleSchema);
            // builder.HasAlternateKey(u => u.TelegramId);
            builder.Property(u => u.TelegramId).IsRequired();
            builder.Property(u => u.TelegramUsername);
            builder.HasIndex(u => u.TelegramId).IsUnique();
            builder.HasIndex(u => u.TelegramUsername);//.IsUnique();
        });

        builder.Entity<AzureAdB2CUser>(builder =>
        {
            builder.ToTable(Dgmjr.AzureAdB2C.Identity.TableNames.AzureAdB2CUser, Dgmjr.AzureAdB2C.Identity.Schemas.AzureAdB2C);
            builder.HasIndex(u => u.TenantId);
        });
    }
}
