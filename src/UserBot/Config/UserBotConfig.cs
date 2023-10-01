using System;

namespace Telegram.UserBot.Config;

using Telegram.UserBot.Store.Abstractions;
using Telegram.UserBot.Store;
using static Environment;
using Microsoft.EntityFrameworkCore;

public class UserBotConfig
{
    public const string api_hash = nameof(api_hash);
    public const string api_id = nameof(api_id);
    public const string verification_code = nameof(verification_code);
    public const string first_name = nameof(first_name);
    public const string last_name = nameof(last_name);
    public const string password = nameof(password);
    public const string persist_to = nameof(persist_to);
    public const string db_conection_string = nameof(db_conection_string);
    public const string session_pathname = nameof(session_pathname);

    public virtual string Prompt(string variable)
    {
        Write($"Enter {variable}: ");
        return ReadLine();
    }

    public string? GetConfigVariable(string variable) =>
        variable switch
        {
            api_id => (ApiId = long.Parse(ApiId.ToString() ?? Prompt(api_id))).ToString(),
            api_hash => ApiHash ??= Prompt(api_hash),
            verification_code => VerificationCode ??= Prompt(verification_code),
            first_name => FirstName ??= Prompt(first_name),
            last_name => LastName ??= Prompt(last_name),
            password => Password ??= Prompt(password),
            persist_to => PersistTo.ToString(),
            db_conection_string => DbConectionString,
            session_pathname => SessionPathname,
            _ => Prompt(variable)
        };

    public IUserBotStore? GetSessionStore()
    {
        return PersistTo switch
        {
            PersistTo.Memory => new MemoryUserBotStore(),
            PersistTo.File => new FileUserBotStore(SessionPathname),
            PersistTo.Database
                => new DbUserBotStore(
                    new UserBotDbContext(
                        new DbContextOptionsBuilder<UserBotDbContext>()
                            .UseSqlServer(DbConectionString)
                            .Options
                    ),
                    SessionPathname
                ),
            _ => null,
        };
    }

    [JProp((persist_to))]
    public PersistTo PersistTo { get; set; } =
        PersistToWhere.TryParse(GetEnvironmentVariable(persist_to), out var @enum)
            ? @enum.Value
            : default;

    [JProp(db_conection_string)]
    public string DbConectionString { get; set; } = GetEnvironmentVariable(db_conection_string);

    [JProp(api_id)]
    public long ApiId { get; set; } =
        long.TryParse(GetEnvironmentVariable(api_id), out var @long) ? @long : default;

    [JProp(api_hash)]
    public string ApiHash { get; set; } = GetEnvironmentVariable(api_hash);

    [JProp(verification_code)]
    public string VerificationCode { get; set; } = GetEnvironmentVariable(verification_code);

    [JProp(first_name)]
    public string FirstName { get; set; } = GetEnvironmentVariable(first_name);

    [JProp(last_name)]
    public string LastName { get; set; } = GetEnvironmentVariable(last_name);

    [JProp(password)]
    public string Password { get; set; } = GetEnvironmentVariable(password);

    [JProp(session_pathname)]
    public string SessionPathname { get; set; } = GetEnvironmentVariable(session_pathname);
}
