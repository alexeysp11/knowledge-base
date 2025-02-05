using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using SimpleConfigReader.Core.ObjectPooling;
using SimpleConfigReader.Core.Models;
using SimpleConfigReader.Tests.Stabs;

namespace SimpleConfigReader.Tests.Core.ObjectPooling;

public class ConcurrentConfigurationPoolTests
{
    private List<Configuration> m_stabConfigurations;

    public ConcurrentConfigurationPoolTests()
    {
        m_stabConfigurations = ConfigurationStab.GetConfigurations();
    }

    [Fact]
    public void AddConfiguration_Sequential_CollectionsEqual()
    {
        // Arrange.
        var configurationPool = new ConcurrentConfigurationPool();

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
        // Arrange.
        var configurationPool = new ConcurrentConfigurationPool();

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
        Assert.True(collectionsEqual);
    }
}