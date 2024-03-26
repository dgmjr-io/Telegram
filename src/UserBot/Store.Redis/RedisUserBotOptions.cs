namespace Telegram.UserBot.Store.Redis;

public class RedisUserBotOptions : Dgmjr.Redis.Extensions.RedisCacheOptions
{
    public string Key { get; set; }
}
