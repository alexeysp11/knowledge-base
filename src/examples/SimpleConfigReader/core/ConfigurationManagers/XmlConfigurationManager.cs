using System.Reflection;
using System.Xml;
using SimpleConfigReader.Core.Models;

namespace SimpleConfigReader.Core.ConfigurationManagers;

/// <summary>
/// Manager for getting configuration from XML files.
/// </summary>
public class XmlConfigurationManager : IConfigurationManager
{
    private readonly string m_rootObjectName;
    private Dictionary<string, List<string>> m_fieldMappings;
    private PropertyInfo[] m_configurationProperties;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public XmlConfigurationManager(XmlSettings settings)
    {
        if (settings == null)
            throw new System.ArgumentNullException(nameof(settings), "Settings could not be null");
        if (string.IsNullOrEmpty(settings.RootObjectName))
            throw new ArgumentNullException(nameof(settings.RootObjectName), "Root object name could not be null");
        if (settings.FieldMappings == null)
            throw new ArgumentNullException(nameof(settings.FieldMappings), "Field mappings could not be null");

        m_rootObjectName = settings.RootObjectName;
        m_fieldMappings = settings.FieldMappings;
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
            throw new System.ArgumentNullException(nameof(configFilePath), "Config file path could not be null or empty");

        XmlDocument doc = new XmlDocument();
        doc.Load(configFilePath);

        var configuration = new Configuration();
        foreach (var fieldName in m_fieldMappings.Keys)
        {
            var field = m_configurationProperties.FirstOrDefault(x => x.Name == fieldName);
            if (field == null)
            {
                continue;
            }

            foreach (string xmlField in m_fieldMappings[fieldName])
            {
                XmlNode fieldNode = doc.SelectSingleNode($"/{m_rootObjectName}/{xmlField}");
                if (fieldNode != null)
                {
                    field.SetValue(configuration, fieldNode.InnerText);
                }
            }
        }
        if (string.IsNullOrEmpty(configuration.Name))
            throw new System.Exception($"The instance of the Configuration object must have a name when attempting to parse the specified file: '{configFilePath}'");

        return configuration;
    }
}