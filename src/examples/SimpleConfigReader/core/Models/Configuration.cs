namespace SimpleConfigReader.Core.Models;

/// <summary>
/// Model of the configuaration item.
/// </summary>
public class Configuration
{
    /// <summary>
    /// Name of the configuaration item.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Description of the configuaration item.
    /// </summary>
    public string? Description { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, Description: {Description}";
    }
}