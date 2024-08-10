using System.ComponentModel;
using Reloaded.Universal.Localisation.Provider.Steam.Template.Configuration;

namespace Reloaded.Universal.Localisation.Provider.Steam.Configuration;

public class Config : Configurable<Config>
{
}

/// <summary>
/// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
/// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
/// </summary>
public class ConfiguratorMixin : ConfiguratorMixinBase
{
    // 
}