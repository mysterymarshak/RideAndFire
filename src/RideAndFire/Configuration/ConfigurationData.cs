using System.Text.Json.Serialization;
using RideAndFire.Helpers;

namespace RideAndFire.Configuration;

public record ConfigurationData
{
    [JsonConverter(typeof(DoubleConverter))]
    public required double BestScore { get; init; }
}