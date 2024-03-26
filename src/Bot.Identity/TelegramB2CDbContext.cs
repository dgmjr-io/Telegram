namespace Telegram.Bot.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Dgmjr.AzureAdB2C.Identity;
using BotApiToken = Types.BotApiToken;
using Microsoft.EntityFrameworkCore.Abstractions;

public class TelegramB2CDbContext<TUser>(DbContextOptions options) : AzureAdB2CDbContext<TUser>(options), IDbContext<TelegramB2CDbContext<TUser>>
    where TUser : TelegramB2CUser
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<TUser>(builder =>
        {
            builder.HasOne<AzureAdB2CUser>().WithOne().HasForeignKey<TUser>(u => u.Id).HasPrincipalKey<AzureAdB2CUser>(u => u.Id);
            builder.ToTable(Constants.TableNames.TelegramB2CUser, Constants.Schemas.TeleSchema);
            builder.Property(u => u.TelegramId).IsRequired();
            builder.Property(u => u.TelegramUsername);
            builder.HasIndex(u => u.TelegramId).IsUnique();
            builder.HasIndex(u => u.TelegramUsername);
            builder.Property(u => u.AccessFailedCount).HasDefaultValue(0);
            builder.Property(u => u.LockoutEnabled).HasDefaultValue(false);
            builder.Property(u => u.PhoneNumberConfirmed).HasDefaultValue(false);
            builder.Property(u => u.TwoFactorEnabled).HasDefaultValue(false);
        });

        builder.Entity<AzureAdB2CUser>(builder =>
        {
            builder.ToTable(TableNames.AzureAdB2CUser, Dgmjr.AzureAdB2C.Identity.Schemas.AzureAdB2C);
            builder.HasIndex(u => u.TenantId);
        });
    }

    public bool IsValidBotToken(string s) => !BotApiToken.Validate(s).ErrorMessage.IsPresent();
}
