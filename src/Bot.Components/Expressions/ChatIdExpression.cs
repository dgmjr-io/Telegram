namespace Telegram.Bot.Components.Expressions;

using AdaptiveExpressions.Properties;

using Microsoft.AspNetCore.Routing.Tree;

using Newtonsoft.Json.Linq;

public class ChatIdExpression : ObjectExpression<string>
{
    public ChatIdExpression(string value) : base(value)
    {
    }

    public ChatIdExpression() : this(string.Empty)
    {
    }

    public ChatIdExpression(ChatId value) : this(value.Username ?? value.Identifier.ToString())
    {
    }

    public ChatIdExpression(long value) : base(new JValue(value))
    {
    }

    public ChatIdExpression(JToken value) : base(value.ToString())
    {
    }
    public ChatIdExpression(StrExp value) : base(new JValue(value))
    {
    }

    public static implicit operator ChatIdExpression(string value)
    {
        return new ChatIdExpression(value);
    }

    public static implicit operator ChatIdExpression(IntExp value)
    {
        return new ChatIdExpression(value.Value);
    }

    public static implicit operator ChatIdExpression(ChatId value)
    {
        return new ChatIdExpression(value.ToString());
    }

    public ChatId GetChatIdValue(object context)
        => long.TryParse(GetValue(context), out var id) ? new ChatId(id) : new ChatId(GetValue(context));

    public void SetValue(ChatId value) => Value = value.ToString();
}
