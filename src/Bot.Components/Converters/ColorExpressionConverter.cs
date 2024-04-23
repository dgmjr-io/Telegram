namespace Telegram.Bot.Components.Converters;

using System;

using Newtonsoft.Json;

using ColorTranslator = System.Drawing.ColorTranslator;
using JObject = Newtonsoft.Json.Linq.JObject;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

[CustomJsonConverter]
public class ColorExpressionConverter
    : Dgmjr.BotFramework.Converters.ExpressionConverter<ColorExpression, ColorBox>
{
    private const string DefaultHexColorString = "#000000";

    public override void WriteJson(
        JWriter writer,
        ColorExpression? value,
        JsonSerializer serializer
    )
    {
        // Serialize ColorExpression to JSON
        var color = value.GetValue(null); // Assuming null for the state parameter works for this example
        JObject obj = new JObject
        {
            ["red"] = color.Value.Red,
            ["green"] = color.Value.Green,
            ["blue"] = color.Value.Blue
        };
        obj.WriteTo(writer);
    }

    public override ColorExpression ReadJson(
        JReader reader,
        type objectType,
        ColorExpression? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer
    )
    {
        int red,
            green,
            blue;
        if (reader.TokenType is JsonToken.String)
        {
            // Deserialize JSON string to ColorExpression
            var strColor = reader.Value?.ToString() ?? DefaultHexColorString;
            if (strColor.StartsWith("#"))
            {
                var hex = strColor[1..];
                var sysColor = ColorTranslator.FromHtml(hex);
                return new ColorExpression(new Color(sysColor.R, sysColor.G, sysColor.B));
            }
            else if (strColor.StartsWith("rgb("))
            {
                strColor = strColor[4..^1];
                var rgb = strColor.Split(',');
                red = int.Parse(rgb[0]);
                green = int.Parse(rgb[1]);
                blue = int.Parse(rgb[2]);
                return new ColorExpression(new Color(red, green, blue));
            }
        }
        // Deserialize JSON back into a ColorExpression
        JObject obj = JObject.Load(reader);
        red = obj.Value<int>("red");
        green = obj.Value<int>("green");
        blue = obj.Value<int>("blue");
        // Assuming a constructor or method to create a Color object from RGB values exists
        return new ColorExpression(new Color(red, green, blue));
    }
}
