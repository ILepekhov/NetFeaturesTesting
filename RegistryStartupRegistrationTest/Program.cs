using RegistryStartupRegistrationTest;
using System.Reflection;

string appTitle = "RegistryStartupRegistration Test";
string path = Assembly.GetCallingAssembly().Location;

path = Path.ChangeExtension(path, "exe");

bool isInStartup = Startup.IsInStartup(appTitle);

if (isInStartup)
{
    Console.WriteLine("Application is in Startup");

    bool unregistered = Startup.RemoveFromStartup(appTitle);

    if (unregistered)
        Console.WriteLine("Application unregistered from Startup");
    else
        Console.WriteLine("Failed to unregister Application");
}
else
{
    Console.WriteLine("Application is not in Startup");

    bool registered = Startup.RunOnStartup(appTitle, path);

    if (registered)
        Console.WriteLine("Application registered in Startup");
    else
        Console.WriteLine("Failed to register Application in Startup");
}


