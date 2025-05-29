using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RideAndFire.Helpers;

public class DoubleConverter : JsonConverter<double>
{
    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
            return reader.GetDouble();

        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();
            return value switch
            {
                "Infinity" or "+Infinity" => double.PositiveInfinity,
                "-Infinity" => double.NegativeInfinity,
                "NaN" => double.NaN,
                _ => throw new JsonException($"Unexpected string value: {value}")
            };
        }

        throw new JsonException($"Unexpected token type: {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        if (double.IsPositiveInfinity(value))
        {
            writer.WriteStringValue("Infinity");
        }
        else if (double.IsNegativeInfinity(value))
        {
            writer.WriteStringValue("-Infinity");
        }
        else if (double.IsNaN(value))
        {
            writer.WriteStringValue("NaN");
        }
        else
        {
            writer.WriteNumberValue(value);
        }
    }
}