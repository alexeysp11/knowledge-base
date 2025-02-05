namespace SimpleConfigReader.Core.Models;

/// <summary>
/// Settings for the CSV configuration manager.
/// </summary>
public class CsvSettings
{
    /// <summary>
    /// Class field indexes.
    /// </summary>
    public Dictionary<string, int>? ClassFieldIndexes { get; set; }

    /// <summary>
    /// Separators.
    /// </summary>
    public List<string>? Separators { get; set; }
}