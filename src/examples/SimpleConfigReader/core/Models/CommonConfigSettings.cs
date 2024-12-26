namespace SimpleConfigReader.Core.Models;

/// <summary>
/// Common config settings.
/// </summary>
public class CommonConfigSettings
{
    /// <summary>
    /// Directory path.
    /// </summary>
    public string? DirectoryPath { get; set; }

    /// <summary>
    /// Indicates whether to use asynchronous reading of configuration files.
    /// </summary>
    public bool UseAsyncReading { get; set; }
}
