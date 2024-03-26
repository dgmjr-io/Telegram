namespace Telegram.UserBot.Store.FileSystem;

using Telegram.UserBot.Store.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

public class FileUserBotStore : IUserBotStore, ILog
{
    private readonly string _filePath;
    public ILogger? Logger { get; }
    private readonly FilePersistedUserBotConfig _options;

    public FileUserBotStore(IOptions<FilePersistedUserBotConfig> options, ILogger<FileUserBotStore>? logger = null)
    {
        _options = options.Value;

        _filePath =
            _options.FilePath
            ?? Combine(GetDirectoryName(GetType().Assembly.Location), "session.dat");

        if (!File.Exists(_filePath))
        {
            Create(_filePath).Close();
        }

        Logger = logger;
    }

    public Stream GetStream()
    {
        return new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
    }
}

public class FilePersistedUserBotConfig : Telegram.UserBot.Config.UserBotConfig
{
    public string? FilePath { get; set; }
}
