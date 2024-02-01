namespace Telegram.OpenIdConnect.Extensions;

public static class SessionExtensions
{
    public static T? Get<T>(this ISession session, string key, Jso? jso = default)
        where T : class
    {
        var data = session.GetString(key);
        return data is null ? default : Deserialize<T>(data, jso);
    }

    public static void Set<T>(this ISession session, string key, T value, Jso? jso = default)
        where T : class
    {
        session.SetString(key, Serialize(value, jso));
    }

    public static T GetOrCreate<T>(
        this ISession session,
        string key,
        Func<T> factory,
        Jso? jso = default
    )
        where T : class
    {
        var data = session.GetString(key);
        if (data is null)
        {
            var value = factory();
            session.SetString(key, Serialize(value, jso));
            return value;
        }
        return Deserialize<T>(data, jso);
    }
}
