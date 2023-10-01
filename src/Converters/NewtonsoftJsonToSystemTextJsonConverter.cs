/*
* NewtonsoftJsonToSystemTextJsonConverter.cs
*
*   Created: 2022-12-09-04:48:30
*   Modified: 2022-12-09-04:48:30
*
*   Author: David G. Moore, Jr. <david@dgmjr.io>
*
*   Copyright © 2022-2023 David G. Moore, Jr., All Rights Reserved
*      License: MIT (https://opensource.org/licenses/MIT)
*/

namespace Telegram.System.Text.Json.Serialization;

using global::System.Text.Json;
using static global::System.Text.Encoding;

public class NewtonsoftJsonToSystemTextJsonConverter<T>
    : global::System.Text.Json.Serialization.JsonConverter<T>
{
    public override T? Read(
        ref Utf8JsonReader reader,
        type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return NSJsonConvert.DeserializeObject<T>(UTF8.GetString(reader.ValueSpan.ToArray()));
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(NSJsonConvert.SerializeObject(value));
    }
}
