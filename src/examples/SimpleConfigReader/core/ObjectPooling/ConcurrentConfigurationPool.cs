using SimpleConfigReader.Core.Models;

namespace SimpleConfigReader.Core.ObjectPooling;

/// <summary>
/// A class that implements an object pool pattern for storing the Configuration instances in a multithreaded application.
/// </summary>
public class ConcurrentConfigurationPool : ConfigurationPool, IConfigurationPool
{
    private object m_lockObject;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public ConcurrentConfigurationPool() : base()
    {
        m_lockObject = new object();
    }

    /// <summary>
    /// Add an instance of the Configuration object to the pool using lock object.
    /// </summary>
    /// <param name="configuration">Instance of the Configuration object</param>
    public override void AddConfiguration(Configuration configuration)
    {
        lock (m_lockObject)
        {
            m_configurations.Add(configuration);
        }
        ClearCachedFields();
    }
}