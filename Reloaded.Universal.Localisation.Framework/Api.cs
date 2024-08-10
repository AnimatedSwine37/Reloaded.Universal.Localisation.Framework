using Reloaded.Universal.Localisation.Framework.Interfaces;

namespace Reloaded.Universal.Localisation.Framework;

public class Api: ILocalisationFramework
{
    
    private Language? _language;

    public void SetLanguage(Language language)
    {
        Utils.Log($"Set language to {language.Name}");
        _language = language;
    }

    public bool TryGetLanguage(out Language language)
    {
        language = _language;
        return language != null;
    }
}