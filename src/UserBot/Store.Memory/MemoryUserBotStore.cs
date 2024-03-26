namespace Telegram.UserBot.Store;
using Microsoft.Extensions.Logging;
using Telegram.UserBot.Store.Abstractions;

internal class MemoryUserBotStore : IUserBotStore, ILog
{
    private readonly MemoryStream _stream;
    public ILogger? Logger { get; }

    public MemoryUserBotStore(ILogger<MemoryUserBotStore>? logger = null)
    {
        _stream = new MemoryStream();
        Logger = logger;
    }

    public Stream GetStream()
    {
        Logger?.LogInformation("Getting memory stream for userbot session storage.");
        return _stream;
    }
}
