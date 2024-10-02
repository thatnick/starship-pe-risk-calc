using System.Text.Json;
using System.Text.Json.Serialization;

namespace PeRiskCalc.Api;

public class EthnicityJsonConverter : JsonConverter<Ethnicity>
{
    public override Ethnicity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString()!;
        return value.ToLower() switch
        {
            "south-asian" => Ethnicity.SouthAsian,
            "black" => Ethnicity.Black,
            "other" => Ethnicity.Other,
            _ => throw new JsonException("Invalid value for Ethnicity")
        };
    }

    public override void Write(Utf8JsonWriter writer, Ethnicity value, JsonSerializerOptions options)
    {
        var stringValue = value switch
        {
            Ethnicity.SouthAsian => "south-asian",
            Ethnicity.Black => "black",
            Ethnicity.Other => "other",
            _ => throw new JsonException("Invalid value for Ethnicity")
        };
        writer.WriteStringValue(stringValue);
    }
}