using System.Diagnostics;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using Reloaded.Universal.Localisation.Provider.Steam.Template;
using Reloaded.Universal.Localisation.Provider.Steam.Configuration;
using Reloaded.Universal.Localisation.Framework.Interfaces;
using Reloaded.Universal.Localisation.Provider.Steam.Steam;
using static Reloaded.Universal.Localisation.Provider.Steam.Utils;

namespace Reloaded.Universal.Localisation.Provider.Steam;

/// <summary>
/// Your mod logic goes here.
/// </summary>
public class Mod : ModBase // <= Do not Remove.
{
    /// <summary>
    /// Provides access to the mod loader API.
    /// </summary>
    private readonly IModLoader _modLoader;

    /// <summary>
    /// Provides access to the Reloaded.Hooks API.
    /// </summary>
    /// <remarks>This is null if you remove dependency on Reloaded.SharedLib.Hooks in your mod.</remarks>
    private readonly IReloadedHooks? _hooks;

    /// <summary>
    /// Provides access to the Reloaded logger.
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// Entry point into the mod, instance that created this class.
    /// </summary>
    private readonly IMod _owner;

    /// <summary>
    /// Provides access to this mod's configuration.
    /// </summary>
    private Config _configuration;

    /// <summary>
    /// The configuration of the currently executing mod.
    /// </summary>
    private readonly IModConfig _modConfig;

    private ILocalisationFramework? _localisationFramework;

    private SteamApi _steamApi;

    public Mod(ModContext context)
    {
        _modLoader = context.ModLoader;
        _hooks = context.Hooks;
        _logger = context.Logger;
        _owner = context.Owner;
        _configuration = context.Configuration;
        _modConfig = context.ModConfig;

        Initialise(_logger);

        _steamApi = new SteamApi(_hooks, SteamApiInitialised);
        if (!_steamApi.IsLoaded)
        {
            LogError("Steam API could not be loaded, no language will be provided.");
            return;
        }

        var localisationFrameworkController = _modLoader.GetController<ILocalisationFramework>();
        if (localisationFrameworkController == null ||
            !localisationFrameworkController.TryGetTarget(out _localisationFramework))
        {
            LogError(
                $"Unable to get controller for Localisation Framework, steam game languages will not be available.");
        }
    }

    private void SteamApiInitialised()
    {
        if(_localisationFramework == null)
            return;
        
        Log("Steam API initialised, trying to get language.");

        var languageStr = _steamApi.GetGameLanguage();
        if (languageStr == null)
        {
            LogError("Failed to get language from Steam API.");
            return;
        }

        Log($"Steam game language is {languageStr}");
        if (!Language.TryGetByName(languageStr, out var language))
        {
            LogError($"Failed parse language {languageStr} from Steam API.");
        }

        _localisationFramework.SetLanguage(language);
    }

    #region Standard Overrides

    public override void ConfigurationUpdated(Config configuration)
    {
        // Apply settings from configuration.
        // ... your code here.
        _configuration = configuration;
        _logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");
    }

    #endregion

    #region For Exports, Serialization etc.

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Mod()
    {
    }
#pragma warning restore CS8618

    #endregion
}