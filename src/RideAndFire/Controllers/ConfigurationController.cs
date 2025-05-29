using System.IO;
using System.Text.Json;
using RideAndFire.Configuration;

namespace RideAndFire.Controllers;

public class ConfigurationController
{
    public ConfigurationData Configuration { get; private set; }

    private bool ConfigurationExists => File.Exists(ConfigurationFile);

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
            Configuration = new ConfigurationData(0d);
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
        var json = JsonSerializer.Serialize(Configuration);
        File.WriteAllText(ConfigurationFile, json);
    }
}