using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace KnowledgeBase.Examples.ManageTranslationsAspNetCore.Core;

public class TranslationHelper
{
    /// <summary>
    /// Get a collection of labels for visual components from the database to implement language support.
    /// </summary>
    public static Dictionary<string, string> GetLanguageKvpByFormName(
        EmployeesMvcDbContext context,
        LanguageType languageType,
        string formName,
        string applicationUid)
    {
        List<LanguageKeyValuePair> languageKvpList = context.LanguageKeyValuePairs
            .FromSqlRaw("select * from public.\"GetLanguageKvpByFormName\"(@LanguageType, @FormName, @ApplicationUid)", 
                new NpgsqlParameter("LanguageType", (int)languageType),
                new NpgsqlParameter("FormName", formName),
                new NpgsqlParameter("ApplicationUid", applicationUid))
            .ToList();
        
        var result = new Dictionary<string, string>();
        foreach (var kvp in languageKvpList)
        {
            result.Add(kvp.Key, kvp.Value);
        }
        return result;
    }

    /// <summary>
    /// Get a collection of labels for visual components from the database to implement language support.
    /// </summary>
    public static Dictionary<string, string> GetLanguageKvpByFormName(
        string connectionString,
        LanguageType languageType,
        string formName,
        string applicationUid)
    {
        List<LanguageKeyValuePair> languageKvpList;
        using (var connection = new NpgsqlConnection(connectionString))
        {
            string query = "select * from public.\"GetLanguageKvpByFormName\"(@LanguageType, @FormName, @ApplicationUid)";
            
            var parameters = new DynamicParameters();
            parameters.Add("@LanguageType", (int)languageType);
            parameters.Add("@FormName", formName);
            parameters.Add("@ApplicationUid", applicationUid);

            languageKvpList = connection.Query<LanguageKeyValuePair>(query, parameters).ToList();
        }
        
        var result = new Dictionary<string, string>();
        foreach (var kvp in languageKvpList)
        {
            result.Add(kvp.Key, kvp.Value);
        }
        return result;
    }
    
    /// <summary>
    /// Get a collection of labels for visual components from the database to implement language support.
    /// </summary>
    public static Dictionary<string, string> GetLanguageKvpByFormName(LanguageType languageType)
    {
        List<LanguageKeyValuePair> languageKvpList = new TranslationHelper().GetTranslations(languageType);
        
        var result = new Dictionary<string, string>();
        foreach (var kvp in languageKvpList)
        {
            result.Add(kvp.Key, kvp.Value);
        }
        return result;
    }

    internal List<LanguageKeyValuePair> GetTranslations(LanguageType languageType)
    {
        switch (languageType)
        {
            case LanguageType.Russian:
                return new List<LanguageKeyValuePair>
                {
                    new LanguageKeyValuePair
                    {
                        LanguageType = LanguageType.Russian,
                        Key = "AppName",
                        Value = "Наименование приложения"
                    },
                    new LanguageKeyValuePair
                    {
                        LanguageType = LanguageType.Russian,
                        Key = "ForAuthenticatedOnly",
                        Value = "Для авторизованного пользователя"
                    }
                };
            default:
                return new List<LanguageKeyValuePair>
                {
                    new LanguageKeyValuePair
                    {
                        LanguageType = LanguageType.English,
                        Key = "AppName",
                        Value = "Application name"
                    },
                    new LanguageKeyValuePair
                    {
                        LanguageType = LanguageType.English,
                        Key = "ForAuthenticatedOnly",
                        Value = "For authenticated user"
                    }
                };
        }
    }
}
