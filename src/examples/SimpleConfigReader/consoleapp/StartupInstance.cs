using SimpleConfigReader.Core.ConfigurationManagers;
using SimpleConfigReader.Core.Models;
using SimpleConfigReader.Core.ObjectPooling;

namespace SimpleConfigReader.ConsoleApp;

/// <summary>
/// Class for standardized launch of an application instance.
/// </summary>
public class StartupInstance : IStartupInstance
{
    private readonly string m_directoryPath;
    private readonly bool m_useAsyncReading;
    private IConfigurationManager m_xmlConfigurationManager;
    private IConfigurationManager m_csvConfigurationManager;
    private IConfigurationPool m_configurationPool;

    public StartupInstance(
        CommonConfigSettings commonConfigSettings,
        XmlConfigurationManager xmlConfigurationManager,
        CsvConfigurationManager csvConfigurationManager,
        IConfigurationPool configurationPool)
    {
        if (commonConfigSettings == null)
            throw new System.ArgumentNullException(nameof(commonConfigSettings), "Common config settings could not be null");
        if (commonConfigSettings.DirectoryPath == null)
            throw new System.ArgumentNullException(nameof(commonConfigSettings.DirectoryPath), "Directory path from the common config settings could not be null or empty");

        m_directoryPath = commonConfigSettings.DirectoryPath;
        m_useAsyncReading = commonConfigSettings.UseAsyncReading;
        m_xmlConfigurationManager = xmlConfigurationManager;
        m_csvConfigurationManager = csvConfigurationManager;
        m_configurationPool = configurationPool;
    }

    /// <summary>
    /// Method for standardized launch of an application instance.
    /// </summary>
    public void Start()
    {
        // Reading XML configurations.
        ReadConfigurations(m_directoryPath, "*.xml", "XML", m_xmlConfigurationManager.ImportConfiguration);
        ReadConfigurations(m_directoryPath, "*.csv", "CSV", m_csvConfigurationManager.ImportConfiguration);

        PrintConfigurations();
    }

    private void ReadConfigurations(
        string directoryPath,
        string mask,
        string fileExtension,
        Func<string, Configuration> processingDelegate)
    {
        string[] files = Directory.GetFiles(directoryPath, mask);
        if (files.Length == 0)
            return;

        Console.WriteLine();
        System.Console.WriteLine($"Reading {fileExtension} configurations: started");

        if (m_useAsyncReading)
        {
            Task[] tasks = new Task[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                int index = i;
                tasks[index] = Task.Run(() => ImportConfigurationFromFile(files[index], processingDelegate));
            }
            Task.WaitAll(tasks);
        }
        else
        {
            foreach (var file in files)
            {
                ImportConfigurationFromFile(file, processingDelegate);
            }
        }

        System.Console.WriteLine($"Reading {fileExtension} configurations: finished");
    }

    private void ImportConfigurationFromFile(
        string filePath, 
        Func<string, Configuration> processingDelegate)
    {
        try
        {
            var configuration = processingDelegate(filePath);
            m_configurationPool.AddConfiguration(configuration);
            System.Console.WriteLine($"- Successfully parsed the file '{filePath}'");
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine($"- Exception while parsing the file '{filePath}': '{ex.Message}'");
        }
    }

    private void PrintConfigurations()
    {
        Console.WriteLine();
        Console.WriteLine("Configurations:");
        var configurations = m_configurationPool.Configurations;
        foreach (var config in configurations)
        {
            System.Console.WriteLine(config);
        }
    }
}