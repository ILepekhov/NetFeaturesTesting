using ExtensionsLibrary;
using System.Reactive.Linq;

var observable = Observable
    .Timer(TimeSpan.FromSeconds(1))
    .Concat(Observable.Timer(TimeSpan.FromSeconds(1)))
    .Concat(Observable.Timer(TimeSpan.FromSeconds(4)))
    .Concat(Observable.Timer(TimeSpan.FromSeconds(4)));

observable
    .Timestamp()
    .Delay(TimeSpan.FromSeconds(2))
    .Timestamp()
    .Take(5)
    .SubscribeConsole("Delay");

Console.ReadLine();