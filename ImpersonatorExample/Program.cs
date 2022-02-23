using Microsoft.Win32.SafeHandles;
using SimpleImpersonation;
using System.Security.Principal;

namespace ImpersonatorExample;

public class Program
{
    public static void Main()
    {
        UserCredentials credentials = new("10.10.10.1", "ILepekhov", "22Nuttertools10");

        using SafeAccessTokenHandle handle = credentials.LogonUser(LogonType.NewCredentials);

        WindowsIdentity.RunImpersonated(handle, WriteUserNameToFile);

        WriteUserNameToFile();

        Console.WriteLine("Press Enter to exit");
        Console.ReadLine();
    }

    private static void WriteUserNameToFile()
    {
        string currentUserName = WindowsIdentity.GetCurrent().Name.Replace('\\', '-');
        string path = @$"\\10.10.10.1\TempShare\Created by {currentUserName}.txt";

        Console.WriteLine("Writing file for " + currentUserName);

        try
        {
            File.WriteAllText(path, currentUserName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to write to the file {path}. {ex.Message}");
        }
    }
}