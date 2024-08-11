using System.Diagnostics;
using FileEmulationFramework.Interfaces;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;
using Reloaded.Universal.Localisation.Framework.Template;
using Reloaded.Universal.Localisation.Framework.Configuration;
using Reloaded.Universal.Localisation.Framework.FileEmulator;
using Reloaded.Universal.Localisation.Framework.Interfaces;

using static Reloaded.Universal.Localisation.Framework.Utils;

namespace Reloaded.Universal.Localisation.Framework;

/// <summary>
/// Your mod logic goes here.
/// </summary>
public class Mod : ModBase, IExports // <= Do not Remove.
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

    private ILocalisationFramework _api;
    
    private LocalisationEmulator _localisationEmulator;

    public Mod(ModContext context)
    {
        _modLoader = context.ModLoader;
        _hooks = context.Hooks;
        _logger = context.Logger;
        _owner = context.Owner;
        _configuration = context.Configuration;
        _modConfig = context.ModConfig;

        Initialise(_logger, _configuration);
        
        _localisationEmulator = new LocalisationEmulator();
        _modLoader.GetController<IEmulationFramework>().TryGetTarget(out var framework);
        framework!.Register(_localisationEmulator);
        
        _api = new Api(_localisationEmulator);

        // Expose API
        _modLoader.AddOrReplaceController(context.Owner, _api);
        _modLoader.OnModLoaderInitialized += OnModLoaderInitialised;
        _modLoader.ModLoading += OnModLoading;
    }

    private void OnModLoaderInitialised()
    {
        _modLoader.ModLoading -= OnModLoading;
        _modLoader.OnModLoaderInitialized -= OnModLoaderInitialised;
    }
    
    private void OnModLoading(IModV1 mod, IModConfigV1 modConfig) => _localisationEmulator.OnModLoading(_modLoader.GetDirectoryForModId(modConfig.ModId));

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

    public Type[] GetTypes() => new[] { typeof(ILocalisationFramework) };
}