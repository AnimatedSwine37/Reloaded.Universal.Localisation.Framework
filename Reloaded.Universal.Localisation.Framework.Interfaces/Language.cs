namespace Reloaded.Universal.Localisation.Framework.Interfaces;

/// <summary>
/// A class that represents an individual language
/// </summary>
public class Language
{
    /// <summary>
    /// The friendly name of the language, used for display
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The id of the language, used as the folder name for files in this language
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Creates a new language instance
    /// </summary>
    /// <param name="name">The name of the language</param>
    /// <param name="id">The id of the language</param>
    public Language(string name, string id)
    {
        Name = name;
        Id = id;
    }

    /// <summary>
    /// Tries to parse a language id to one of the known Languages
    /// </summary>
    /// <param name="id">The language id to parse</param>
    /// <param name="language">The matching Language if one was found</param>
    /// <returns>True if the language could be parsed, false otherwise</returns>
    public static bool TryGetById(string id, out Language language)
    {
        language = Languages.Find(language => language.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        return language != null;
    }
    
    /// <summary>
    /// Tries to parse a language name to one of the known Languages
    /// </summary>
    /// <param name="name">The language name to parse</param>
    /// <param name="language">The matching Language if one was found</param>
    /// <returns>True if the language could be parsed, false otherwise</returns>
    public static bool TryGetByName(string name, out Language language)
    {
        language = Languages.Find(language => language.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return language != null;
    }
    
    // ===========================
    // A list of default languages
    // ===========================

    public static readonly Language English = new Language("English", "en");

    public static readonly Language German = new Language("German", "de");

    public static readonly Language Spanish = new Language("Spanish", "es");
    
    public static readonly Language French = new Language("French", "fr");
    
    public static readonly Language Italian = new Language("Italian", "it");
    
    public static readonly Language Japanese = new Language("Japanese", "ja");
    
    public static readonly Language Korean = new Language("Korean", "ko");
    
    public static readonly Language Polish = new Language("Polish", "pl");
    
    public static readonly Language Portuguese = new Language("Portuguese", "pt");
    
    public static readonly Language Russian = new Language("Russian", "ru");
    
    public static readonly Language Turkish = new Language("Turkish", "tr");
    
    public static readonly Language TraditionalChinese = new Language("Traditional Chinese", "zh-Hant");
    
    public static readonly Language SimplifiedChinese = new Language("Simplified Chinese", "zh-Hans");
    
    /// <summary>
    /// A list of all default languages
    /// </summary>
    public static readonly List<Language> Languages = new()
    {
        English,
        German,
        Spanish,
        French,
        Italian,
        Japanese,
        Korean,
        Polish,
        Portuguese,
        Russian,
        Turkish,
        TraditionalChinese,
        SimplifiedChinese
    };

}