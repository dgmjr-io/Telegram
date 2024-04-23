namespace Telegram.Bot.Components.Expressions;

using AdaptiveExpressions.Properties;

using Expression = AdaptiveExpressions.Expression;

[CustomExpression(Kind)]
public class ColorExpression : ExpressionProperty<ColorBox>
{
    public const string Kind = $"{Constants.Namespace}.{nameof(ColorExpression)}";

    public ColorExpression(byte[] value)
        : this(value as object) { }

    public ColorExpression(string expressionOrString)
        : base(expressionOrString) { }

    public ColorExpression(Expression expression)
        : base(expression) { }

    public ColorExpression(Func<object, object> expression)
        : base(Expression.Lambda(expression)) { }

    public ColorExpression(JToken expressionOrValue)
        : base(expressionOrValue) { }

    public ColorExpression(Color value)
        : base(value) { }

    public ColorExpression(ColorBox value)
        : base(value.Value) { }

    public ColorExpression(object value)
        : base(value) { }

    public ColorExpression()
        : base(Color.BlueColor) { }

    public static implicit operator ColorExpression(Color value) => new(value);

    public static implicit operator ColorExpression(string expressionOrString) =>
        new(expressionOrString);

    public static implicit operator ColorExpression(Expression expression) => new(expression);

    public static implicit operator ColorExpression(byte[] value) => new(value);

    public virtual int GetRed(object state) => GetValue(state).Value.Red;

    public virtual int GetGreen(object state) => GetValue(state).Value.Green;

    public virtual int GetBlue(object state) => GetValue(state).Value.Blue;
}
