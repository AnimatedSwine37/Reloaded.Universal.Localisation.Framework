using Reloaded.Universal.Localisation.Framework.Interfaces;

namespace Reloaded.Universal.Localisation.Framework.FileEmulator;

public class LocalisationBuilder
{
    /// <summary>
    /// A dictionary that maps a language ids to a localised file for that language
    /// </summary>
    private Dictionary<string, string> _localisedFiles = new();

    public void AddFile(string groupFile, string languageId)
    {
        if (!Language.TryGetById(languageId, out var language))
        {
            Utils.LogError($"Unable to find language with id: {languageId}. File {groupFile} will not be localised.");
            return;
        }

        _localisedFiles.Add(languageId, groupFile);
    }

    /// <summary>
    /// Tries to build a localised version of a file
    /// </summary>
    /// <param name="language">The language the localised file should be in</param>
    /// <param name="srcFilePath">The path to the file that is being localised</param>
    /// <returns>A stream of the localised file or null if one couldn't be built</returns>
    public Stream? Build(Language language, string srcFilePath)
    {
        if (!_localisedFiles.TryGetValue(language.Id, out var localisedPath))
            return null;

        if (!File.Exists(localisedPath))
            return null;

        Utils.Log($"Using {language.Name} file {localisedPath} for {srcFilePath}");
        return new FileStream(localisedPath, FileMode.Open);
    }
}