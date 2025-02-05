using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using SimpleConfigReader.Benchmarks.Stabs;
using SimpleConfigReader.Core.ConfigurationManagers;
using SimpleConfigReader.Core.Models;
using SimpleConfigReader.Core.ObjectPooling;

namespace SimpleConfigReader.Benchmarks;

/// <summary>
/// A class that tests the performance of sequential and asynchronous reading XML files.
/// </summary>
[MemoryDiagnoser]
public class SeqVsAsyncXmlReadingBenchmark
{
    private string[] m_files;
    private IConfigurationManager m_configurationManager;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public SeqVsAsyncXmlReadingBenchmark()
    {
        m_files = Directory.GetFiles("data", ".xml");

        var settings = XmlSettingsStab.GetXmlSettings();
        m_configurationManager = new XmlConfigurationManager(settings);
    }

    [Benchmark]
    public void UseSeqReading()
    {
        var configurationPool = new ConfigurationPool();
        foreach (var file in m_files)
        {
            var configuration = m_configurationManager.ImportConfiguration(file);
            configurationPool.AddConfiguration(configuration);
        }
    }

    [Benchmark]
    public void UseSeqProtectedReading()
    {
        var configurationPool = new ConcurrentConfigurationPool();
        foreach (var file in m_files)
        {
            var configuration = m_configurationManager.ImportConfiguration(file);
            configurationPool.AddConfiguration(configuration);
        }
    }

    [Benchmark]
    public void UseAsyncReading()
    {
        var configurationPool = new ConcurrentConfigurationPool();
        Task[] tasks = new Task[m_files.Length];
        for (int i = 0; i < m_files.Length; i++)
        {
            int index = i;
            tasks[index] = Task.Run(() => 
            {
                var configuration = m_configurationManager.ImportConfiguration(m_files[index]);
                configurationPool.AddConfiguration(configuration);
            });
        }
        Task.WaitAll(tasks);
    }

    [Benchmark]
    public void UseAsyncUnprotectedReading()
    {
        var configurationPool = new ConfigurationPool();
        Task[] tasks = new Task[m_files.Length];
        for (int i = 0; i < m_files.Length; i++)
        {
            int index = i;
            tasks[index] = Task.Run(() => 
            {
                var configuration = m_configurationManager.ImportConfiguration(m_files[index]);
                configurationPool.AddConfiguration(configuration);
            });
        }
        Task.WaitAll(tasks);
    }
}