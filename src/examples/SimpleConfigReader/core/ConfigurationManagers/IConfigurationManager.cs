using SimpleConfigReader.Core.Models;

namespace SimpleConfigReader.Core.ConfigurationManagers;

/// <summary>
/// Interface for configuration managers.
/// </summary>
public interface IConfigurationManager
{
    /// <summary>
    /// Read the configuration from the config file.
    /// </summary>
    /// <param name="configFilePath">Specified name of the config file.</param>
    /// <returns>Instance of the Configuration object.</returns>
    Configuration ImportConfiguration(string configFilePath);
}