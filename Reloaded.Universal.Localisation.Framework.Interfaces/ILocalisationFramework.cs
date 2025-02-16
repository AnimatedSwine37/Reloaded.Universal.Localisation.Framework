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
    /// Gets the current language of the game
    /// </summary>
    /// <param name="language">The language of the game if it has been set</param>
    /// <returns>True if the language has been set, false otherwise</returns>
    public bool TryGetLanguage(out Language language);

    /// <summary>
    /// Checks whether a file has a localised version for the current language.
    /// </summary>
    /// <param name="filePath">The full path to the file to check</param>
    /// <returns>True if there is a localised version of the file for the game's current language.
    /// False if the language is not set or there is no localised version.</returns>
    public bool IsFileLocalised(string filePath);
}