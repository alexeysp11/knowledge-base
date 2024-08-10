namespace KnowledgeBase.Examples.SimpleWebService.Core.Models;

/// <summary>
/// Contains application configuration settings during startup.
/// </summary>
public class AppInitConfigs
{
    /// <summary>
    /// Connection string for DB.
    /// </summary>
    public string? DbConnectionString { get; set;}

    /// <summary>
    /// Remove cached data from database before adding.
    /// </summary>
    public bool DeleteCacheBeforeInserting { get; set;}
}