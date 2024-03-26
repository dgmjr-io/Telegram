using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Telegram.Bot.Components.Converters;

public class ChatIdExpressionConverter : Newtonsoft.Json.JsonConverter<StrExp>
{
    public override StrExp? ReadJson(JsonReader reader, type objectType, StrExp? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }

        var value = serializer.Deserialize<string>(reader);
        return new StrExp(value);
    }

    public override void WriteJson(JsonWriter writer, StrExp? value, JsonSerializer serializer)
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
