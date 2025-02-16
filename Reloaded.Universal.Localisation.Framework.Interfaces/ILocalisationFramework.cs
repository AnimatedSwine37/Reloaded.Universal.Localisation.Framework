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
    
    /// <summary>
    /// Adds a new file to be used when looking for localised versions
    /// </summary>
    /// <param name="file">The path to the file to add</param>
    /// <param name="route">The route the file is in</param>
    /// <param name="modDir">The base directory to the mod that this localised file is for</param>
    /// <param name="language">The language that the new file is in</param>
    public void AddFile(string file, string route, string modDir, Language language);

    /// <summary>
    /// Adds a directory to Localisation Framework so it's like the files were in FEmulator\L10N.
    /// There must be appropriate language subfolders in the spcified path, such as `{dir}\ja\...` for Japanese files. 
    /// </summary>
    /// <param name="dir">The directory to add the files from</param>
    /// <param name="modDir">The base directory to the mod that the localised files are for</param>
    public void AddDirectory(string dir, string modDir);
    
    /// <summary>
    /// Adds a directory to Localisation Framework so it's like the files were in FEmulator\L10N\{language}
    /// </summary>
    /// <param name="dir">The directory to add the files from</param>
    /// <param name="modDir">The base directory to the mod that the localised files are for</param>
    /// <param name="language">The language that the files in this folder are in</param>
    public void AddDirectory(string dir, string modDir, Language language);
}