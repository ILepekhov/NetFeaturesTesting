using ExtensionsLibrary;
using System.Reactive.Linq;

var observable = new[] { 4, 1, 3, 2 }.ToObservable();

observable
    .Timestamp()
    .Delay(x => Observable.Timer(TimeSpan.FromSeconds(x.Value)))
    .Timestamp()
    .SubscribeConsole("Delay");

Console.ReadLine();