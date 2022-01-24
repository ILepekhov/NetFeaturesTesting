using Microsoft.Win32;

namespace RegistryStartupRegistrationTest;

internal static class Startup
{
    public static bool RunOnStartup(string appTitle, string appPath)
    {
        try
        {
            RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            if (registryKey is null)
                return false;

            registryKey.SetValue(appTitle, appPath);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool RemoveFromStartup(string appTitle)
    {
        try
        {
            RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            if (registryKey is null)
                return false;

            registryKey.DeleteValue(appTitle);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool IsInStartup(string appTitle)
    {
        try
        {
            RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            if (registryKey is null)
                return false;

            var value = registryKey.GetValue(appTitle);

            return value is not null;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
