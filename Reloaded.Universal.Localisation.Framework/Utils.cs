using Reloaded.Mod.Interfaces;
using Reloaded.Universal.Localisation.Framework.Configuration;

namespace Reloaded.Universal.Localisation.Framework;

internal class Utils
{
    private static ILogger _logger;
    private static Config _config;

    internal static bool Initialise(ILogger logger, Config config)
    {
        _logger = logger;
        _config = config;
        return true;
    }

    internal static void LogDebug(string message)
    {
        if (_config.DebugEnabled)
            _logger.WriteLineAsync($"[Localisation Framework] {message}");
    }

    internal static void Log(string message)
    {
        _logger.WriteLineAsync($"[Localisation Framework] {message}");
    }

    internal static void LogError(string message, Exception e)
    {
        _logger.WriteLineAsync($"[Localisation Framework] {message}: {e.Message}", System.Drawing.Color.Red);
    }

    internal static void LogError(string message)
    {
        _logger.WriteLineAsync($"[Localisation Framework] {message}", System.Drawing.Color.Red);
    }
    
}