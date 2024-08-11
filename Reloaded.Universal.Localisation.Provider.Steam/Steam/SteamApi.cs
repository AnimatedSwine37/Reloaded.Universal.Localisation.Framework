using System.Runtime.InteropServices;
using Reloaded.Hooks.Definitions;
using Reloaded.Universal.Localisation.Provider.Steam.Native;
using static Reloaded.Universal.Localisation.Provider.Steam.Utils;

namespace Reloaded.Universal.Localisation.Provider.Steam.Steam;

public unsafe class SteamApi
{
    public const string InitName = "SteamAPI_Init";
    public const string GetHUserName = "SteamAPI_GetHSteamUser";
    public const string FindOrCreateInterfaceName = "SteamInternal_FindOrCreateUserInterface";
    public const string GetCurrentGameLanguageName = "SteamAPI_ISteamApps_GetCurrentGameLanguage";

    private ISteamApi? _steamApi;
    
    /// <summary>
    /// Checks whether the steam api could be loaded
    /// This is different from it having initialised, it only indicates the dll existed
    /// </summary>
    public bool IsLoaded => _steamApi != null;
    
    /// <summary>
    /// Creates a new steam api interface
    /// </summary>
    public SteamApi()
    {
        if (Environment.Is64BitProcess)
        {
            _steamApi = new SteamApi64();
        }
        else
        {
            _steamApi = new SteamApi32();
        }

        try
        {
            _steamApi.GetHSteamUser();
        }
        catch (DllNotFoundException)
        {
            Log("Failed to load steam api, not providing languages.");
            _steamApi = null;
            return;
        }
        
        if (!_steamApi.ApiInit())
        {
            LogError("Failed to initialise steam api, not providing languages.");
            _steamApi = null;
        }
    }
    
    /// <summary>
    /// Tries to get the language of the current game
    /// </summary>
    /// <returns>The language of the current game as a string or null if none could be determined</returns>
    public string? GetGameLanguage()
    {
        if (_steamApi == null)
        {
            return null;
        }
        
        int hSteamUser = _steamApi.GetHSteamUser();
        ISteamApps* steamApps = (ISteamApps*)_steamApi.FindOrCreateInterface(hSteamUser, "STEAMAPPS_INTERFACE_VERSION008");
        if (steamApps == (ISteamApps*)0)
        {
            LogError("Unable to get game language as ISteamApps was not found!");
            return null;
        }
        
        byte* language = _steamApi.GetCurrentGameLanguage(steamApps);
        return Marshal.PtrToStringAnsi((nint)language);
    }


    public struct ISteamApps
    {
    }
}