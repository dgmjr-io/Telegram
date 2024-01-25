using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Telegram.OpenIdConnect.Json;

public class EnumToDisplayStringJsonConverter : JsonConverter<Enum>
{
    public override bool CanConvert(type typeToConvert) => typeToConvert.IsEnum;

    public override Enum? Read(ref Utf8JsonReader reader, type typeToConvert, Jso options)
    {
        var name = reader.GetString();
        if (name is null)
            return null;

        var fields = typeToConvert.GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (var field in fields)
        {
            var displayAttribute = field.GetCustomAttribute<DescriptionAttribute>();
            if (displayAttribute is null)
                continue;

            if (displayAttribute.Description == name)
                return (Enum)field.GetValue(null)!;
        }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, Enum value, Jso options)
    {
        var fields = value.GetType().GetFields(BindingFlags.Public | BindingFlags.Static);
        var matchingField = Find(fields, field => field.GetValue(null)!.Equals(value));
        if (matchingField != null)
        {
            var displayAttribute = matchingField.GetCustomAttribute<DescriptionAttribute>();
            if (displayAttribute != null)
            {
                writer.WriteStringValue(displayAttribute.Description);
                return;
            }
        }

        // throw new InvalidOperationException($"Enum value {value} not found.");
    }
}
