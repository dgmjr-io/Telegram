namespace Telegram.Bot.Components.Expressions;

using AdaptiveExpressions;
using AdaptiveExpressions.Properties;
using Microsoft.AspNetCore.Routing.Tree;

using Newtonsoft.Json.Linq;

[CustomExpression(Kind)]
public class ChatIdExpression : ExpressionProperty<ChatId>
{
    [JsonProperty("$kind")]
    [JProp("$kind")]
    public const string Kind = $"{Constants.Namespace}.{nameof(ChatIdExpression)}";

    public ChatIdExpression(string expressionOrString)
        : base(expressionOrString) { }

    public ChatIdExpression(Expression expression)
        : base(expression) { }

    public ChatIdExpression(Func<object, object> expression)
        : base(Expression.Lambda(expression)) { }

    public ChatIdExpression(JToken expressionOrValue)
        : base(expressionOrValue) { }

    public static implicit operator ChatIdExpression(string value)
    {
        return new ChatIdExpression(value);
    }

    public static implicit operator ChatIdExpression(IntExp value)
    {
        return new ChatIdExpression(value.Value);
    }

    public static implicit operator ChatIdExpression(long value)
    {
        return new ChatIdExpression(value);
    }

    public static implicit operator ChatIdExpression(ChatId value)
    {
        return new ChatIdExpression(value.ToString());
    }

    public ChatId GetChatIdValue(object context)
    {
        var value = GetValue(context);
        return value;
    }

    public override void SetValue(object? value)
    {
        if (value is Expression or long or int or ChatId)
        {
            base.SetValue(value.ToString());
            return;
        }
        value = value is JValue value1 ? value1.Value : value;
        var text = value?.ToString();
        if (text == null)
        {
            return;
        }
        if (text.StartsWith("="))
        {
            ExpressionText = text;
            return;
        }
        if (text.StartsWith("\\=", Ordinal))
        {
            text = text.TrimStart('\\');
        }

        ExpressionText = "=`" + text.Replace("`", "\\`") + "`";
    }
}
