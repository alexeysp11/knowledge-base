using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using SimpleConfigReader.Benchmarks.Stabs;
using SimpleConfigReader.Core.Models;
using SimpleConfigReader.Core.ObjectPooling;

namespace SimpleConfigReader.Benchmarks;

/// <summary>
/// A class that tests the performance of sequential and asynchronous addition of objects to a configuration pool.
/// </summary>
[MemoryDiagnoser]
public class SeqVsAsyncPoolingBenchmark
{
    private List<Configuration> m_stabConfigurations;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public SeqVsAsyncPoolingBenchmark()
    {
        m_stabConfigurations = ConfigurationStab.GetConfigurations();
    }

    [Benchmark]
    public void UseSeqPooling()
    {
        var configurationPool = new ConfigurationPool();
        foreach (var configuration in m_stabConfigurations)
        {
            configurationPool.AddConfiguration(configuration);
        }
    }

    [Benchmark]
    public void UseSeqProtectedPooling()
    {
        var configurationPool = new ConcurrentConfigurationPool();
        foreach (var configuration in m_stabConfigurations)
        {
            configurationPool.AddConfiguration(configuration);
        }
    }

    [Benchmark]
    public void UseAsyncPooling()
    {
        var configurationPool = new ConcurrentConfigurationPool();
        Task[] tasks = new Task[m_stabConfigurations.Count];
        for (int i = 0; i < m_stabConfigurations.Count; i++)
        {
            int index = i;
            tasks[index] = Task.Run(() => configurationPool.AddConfiguration(m_stabConfigurations[index]));
        }
        Task.WaitAll(tasks);
    }

    [Benchmark]
    public void UseAsyncUnprotectedPooling()
    {
        var configurationPool = new ConfigurationPool();
        Task[] tasks = new Task[m_stabConfigurations.Count];
        for (int i = 0; i < m_stabConfigurations.Count; i++)
        {
            int index = i;
            tasks[index] = Task.Run(() => configurationPool.AddConfiguration(m_stabConfigurations[index]));
        }
        Task.WaitAll(tasks);
    }
}