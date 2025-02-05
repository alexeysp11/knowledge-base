namespace SimpleConfigReader.Core.Models;

/// <summary>
/// Settings for the XML configuration manager.
/// </summary>
public class XmlSettings
{
    /// <summary>
    /// Root object name.
    /// </summary>
    public string? RootObjectName { get; set; }

    /// <summary>
    /// Field mappings.
    /// </summary>
    public Dictionary<string, List<string>>? FieldMappings { get; set; }
}