using System.Diagnostics;
using System.Security;

Console.WriteLine("This utility will start the 'cmd.exe' with the specified credentials");

Console.Write("Enter username: ");
string? username = Console.ReadLine();

Console.Write("Enter password: ");
string? password = Console.ReadLine();
SecureString passwordSecureString = new();

foreach (var letter in password)
{
    passwordSecureString.AppendChar(letter);
}

Process process = new();

process.StartInfo.FileName = "cmd.exe";
process.StartInfo.UseShellExecute = false;
process.StartInfo.UserName = username;
process.StartInfo.Password = passwordSecureString;

process.Start();

Console.WriteLine("Press 'Enter' to exit...");
Console.ReadLine();