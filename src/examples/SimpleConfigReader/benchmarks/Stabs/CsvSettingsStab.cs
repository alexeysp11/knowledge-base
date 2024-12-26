using System.Collections.Generic;
using SimpleConfigReader.Core.Models;

namespace SimpleConfigReader.Benchmarks.Stabs;

public static class CsvSettingsStab
{
    private static CsvSettings s_settings;

    static CsvSettingsStab()
    {
        s_settings = new CsvSettings
        {
            ClassFieldIndexes = new Dictionary<string, int>(),
            Separators = new List<string>() { ",", ";" }
        };
        s_settings.ClassFieldIndexes["Name"] = 0;
        s_settings.ClassFieldIndexes["Description"] = 1;
    }

    public static CsvSettings GetCsvSettings()
    {
        return s_settings;
    }
}