using System;

namespace Telegram.UserBot.Config;

using Telegram.UserBot.Store.Abstractions;
using Telegram.UserBot.Store;
using static Environment;
using Microsoft.EntityFrameworkCore;

public class UserBotConfig : IUserBotConfig
{
#pragma warning disable IDE1006
    public const string api_hash = nameof(api_hash);
    public const string api_id = nameof(api_id);
    public const string verification_code = nameof(verification_code);
    public const string first_name = nameof(first_name);
    public const string last_name = nameof(last_name);
    public const string password = nameof(password);
    public const string persist_to = nameof(persist_to);
    public const string db_connection_string = nameof(db_connection_string);
    public const string redis_connection_string = nameof(redis_connection_string);
    public const string session_pathname = nameof(session_pathname);
    public const string session_key = nameof(session_key);
    public const string server_address = nameof(server_address);
    public const string system_lang_code = nameof(system_lang_code);
    public const string lang_code = nameof(lang_code);
    public const string app_version = nameof(app_version);
    public const string system_version = nameof(system_version);
    public const string device_model = nameof(device_model);
#pragma warning restore IDE1006

    public virtual string? Prompt(string variable)
    {
        Console.Write($"Enter {variable}: ");
        var value = Console.ReadLine();
        return value.IsPresent() ? value : null;
    }

    public string? GetConfigVariable(string variable) =>
        variable switch
        {
#pragma warning disable S1121
            api_id => ApiId ??= Prompt(api_id),
            api_hash => ApiHash ??= Prompt(api_hash),
            verification_code => VerificationCode ??= Prompt(verification_code),
            first_name => FirstName ??= Prompt(first_name),
            last_name => LastName ??= Prompt(last_name),
            password => Password ??= Prompt(password),
#pragma warning restore
            persist_to => PersistTo.ToString(),
            db_connection_string => DbConnectionString,
            session_pathname => SessionPathname,
            _ => Prompt(variable)
        };

    // public IUserBotStore? GetSessionStore()
    // {
    //     return PersistTo switch
    //     {
    //         PersistTo.Memory => new MemoryUserBotStore(),
    //         PersistTo.File => new FileUserBotStore(SessionPathname),
    //         PersistTo.Database
    //             => new DbUserBotStore(
    //                 new UserBotDbContext(
    //                     new DbContextOptionsBuilder<UserBotDbContext>()
    //                         .UseSqlServer(DbConnectionString)
    //                         .Options
    //                 ),
    //                 SessionPathname
    //             ),
    //         PersistTo.Redis => new RedisUserBotStore(
    //             new RedisUserBotOptions { Key = SessionKey, ConnectionString = RedisConnectionString }
    //         ),
    //         _ => null,
    //     };
    // }

    // public GetSessionStore GetSessionStore { get; set; }

    [JProp(persist_to)]
    public virtual PersistTo PersistTo { get; set; } =
        Enum.TryParse<PersistTo>(GetEnvironmentVariable(persist_to), out var @enum)
            ? @enum
            : default;

    [JProp(db_connection_string)]
    public virtual string DbConnectionString { get; set; } = GetEnvironmentVariable(db_connection_string);

    [JProp(api_id)]
    public virtual string ApiId { get; set; } = GetEnvironmentVariable(api_id);

    [JProp(api_hash)]
    public virtual string ApiHash { get; set; } = GetEnvironmentVariable(api_hash);

    [JProp(verification_code)]
    public virtual string VerificationCode { get; set; } = GetEnvironmentVariable(verification_code);

    [JProp(first_name)]
    public string FirstName { get; set; } = GetEnvironmentVariable(first_name);

    [JProp(last_name)]
    public string LastName { get; set; } = GetEnvironmentVariable(last_name);

    [JProp(password)]
    public virtual string Password { get; set; } = GetEnvironmentVariable(password);

    [JProp(session_pathname)]
    public virtual string SessionPathname { get; set; } = GetEnvironmentVariable(session_pathname);

    [JProp(redis_connection_string)]
    public virtual string RedisConnectionString { get; set; } = GetEnvironmentVariable(redis_connection_string);

    [JProp(session_key)]
    public virtual string SessionKey { get; set; } = GetEnvironmentVariable(session_key);

    [JProp(server_address)]
    public virtual string ServerAddress { get; set; } = GetEnvironmentVariable(server_address);

    [JProp(system_lang_code)]
    public virtual string SystemLanguageCode { get; set; } = GetEnvironmentVariable(system_lang_code);

    [JProp(lang_code)]
    public virtual string LanguageCode { get; set; } = GetEnvironmentVariable(lang_code);

    [JProp(app_version)]
    public virtual string AppVersion { get; set; } = GetEnvironmentVariable(app_version);

    [JProp(system_version)]
    public virtual string SystemVersion { get; set; } = GetEnvironmentVariable(system_version);

    [JProp(device_model)]
    public virtual string DeviceModel { get; set; } = GetEnvironmentVariable(device_model);
}
