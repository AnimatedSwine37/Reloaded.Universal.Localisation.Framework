using System.Runtime.InteropServices;

namespace Reloaded.Universal.Localisation.Provider.Steam.Steam;

public unsafe class SteamApi32 : ISteamApi
{
    private const string STEAM_API_DLL = "steam_api";
    public string SteamApiDll => STEAM_API_DLL;
    
    public int GetHSteamUser() => GetHSteamUserImport();
    public void* FindOrCreateInterface(int hSteamUser, string interfaceName) => FindOrCreateInterfaceImport(hSteamUser, interfaceName);
    public byte* GetCurrentGameLanguage(SteamApi.ISteamApps* steamApps) => GetCurrentGameLanguageImport(steamApps);
    public bool ApiInit() => ApiInitImport();

    [DllImport(STEAM_API_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = SteamApi.GetHUserName)]
    private static extern int GetHSteamUserImport();
    
    [DllImport(STEAM_API_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = SteamApi.FindOrCreateInterfaceName)]
    private static extern void* FindOrCreateInterfaceImport(int hSteamUser, string interfaceName);
    
    [DllImport(STEAM_API_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = SteamApi.GetCurrentGameLanguageName)]
    private static extern byte* GetCurrentGameLanguageImport(SteamApi.ISteamApps* steamApps);
    
    [DllImport(STEAM_API_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = SteamApi.InitName)]
    private static extern bool ApiInitImport();
}