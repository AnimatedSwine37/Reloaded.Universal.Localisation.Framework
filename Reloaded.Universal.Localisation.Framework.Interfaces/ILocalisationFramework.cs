namespace Reloaded.Universal.Localisation.Framework.Interfaces;

/// <summary>
/// An interface to the Localisation Framework mod
/// </summary>
public interface ILocalisationFramework
{
    /// <summary>
    /// Sets/changes the current language
    /// </summary>
    /// <param name="language">The new language</param>
    public void SetLanguage(Language language);
    
    /// <summary>
    /// Tries to get the language for the current game using all registered language providers
    /// </summary>
    /// <param name="language">The language of the game if it could be determined</param>
    /// <returns>True if the language could be determined, false otherwise</returns>
    public bool TryGetLanguage(out Language language);
}