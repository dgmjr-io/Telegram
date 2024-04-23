namespace Telegram.Bot.Components.Models;

using System.Collections;

public class Letter2CustomEmojiMap : IDictionary<string, long>
{
    private readonly IDictionary<string, long> _map = new Dictionary<string, long>();

    public long this[string key]
    {
        get => _map[key];
        set => _map[key] = value;
    }

    public ICollection<string> Keys => _map.Keys;

    public ICollection<long> Values => _map.Values;

    public int Count => _map.Count;

    public bool IsReadOnly => _map.IsReadOnly;

    public void Add(string key, long value)
    {
        _map.Add(key, value);
    }

    public void Add(KeyValuePair<string, long> item)
    {
        _map.Add(item);
    }

    public void Clear()
    {
        _map.Clear();
    }

    public bool Contains(KeyValuePair<string, long> item)
    {
        return _map.Contains(item);
    }

    public bool ContainsKey(string key)
    {
        return _map.ContainsKey(key);
    }

    public void CopyTo(KeyValuePair<string, long>[] array, int arrayIndex)
    {
        _map.CopyTo(array, arrayIndex);
    }

    public IEnumerator<KeyValuePair<string, long>> GetEnumerator()
    {
        return _map.GetEnumerator();
    }

    public bool Remove(string key)
    {
        return _map.Remove(key);
    }

    public bool Remove(KeyValuePair<string, long> item)
    {
        return _map.Remove(item);
    }

    public bool TryGetValue(string key, out long value)
    {
        return _map.TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
