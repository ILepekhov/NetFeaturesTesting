using ExtensionsLibrary;
using System.Reactive.Linq;

IObservable<string> weatherSimulationResults = Observable
    .Throw<string>(new OutOfMemoryException());

weatherSimulationResults
    .Catch((OutOfMemoryException ex) =>
    {
        Console.WriteLine("Handling OOM exception");

        return Observable.Empty<string>();
    })
    .SubscribeConsole("Catch (source throws)");

Console.ReadLine();