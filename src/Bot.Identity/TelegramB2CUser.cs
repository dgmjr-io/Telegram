namespace Telegram.Bot.Identity;

[Table(Constants.TableNames.TelegramB2CUser, Schema = Constants.Schemas.TeleSchema)]
public class TelegramB2CUser : Dgmjr.AzureAdB2C.Identity.AzureAdB2CUser, IHaveATelegramId, IHaveATelegramUsername
{
    public long TelegramId { get; set; }
    public string? TelegramUsername { get; set; }
}
