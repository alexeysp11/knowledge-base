namespace KnowledgeBase.Examples.ManageTranslationsAspNetCore.Core;

/// <summary>
/// Language key-value pair.
/// </summary>
public class LanguageKeyValuePair
{
    /// <summary>
    /// Language type.
    /// </summary>
    public LanguageType LanguageType { get; set; }
    
    /// <summary>
    /// Key of the language key-value pair.
    /// </summary>
    public string Key { get; set; }
    
    /// <summary>
    /// Value of the language key-value pair.
    /// </summary>
    public string Value { get; set; }
}