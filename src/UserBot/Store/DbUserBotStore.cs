namespace Telegram.UserBot.Store;
using Telegram.UserBot.Store.Abstractions;
using Telegram.UserBot.Models;


internal class DbUserBotStore : Stream, IUserBotStore
{
    private readonly UserBotDbContext _dbContext;
    private readonly string _sessionName;
    private byte[] _data;
    private int _dataLen;
    private DateTime _lastWrite;
    private Task _delayedWrite;

    public Stream GetStream() => this;

    public DbUserBotStore(UserBotDbContext dbContext, string sessionName = null)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _sessionName = sessionName ?? "DefaultSession";

        var session = _dbContext.UserTelegramSessions.FirstOrDefault(
            s => s.SessionName == _sessionName
        );

        if (session != null)
        {
            _dataLen = (_data = session.Session).Length;
        }
        else
        {
            // Create a new session with empty data
            var newSession = new UserTelegramSession()
            {
                SessionName = _sessionName,
                Session = Array.Empty<byte>(),
                IsActive = true
            };

            _dbContext.UserTelegramSessions.Add(newSession);
            _dbContext.SaveChanges();
        }
    }

    protected override void Dispose(bool disposing)
    {
        // Wait for any delayed write to complete before disposing the context
        _delayedWrite?.Wait();

        base.Dispose(disposing);
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        Array.Copy(_data, 0, buffer, offset, count);

        return count;
    }

    public override void Write(byte[] buffer, int offset, int count) // Write call and buffer modifications are done within a lock()
    {
        _data = buffer;
        _dataLen = count;

        if (_delayedWrite != null)
            return;

        var left = 1000 - (int)(DateTime.UtcNow - _lastWrite).TotalMilliseconds;

        if (left < 0)
        {
            var sessionToUpdate = _dbContext.UserTelegramSessions.FirstOrDefault(
                s => s.SessionName == _sessionName
            );

            sessionToUpdate.Session =
                count == buffer.Length ? buffer : buffer[offset..(offset + count)];

            _dbContext.SaveChanges();

            _lastWrite = DateTime.UtcNow;
        }
        else // delay writings for a full second
            _delayedWrite = Task.Delay(left)
                .ContinueWith(t =>
                {
                    lock (this)
                    {
                        _delayedWrite = null;
                        Write(_data, 0, _dataLen);
                    }
                });
    }

    public override long Length => _dataLen;

    public override long Position
    {
        get => 0;
        set { }
    }


    public override bool CanSeek => false;

    public override bool CanRead => true;

    public override bool CanWrite => true;

    public override long Seek(long offset, SeekOrigin origin) => 0;

    public override void SetLength(long value) { }

    public override void Flush() { }
}