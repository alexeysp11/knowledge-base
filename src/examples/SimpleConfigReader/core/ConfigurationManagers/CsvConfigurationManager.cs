using SimpleConfigReader.Core.Models;
using System.Reflection;

namespace SimpleConfigReader.Core.ConfigurationManagers;

/// <summary>
/// Manager for getting configuration from CSV files.
/// </summary>
public class CsvConfigurationManager : IConfigurationManager
{
    private Dictionary<string, int> m_classFieldIndexes;
    private List<string> m_separators;
    private PropertyInfo[] m_configurationProperties;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public CsvConfigurationManager(CsvSettings settings)
    {
        if (settings == null) 
            throw new System.ArgumentNullException(nameof(settings), "Settings could not be null");
        if (settings.ClassFieldIndexes == null)
            throw new System.ArgumentNullException(nameof(settings.ClassFieldIndexes), "Class field indexes could not be null");
        if (settings.Separators == null)
            throw new System.ArgumentNullException(nameof(settings.Separators), "Separators could not be null");

        m_classFieldIndexes = settings.ClassFieldIndexes;
        m_separators = settings.Separators;
        m_configurationProperties = typeof(Configuration).GetProperties();
    }

    /// <summary>
    /// Read the configuration from the config file.
    /// </summary>
    /// <param name="configFilePath">Specified name of the config file.</param>
    /// <returns>Instance of the Configuration object.</returns>
    public Configuration ImportConfiguration(string configFilePath)
    {
        if (string.IsNullOrEmpty(configFilePath))
            throw new System.ArgumentNullException("File path could not be null or empty");
        
        string[] lines = File.ReadAllLines(configFilePath);
        if (lines.Length == 0)
            throw new System.Exception($"Could not get configuration because the specified file is empty: {configFilePath}");

        foreach (var line in lines)
        {
            Configuration configuration = new Configuration();
            foreach (var separator in m_separators)
            {
                string[] parts = line.Split(separator);
                if (parts.Length != m_classFieldIndexes.Count)
                {
                    continue;
                }
                foreach (var key in m_classFieldIndexes.Keys)
                {
                    var field = m_configurationProperties.FirstOrDefault(x => x.Name == key);
                    if (field != null)
                    {
                        var index = m_classFieldIndexes[key];
                        field.SetValue(configuration, parts[index]);
                    }
                }
            }
            if (string.IsNullOrEmpty(configuration.Name))
                throw new System.Exception($"The instance of the Configuration object must have a name when attempting to parse the specified file: '{configFilePath}'");
            
            return configuration;
        }
        
        throw new System.Exception($"Could not get configuration because the specified file contains incorrect number of parts: {configFilePath}");
    }
}