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

    public override string ToString() => $"{Name} ({Id})";
    
    // ===========================
    // A list of default languages
    // ===========================

    public static readonly Language Arabic = new Language("Arabic", "ar");
    
    public static readonly Language Bulgarian = new Language("Bulgarian", "bg");
    
    public static readonly Language TraditionalChinese = new Language("Traditional Chinese", "zh-Hant");
    
    public static readonly Language SimplifiedChinese = new Language("Simplified Chinese", "zh-Hans");
    
    public static readonly Language Czech = new Language("Czech", "cs");
    
    public static readonly Language Danish = new Language("Danish", "da");
    
    public static readonly Language Dutch = new Language("Dutch", "nl");
    
    public static readonly Language English = new Language("English", "en");
    
    public static readonly Language Finnish = new Language("Finnish", "fi");
    
    public static readonly Language French = new Language("French", "fr");
    
    public static readonly Language German = new Language("German", "de");
    
    public static readonly Language Greek = new Language("Greek", "el");
    
    public static readonly Language Hungarian = new Language("Hungarian", "hu");
    
    public static readonly Language Indonesian = new Language("Indonesian", "id");
    
    public static readonly Language Italian = new Language("Italian", "it");
    
    public static readonly Language Japanese = new Language("Japanese", "ja");
    
    public static readonly Language Korean = new Language("Korean", "ko");
    
    public static readonly Language Norwegian = new Language("Norwegian", "no");
    
    public static readonly Language Polish = new Language("Polish", "pl");
    
    public static readonly Language Portuguese = new Language("Portuguese", "pt");
    
    public static readonly Language Brazilian = new Language("Portuguese-Brazil", "pt-BR");
    
    public static readonly Language Romanian = new Language("Romanian", "ro");
    
    public static readonly Language Russian = new Language("Russian", "ru");

    public static readonly Language Spanish = new Language("Spanish", "es");
    
    public static readonly Language SpanishLatinAmerica = new Language("Spanish-Latin America", "es-419");
    
    public static readonly Language Swedish = new Language("Swedish", "sv");
    
    public static readonly Language Thai = new Language("Thai", "th");
    
    public static readonly Language Turkish = new Language("Turkish", "tr");
    
    public static readonly Language Ukrainian = new Language("Ukrainian", "uk");
    
    public static readonly Language Vietnamese = new Language("Vietnamese", "vn");
    
    
    /// <summary>
    /// A list of all default languages
    /// </summary>
    public static readonly List<Language> Languages = new()
    {
        Arabic,
        Bulgarian,
        TraditionalChinese,
        SimplifiedChinese,
        Czech,
        Danish,
        Dutch,
        English,
        French,
        Finnish,
        German,
        Greek,
        Hungarian,
        Indonesian,
        Italian,
        Japanese,
        Korean,
        Norwegian,
        Polish,
        Portuguese,
        Brazilian,
        Romanian,
        Russian,
        Spanish,
        SpanishLatinAmerica,
        Swedish,
        Thai,
        Turkish,
        Ukrainian,
        Vietnamese
    };

}