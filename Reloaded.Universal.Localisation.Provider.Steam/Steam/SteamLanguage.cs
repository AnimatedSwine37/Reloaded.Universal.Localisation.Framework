using Reloaded.Universal.Localisation.Framework.Interfaces;

namespace Reloaded.Universal.Localisation.Provider.Steam.Steam;

public class SteamLanguage
{

    /// <summary>
    /// Tries to parse a language code from Steam's api to a Language
    /// </summary>
    /// <param name="code">The language code from Steam's api</param>
    /// <param name="language">An out variable containing the language if it could be parsed</param>
    /// <returns>True if the language could be parsed, false otherwise</returns>
    public static bool TryParseLanguage(string code, out Language? language)
    {
        return _languages.TryGetValue(code, out language);
    }

    private static readonly Dictionary<String, Language> _languages = new()
    {
        { "arabic", Language.Arabic },
        { "bulgarian", Language.Bulgarian },
        { "schinese", Language.SimplifiedChinese },
        { "tchinese", Language.TraditionalChinese },
        { "czech", Language.Czech },
        { "danish", Language.Danish },
        { "dutch", Language.Dutch },
        { "english", Language.English },
        { "finnish", Language.Finnish },
        { "french", Language.French },
        { "german", Language.German },
        { "greek", Language.Greek },
        { "hungarian", Language.Hungarian },
        { "indonesian", Language.Indonesian },
        { "italian", Language.Italian },
        { "japanese", Language.Japanese },
        { "koreana", Language.Korean },
        { "norwegian", Language.Norwegian },
        { "polish", Language.Polish },
        { "portuguese", Language.Portuguese },
        { "brazilian", Language.Brazilian },
        { "romanian", Language.Romanian },
        { "russian", Language.Russian },
        { "spanish", Language.Spanish },
        { "latam", Language.SpanishLatinAmerica },
        { "swedish", Language.Swedish },
        { "thai", Language.Thai },
        { "turkish", Language.Turkish },
        { "ukrainian", Language.Ukrainian },
        { "vietnamese", Language.Vietnamese },
    };
}