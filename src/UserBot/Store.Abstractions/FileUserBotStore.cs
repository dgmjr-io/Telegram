namespace Telegram.UserBot.Store;

using Telegram.UserBot.Store.Abstractions;

internal class FileUserBotStore : IUserBotStore
{
    private readonly string _filePath;

    public FileUserBotStore(string? filePath = null)
    {
        _filePath =
            filePath
            ?? Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), "session.dat");

        if (!File.Exists(_filePath))
        {
            File.Create(_filePath).Close();
        }
    }

    public Stream GetStream()
    {
        return new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
    }
}
