using Reloaded.Mod.Interfaces;

namespace Reloaded.Universal.Localisation.Provider.Steam;

internal class Utils
{
    private static ILogger _logger;

    internal static bool Initialise(ILogger logger)
    {
        _logger = logger;
        return true;
    }

    internal static void Log(string message)
    {
        _logger.WriteLine($"[Localisation Framework - Steam Support] {message}");
    }

    internal static void LogError(string message, Exception e)
    {
        _logger.WriteLine($"[Localisation Framework - Steam Support] {message}: {e.Message}", System.Drawing.Color.Red);
    }

    internal static void LogError(string message)
    {
        _logger.WriteLine($"[Localisation Framework - Steam Support] {message}", System.Drawing.Color.Red);
    }
}