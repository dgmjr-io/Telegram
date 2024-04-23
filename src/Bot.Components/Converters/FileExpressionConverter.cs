namespace Telegram.Bot.Components.Converters;

using System;

using Newtonsoft.Json;

using Telegram.Bot.Components.Expressions;

[CustomJsonConverter]
public class FileExpressionConverter : Newtonsoft.Json.JsonConverter<FileExpression>
{
    public override FileExpression? ReadJson(
        JReader reader,
        type objectType,
        FileExpression? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer
    )
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }

        var value = serializer.Deserialize<string>(reader);
        return new FileExpression(value);
    }

    public override void WriteJson(JWriter writer, FileExpression? value, JsonSerializer serializer)
    {
        if (value.ExpressionText is not null)
        {
            serializer.Serialize(writer, value.ToString());
        }
        else
        {
            serializer.Serialize(writer, value.Value);
        }
    }
}
