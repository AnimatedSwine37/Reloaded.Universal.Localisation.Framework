using System.Globalization;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using Reloaded.Universal.Localisation.Framework.Interfaces;
using Reloaded.Universal.Localisation.Provider.Windows.Template;
using Reloaded.Universal.Localisation.Provider.Windows.Configuration;
using static Reloaded.Universal.Localisation.Provider.Windows.Utils;

namespace Reloaded.Universal.Localisation.Provider.Windows;

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

    private ILocalisationFramework _localisationFramework;

    public Mod(ModContext context)
    {
        _modLoader = context.ModLoader;
        _hooks = context.Hooks;
        _logger = context.Logger;
        _owner = context.Owner;
        _configuration = context.Configuration;
        _modConfig = context.ModConfig;

        Initialise(_logger);

        var localisationFrameworkController = _modLoader.GetController<ILocalisationFramework>();
        if (localisationFrameworkController == null ||
            !localisationFrameworkController.TryGetTarget(out _localisationFramework))
        {
            LogError(
                $"Unable to get controller for Localisation Framework, windows language will not be available.");
            return;
        }

        if (_localisationFramework.TryGetLanguage(out var currentLanguage))
        {
            Log($"Language is already set to {currentLanguage.Name}, not providing windows language.");
            return;
        }

        var culture = CultureInfo.CurrentUICulture;
        if (!WindowsLanguage.TryParseLanguage(culture, out var language))
        {
            LogError($"Unable to parse culture {culture.Name} to a localisation language. Windows language will not be available.");
            return;
        }
        
        _localisationFramework.SetLanguage(language!);
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