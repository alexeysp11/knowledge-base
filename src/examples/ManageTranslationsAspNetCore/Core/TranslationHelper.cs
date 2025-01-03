namespace KnowledgeBase.Examples.ManageTranslationsAspNetCore.Core;

public static class TranslationHelper
{
    /// <summary>
    /// Get a collection of labels for visual components from the database to implement language support.
    /// </summary>
    public static Dictionary<string, string> GetLanguageKvpByFormName(LanguageType languageType)
    {
        List<LanguageKeyValuePair> languageKvpList;
        switch (languageType)
        {
            case LanguageType.Russian:
                languageKvpList = new List<LanguageKeyValuePair>
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
                break;
            default:
                languageKvpList = new List<LanguageKeyValuePair>
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
                break;
        }
        
        var result = new Dictionary<string, string>();
        foreach (var kvp in languageKvpList)
        {
            result.Add(kvp.Key, kvp.Value);
        }
        return result;
    }
}
