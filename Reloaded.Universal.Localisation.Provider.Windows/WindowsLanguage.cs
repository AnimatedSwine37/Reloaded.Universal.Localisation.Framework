using System.Globalization;
using Reloaded.Universal.Localisation.Framework.Interfaces;

namespace Reloaded.Universal.Localisation.Provider.Windows;

public class WindowsLanguage
{
    /// <summary>
    /// Tries to parse a Culture Info to a Language
    /// </summary>
    /// <param name="culture">The culture info for the language</param>
    /// <param name="language">An out variable containing the language if it could be parsed</param>
    /// <returns>True if the culture could be parsed, false otherwise</returns>
    public static bool TryParseLanguage(CultureInfo culture, out Language? language)
    {
        return _languages.TryGetValue(culture.Name, out language);
    }


    /// <summary>
    /// Maps windows culture info name to a Localisation Framework language
    /// </summary>
    private static readonly Dictionary<string, Language> _languages = new()
    {
        { "ar", Language.Arabic },
        { "bg", Language.Bulgarian },
        { "zh-Hant", Language.TraditionalChinese },
        { "zh-Hans", Language.SimplifiedChinese },
        { "cs", Language.Czech },
        { "da", Language.Danish },
        { "nl", Language.Dutch },
        { "en", Language.English },
        { "fr", Language.French },
        { "fi", Language.Finnish },
        { "de", Language.German },
        { "el", Language.Greek },
        { "hu", Language.Hungarian },
        { "id", Language.Indonesian },
        { "it", Language.Italian },
        { "ja", Language.Japanese },
        { "ko", Language.Korean },
        // Until someone asks for these to be differentiated between they can just be the same, Steam only has Norwegian
        { "nb", Language.Norwegian }, // Norwegian Bokmal
        { "nn", Language.Norwegian }, // Norwegian Nynorsk 
        { "pl", Language.Polish },
        { "pt", Language.Portuguese }, // Note that windows doesn't seem to have a pt-BR, not sure how to handle that
        { "ro", Language.Romanian },
        { "ru", Language.Russian },
        { "es", Language.Spanish }, // Note that windows doesn't seem to have Spanish Latin American
        { "sv", Language.Swedish },
        { "th", Language.Thai },
        { "tr", Language.Turkish },
        { "uk", Language.Ukrainian },
        { "vi", Language.Vietnamese },
    };
}