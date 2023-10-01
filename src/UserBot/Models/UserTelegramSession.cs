namespace Telegram.UserBot.Models;

public class UserTelegramSession
{
    public long Id { get; set; }

    public string SessionName { get; set; }

    public byte[] Session { get; set; }

    public bool IsActive { get; set; }
}
