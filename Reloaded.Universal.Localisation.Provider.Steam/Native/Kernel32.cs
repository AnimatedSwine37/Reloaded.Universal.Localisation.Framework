using System.Runtime.InteropServices;

namespace Reloaded.Universal.Localisation.Provider.Steam.Native;

public static class Kernel32
{
    [DllImport("kernel32.dll")]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

}