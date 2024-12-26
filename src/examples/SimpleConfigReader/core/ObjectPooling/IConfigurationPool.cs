using SimpleConfigReader.Core.Models;

namespace SimpleConfigReader.Core.ObjectPooling;

/// <summary>
/// An interface for implementing an object pool pattern for storing the Configuration instances.
/// </summary>
public interface IConfigurationPool
{
    /// <summary>
    /// A cached collection of configuration settings.
    /// </summary>
    IReadOnlyList<Configuration> Configurations { get; }

    /// <summary>
    /// Add an instance of the Configuration object to the pool.
    /// </summary>
    /// <param name="configuration">Instance of the Configuration object</param>
    void AddConfiguration(Configuration configuration);
}