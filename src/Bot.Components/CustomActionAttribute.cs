namespace Telegram.Bot.Components;

/// <summary>
/// Initializes a new instance of the <see cref="CustomActionAttribute"/> class.
/// </summary>
/// <param name="declarativeType">The declarative type for the component registration.</param>
[AttributeUsage(AttributeTargets.Class)]
public class CustomActionAttribute(string declarativeType) : Attribute
{
    /// <summary>
    /// Gets the declarative type for the component registration.
    /// </summary>
    public string DeclarativeType => declarativeType;
}

/// <summary>
/// Initializes a new instance of the <see cref="CustomExpressionAttribute"/> class.
/// </summary>
/// <param name="declarativeType">The declarative type for the component registration.</param>
[AttributeUsage(AttributeTargets.Class)]
public class CustomExpressionAttribute(string declarativeType) : Attribute
{
    /// <summary>
    /// Gets the declarative type for the component registration.
    /// </summary>
    public string DeclarativeType => declarativeType;
}
