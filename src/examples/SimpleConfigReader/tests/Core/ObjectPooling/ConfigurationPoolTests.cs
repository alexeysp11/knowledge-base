using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using SimpleConfigReader.Core.ObjectPooling;
using SimpleConfigReader.Core.Models;
using SimpleConfigReader.Tests.Stabs;

namespace SimpleConfigReader.Tests.Core.ObjectPooling;

public class ConfigurationPoolTests
{
    private List<Configuration> m_stabConfigurations;

    public ConfigurationPoolTests()
    {
        m_stabConfigurations = ConfigurationStab.GetConfigurations();
    }

    [Fact]
    public void AddConfiguration_Sequential_CollectionsEqual()
    {
        // Arrange.
        var configurationPool = new ConfigurationPool();

        // Act.
        foreach (var configuration in m_stabConfigurations)
        {
            configurationPool.AddConfiguration(configuration);
        }
        var returnedConfigurations = configurationPool.Configurations;

        // Assert.
        var collectionsEqual = (m_stabConfigurations.Count == returnedConfigurations.Count) && (m_stabConfigurations.Intersect(returnedConfigurations).Count() == m_stabConfigurations.Count);
        Assert.True(collectionsEqual);
    }

    [Fact]
    public void AddConfiguration_Multithreaded_CollectionsEqual()
    {
        // This method checks whether the collection obtained from the ConfigurationPool is equal to the original collection
        // after adding objects from different threads to the collection.
        // In most cases the test will be False due to a race condition (ie the collections are not equal).
        // However, in some cases there is a false-positive scenario, in which case it is worth repeating the tests again.

        // Arrange.
        var configurationPool = new ConfigurationPool();

        // Act.
        Task[] tasks = new Task[m_stabConfigurations.Count];
        for (int i = 0; i < m_stabConfigurations.Count; i++)
        {
            int index = i;
            tasks[index] = Task.Run(() => configurationPool.AddConfiguration(m_stabConfigurations[index]));
        }
        Task.WaitAll(tasks);
        var returnedConfigurations = configurationPool.Configurations;

        // Assert.
        var collectionsEqual = (m_stabConfigurations.Count == returnedConfigurations.Count) && (m_stabConfigurations.Intersect(returnedConfigurations).Count() == m_stabConfigurations.Count);
        Assert.False(collectionsEqual);
    }
}