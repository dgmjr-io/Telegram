namespace Telegram.UserBot.Store;

using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;

using Telegram.UserBot.Store.Abstractions;
using Telegram.UserBot.Store.Redis;

internal class RedisUserBotStore(RedisUserBotOptions options, string? sessionName = "session") : IUserBotStore
{
    private const string Telegram_UserBot_Store_Redis = "Telegram.UserBot.Store.Redis";

    private RedisUserBotOptions Options => options;
    private RedisCache Cache => new (Options);

    public Stream GetStream()
    {
        return new ObservableMemoryStream(Cache, $"{Telegram_UserBot_Store_Redis}.{Options.Key}.{sessionName}");
    }

    private sealed class ObservableMemoryStream(RedisCache cache, string key) : MemoryStream
    {
        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            cache.Remove(key);
            cache.Set(key, ToArray());
        }
    }
}
