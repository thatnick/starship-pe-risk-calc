using System.Text.Json;
using System.Text.Json.Serialization;

namespace PeRiskCalc.Api;

public class FetusesJsonConverter : JsonConverter<Fetuses>
{
    public override Fetuses Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString()!;
        return value.ToLower() switch
        {
            "singleton" => Fetuses.Singleton,
            "twins-mono" => Fetuses.TwinMonochorionic,
            "twins-di" => Fetuses.TwinDicorionic,
            _ => throw new JsonException("Invalid value for Fetuses")
        };
    }

    public override void Write(Utf8JsonWriter writer, Fetuses value, JsonSerializerOptions options)
    {
        var stringValue = value switch
        {
            Fetuses.Singleton => "singleton",
            Fetuses.TwinMonochorionic => "twins-mono",
            Fetuses.TwinDicorionic => "twins-di",
            _ => throw new JsonException("Invalid value for Fetuses")
        };
        writer.WriteStringValue(stringValue);
    }
}