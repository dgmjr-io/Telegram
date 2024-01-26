namespace Telegram.OpenIdConnect.Telemetry;

public static class Activities
{
    private static readonly Version AssemblyVersion = typeof(Activities).Assembly.GetName().Version;

    /// <summary>
    /// Base ActivitySource
    /// </summary>
    public static readonly ActivitySource BasicActivitySource =
        new(TraceNames.Basic, ServiceVersion);

    /// <summary>
    /// Store ActivitySource
    /// </summary>
    public static readonly ActivitySource StoreActivitySource =
        new(TraceNames.Store, ServiceVersion);

    /// <summary>
    /// Cache ActivitySource
    /// </summary>
    public static readonly ActivitySource CacheActivitySource =
        new(TraceNames.Cache, ServiceVersion);

    /// <summary>
    /// Cache ActivitySource
    /// </summary>
    public static readonly ActivitySource ServiceActivitySource =
        new(TraceNames.Services, ServiceVersion);

    /// <summary>
    /// Detailed validation ActivitySource
    /// </summary>
    public static readonly ActivitySource ValidationActivitySource =
        new(TraceNames.Validation, ServiceVersion);

    /// <summary>
    /// Service version
    /// </summary>
    public static readonly string ServiceVersion =
        $"{AssemblyVersion.Major}.{AssemblyVersion.Minor}.{AssemblyVersion.Build}";
}

public static class TraceNames
{
    /// <summary>
    /// Service name for base traces
    /// </summary>
    public const string Basic = "Telegram.OpenIdConnect.Server";

    /// <summary>
    /// Service name for store traces
    /// </summary>
    public const string Store = Basic + ".Stores";

    /// <summary>
    /// Service name for caching traces
    /// </summary>
    public const string Cache = Basic + ".Cache";

    /// <summary>
    /// Service name for caching traces
    /// </summary>
    public const string Services = Basic + ".Services";

    /// <summary>
    /// Service name for detailed validation traces
    /// </summary>
    public const string Validation = Basic + ".Validation";

    public static readonly string ServiceVersion = Activities.ServiceVersion;
}
