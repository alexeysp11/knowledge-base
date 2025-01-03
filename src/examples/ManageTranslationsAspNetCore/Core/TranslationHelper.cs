﻿namespace KnowledgeBase.Examples.ManageTranslationsAspNetCore.Core;

public class TranslationHelper
{
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