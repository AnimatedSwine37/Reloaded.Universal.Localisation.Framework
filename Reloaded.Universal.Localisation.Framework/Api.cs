using Reloaded.Universal.Localisation.Framework.FileEmulator;
using Reloaded.Universal.Localisation.Framework.Interfaces;

namespace Reloaded.Universal.Localisation.Framework;

public class Api: ILocalisationFramework
{
    
    private Language? _language;
    private LocalisationEmulator _emulator;

    internal Api(LocalisationEmulator emulator)
    {
        _emulator = emulator;
    }

    public void SetLanguage(Language language)
    {
        if (language == null)
        {
            Utils.LogError("Cannot set language to null. Changing nothing.");
            return;
        }
        Utils.Log($"Set language to {language.Name}");
        _language = language;
        _emulator.SetLanguage(language);
    }

    public bool TryGetLanguage(out Language language)
    {
        language = _language;
        return language != null;
    }

    public bool IsFileLocalised(string filePath)
    {
        if (_language == null)
            return false;

        return _emulator.CanCreateLocalisedFile(filePath);
    }

    public void AddFile(string file, string route, string modDir, Language language)
    {
        _emulator.AddFile(file, route, modDir, language);
    }

    public void AddDirectory(string dir, string baseFolder)
    {
        _emulator.AddDirectory(dir, baseFolder);
    }

    public void AddDirectory(string dir, string baseFolder, Language language)
    {
        _emulator.AddDirectory(dir, baseFolder, language);
    }
}