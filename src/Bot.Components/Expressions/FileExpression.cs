using AdaptiveExpressions;

namespace Telegram.Bot.Components.Expressions;

using JValue = Newtonsoft.Json.Linq.JValue;
using Expression = AdaptiveExpressions.Expression;

[CustomExpression(KindConst)]
public class FileExpression : ExpressionProperty<string>
{
    public const string KindConst = $"{Constants.Namespace}.{nameof(FileExpression)}";

    [JsonProperty("$kind")]
    [JProp("$kind")]
    public virtual string Kind => KindConst;

    //
    // Summary:
    //     Initializes a new instance of the AdaptiveExpressions.Properties.FileExpression`1
    //     class.
    public FileExpression()
        : base(default) { }

    public FileExpression(byte[] value)
        : this(value as object) { }

    public FileExpression(string expressionOrString)
        : base(expressionOrString) { }

    public FileExpression(Expression expression)
        : base(expression) { }

    public FileExpression(Func<object, object> expression)
        : base(Expression.Lambda(expression)) { }

    public FileExpression(JToken expressionOrValue)
        : base(expressionOrValue) { }

    public FileExpression(Color value)
        : this(value as object) { }

    public FileExpression(object value)
        : base(value) { }

    //
    // Summary:
    //     Converts a value to an FileExpression instance.
    //
    // Parameters:
    //   value:
    //     The value to convert.
    public static implicit operator FileExpression(byte[] value)
    {
        return new FileExpression(value);
    }

    //
    // Summary:
    //     Converts a string value to an FileExpression instance.
    //
    // Parameters:
    //   expressionOrString:
    //     A string value to convert.
    public static implicit operator FileExpression(string expressionOrString)
    {
        return new FileExpression(expressionOrString);
    }

    //
    // Summary:
    //     Converts an Expression instance to an FileExpression instance.
    //
    // Parameters:
    //   expression:
    //     The Expression instance to convert.
    public static implicit operator FileExpression(Expression expression)
    {
        return new FileExpression(expression);
    }

    //
    // Summary:
    //     Converts a JSON Token to an FileExpression instance.
    //
    // Parameters:
    //   expressionOrvalue:
    //     The JSON Token to convert.
    public static implicit operator FileExpression(JToken expressionOrvalue)
    {
        return new FileExpression(expressionOrvalue);
    }

    public static implicit operator FileExpression(StringExpression expression)
    {
        return new FileExpression(expression.ExpressionText);
    }

    public virtual IInputFile? AsInputFile(DialogContext dc)
    {
        var value = GetValue(dc);
        if (value is string s && Uri.TryCreate(s, Absolute, out var uri))
        {
            return new InputFileUrl(uri.ToString());
        }
        else if (value is string s2)
        {
            try
            {
                var bytes2 = s2.FromBase64String();
                return new InputFile(new MemoryStream(bytes2));
            }
            catch
            {
                // swallow the exception
            }
        }
        dc.EmitEventAsync("error", $"The value \"{value}\" is not a valid file.");
        return default;
    }

    public override void SetValue(object value)
    {
        base.SetValue(null);
        if (value is Expression)
        {
            base.SetValue(value);
            return;
        }
        var text = (value as string) ?? ((value as JValue)?.Value as string);
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
