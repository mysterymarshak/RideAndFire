using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using RideAndFire.Configuration;

namespace RideAndFire.Controllers;

public class ConfigurationController
{
    public ConfigurationData Configuration { get; private set; }

    private bool ConfigurationExists => File.Exists(ConfigurationFile);

    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals
    };

    private const string ConfigurationFile = "configuration.json";

    public void Initialize()
    {
        if (ConfigurationExists)
        {
            var configurationJson = File.ReadAllText(ConfigurationFile);
            Configuration = JsonSerializer.Deserialize<ConfigurationData>(configurationJson)!;
        }
        else
        {
            Configuration = new ConfigurationData { BestScore = double.NaN };
            WriteConfiguration();
        }
    }

    public void UpdateConfiguration(ConfigurationData newConfiguration)
    {
        Configuration = newConfiguration;
        WriteConfiguration();
    }

    private void WriteConfiguration()
    {
        var json = JsonSerializer.Serialize(Configuration, SerializerOptions);
        File.WriteAllText(ConfigurationFile, json);
    }
}