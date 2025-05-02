using System.Runtime.InteropServices;

namespace Reloaded.Universal.Localisation.Provider.Steam.Native;

public static class Kernel32
{
    [DllImport("kernel32", SetLastError=true)]
    public static extern IntPtr LoadLibrary(string lpFileName);

    [DllImport("kernel32", SetLastError=true)]
    public static extern IntPtr FreeLibrary(string lpFileName);

}