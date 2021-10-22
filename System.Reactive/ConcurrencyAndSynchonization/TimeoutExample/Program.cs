using ExtensionsLibrary;
using System.Reactive.Linq;

var observable = Observable
    .Timer(TimeSpan.FromSeconds(1))
    .Concat(Observable.Timer(TimeSpan.FromSeconds(4)))
    .Concat(Observable.Timer(TimeSpan.FromSeconds(4)));

observable
    .Timeout(TimeSpan.FromSeconds(3))
    .SubscribeConsole("Timeout");

Console.ReadLine();