using ExtensionsLibrary;
using System.Reactive.Linq;

IObservable<string> weatherStationA = Observable.Throw<string>(new OutOfMemoryException());

weatherStationA
    .Log()
    .Retry(3) // <-- three attempts to resubscribe to the source observable in case of an error
    .SubscribeConsole("Retry");