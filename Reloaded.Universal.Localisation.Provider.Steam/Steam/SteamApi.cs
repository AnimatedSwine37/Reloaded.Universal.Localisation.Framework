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
    private IHook<SteamApiInitDelegate> _steamApiInitHook;
    private Action _apiInitialised;
    
    /// <summary>
    /// Checks whether the steam api could be loaded
    /// This is different from it having initialised, it only indicates the dll existed
    /// </summary>
    public bool IsLoaded => _steamApi != null;
    
    /// <summary>
    /// Creates a new steam api interface
    /// </summary>
    /// <param name="hooks">A reloaded hooks instance</param>
    /// <param name="apiInitialised">An action that will be called when steam's api finishes initialisng</param>
    public SteamApi(IReloadedHooks hooks, Action apiInitialised)
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
        
        _apiInitialised = apiInitialised;
        HookInit(hooks);
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

    private void HookInit(IReloadedHooks hooks)
    {
        var apiHandle = Kernel32.GetModuleHandle(_steamApi.SteamApiDll);
        if (apiHandle == IntPtr.Zero)
        {
            LogError("Unable to find steam api, init will not be hooked and languages will not be provided!");
            return;
        }
        var initAddress = Kernel32.GetProcAddress(apiHandle, InitName);
        if (initAddress == IntPtr.Zero)
        {
            LogError("Unable to find steam api init, languages will not be provided!");
            return;
        }
        
        _steamApiInitHook = hooks.CreateHook<SteamApiInitDelegate>(SteamApiInit, initAddress).Activate();
    }

    private bool SteamApiInit()
    {
        var initialised = _steamApiInitHook.OriginalFunction();
        if(initialised) _apiInitialised();
        return initialised;
    }

    [Hooks.Definitions.X64.Function(Hooks.Definitions.X64.CallingConventions.Microsoft)]
    [Hooks.Definitions.X86.Function(Hooks.Definitions.X86.CallingConventions.Cdecl)]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate bool SteamApiInitDelegate();

    public struct ISteamApps
    {
    }
}