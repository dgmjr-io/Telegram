namespace Telegram.Bot.Components;

using Telegram.Bot.Components.Extensions;

using TelegramUserDataFactory = Func<UserData>;

public class UserData : INotifyPropertyChanged
{
    public UserData()
    {
        PropertyChanged += (sender, e) => { }; // Do nothing
    }

    private string? _firstName;
    [JsonProperty("first_name")]
    [JProp("first_name")]
    public string? FirstName { get => _firstName; set { _firstName = value; OnPropertyChanged(); } }

    private string? _lastName;
    [JsonProperty("last_name")]
    [JProp("last_name")]
    public string? LastName { get => _lastName; set { _lastName = value; OnPropertyChanged(); } }

    private string? _username;
    [JsonProperty("username")]
    [JProp("username")]
    public string? Username { get => _username; set { _username = value; OnPropertyChanged(); } }

    private string? _languageCode;
    [JsonProperty("language_code")]
    [JProp("language_code")]
    public string? LanguageCode { get => _languageCode; set { _languageCode = value; OnPropertyChanged(); } }

    private long _id;
    [JsonProperty("id")]
    [JProp("id")]
    public long Id { get => _id; set { _id = value; OnPropertyChanged(); } }

    private bool _isBot;
    [JsonProperty("is_bot")]
    [JProp("is_bot")]
    public bool IsBot { get => _isBot; set { _isBot = value; OnPropertyChanged(); } }

    private bool _isPremium;
    [JsonProperty("is_premium")]
    [JProp("is_premium")]
    public bool IsPremium { get => _isPremium; set { _isPremium = value; OnPropertyChanged(); } }

    private string? _photoUrl;
    [JsonProperty("photo_url")]
    [JProp("photo_url")]
    public string? PhotoUrl { get => _photoUrl; set { _photoUrl = value; OnPropertyChanged(); } }

    private string? _biography;
    [JsonProperty("biography")]
    [JProp("biography")]
    public string? Biography { get => _biography; set { _biography = value; OnPropertyChanged(); } }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public string ToJson()
    {
        return SerializeObject(this);
    }

    public static UserData FromJson(string json)
    {
        return DeserializeObject<UserData>(json);
    }
}

public class UserDataAccessor(UserState userState)
{
    public IStatePropertyAccessor<UserData> Accessor { get; } = userState.CreateProperty<UserData>(Telegram);
    public UserState UserState => userState;

    public const string Telegram = "telegram";

    public async Task<UserData> GetAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
    {
        return await Accessor.GetAsync(turnContext, NewUserDataFactory(turnContext), cancellationToken);
    }

    protected virtual TelegramUserDataFactory NewUserDataFactory(ITurnContext turnContext)
    {
        return () =>
        {
            var userData = new UserData();
            var telegramChannelData = turnContext.GetTelegramChannelData();
            telegramChannelData.AssignTo(userData);
            Accessor.SetAsync(turnContext, userData);
            UserState.SaveChangesAsync(turnContext, cancellationToken: default);

            userData.PropertyChanged += (sender, e) =>
            {
                if (sender is UserData userData)
                {
                    Accessor.SetAsync(turnContext, userData);
                    UserState.SaveChangesAsync(turnContext, cancellationToken: default);
                }
            };
            return userData;
        };
    }

    public virtual async Task SetUserDataAsync(ITurnContext turnContext, string key, object value, CancellationToken cancellationToken = default)
    {
        var state = await UserState.CreateProperty<Dictionary<string, object>>(nameof(UserData)).GetAsync(turnContext, () => new Dictionary<string, object>(), cancellationToken);
        state[key] = value;
    }

    public virtual async Task<T?> GetUserDataAsync<T>(ITurnContext turnContext, string key, CancellationToken cancellationToken = default)
    {
        var state = await UserState.CreateProperty<Dictionary<string, object>>(nameof(UserData)).GetAsync(turnContext, () => new Dictionary<string, object>(), cancellationToken);
        return state.TryGetValue(key, out var value) ? (T)value : default;
    }

    public virtual async Task DeleteUserDataAsync(ITurnContext turnContext, string key, CancellationToken cancellationToken = default)
    {
        var state = await UserState.CreateProperty<Dictionary<string, object>>(nameof(UserData)).GetAsync(turnContext, () => new Dictionary<string, object>(), cancellationToken);
        state.Remove(key);
    }
}
