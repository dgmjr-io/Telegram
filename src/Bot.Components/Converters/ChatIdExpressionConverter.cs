using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Telegram.Bot.Components.Converters;

[CustomJsonConverter]
public class ChatIdExpressionConverter
    : Dgmjr.BotFramework.Converters.ExpressionConverter<ChatIdExpression, ChatId>
{
    //     public override ChatIdExpression? ReadJson(
    //         JsonReader reader,
    //         type objectType,
    //         ChatIdExpression? existingValue,
    //         bool hasExistingValue,
    //         JsonSerializer serializer
    //     )
    //     {
    //         if (reader.ValueType == typeof(string))
    //         {
    //             return new ChatIdExpression((string?)reader.Value);
    //         }
    //         else
    //         {
    //             // NOTE: This does not use the serializer because even we could deserialize here
    //             // expression evaluation has no idea about converters.
    //             return new ChatIdExpression(JToken.Load(reader));
    //         }
    //     }

    //     public override void WriteJson(
    //         JsonWriter writer,
    //         ChatIdExpression? value,
    //         JsonSerializer serializer
    //     )
    //     {
    //         if (value.ExpressionText is not null)
    //         {
    //             serializer.Serialize(writer, value.ToString());
    //         }
    //         else
    //         {
    //             serializer.Serialize(writer, value.Value);
    //         }
    //     }
}
