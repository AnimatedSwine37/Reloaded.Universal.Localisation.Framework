namespace Reloaded.Universal.Localisation.Provider.Steam.Steam;

public unsafe interface ISteamApi
{
    public string SteamApiDll { get; }
    
    public int GetHSteamUser();
    
    public void* FindOrCreateInterface(int hSteamUser, string interfaceName);
    
    public byte* GetCurrentGameLanguage(SteamApi.ISteamApps* steamApps);

    public bool ApiInit();
}