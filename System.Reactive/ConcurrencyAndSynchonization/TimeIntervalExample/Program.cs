using ExtensionsLibrary;
using System.Reactive.Linq;

var deviceHeartbeat = Observable
    .Timer(TimeSpan.FromSeconds(1))
    .Concat(Observable.Timer(TimeSpan.FromSeconds(2)))
    .Concat(Observable.Timer(TimeSpan.FromSeconds(4)));

deviceHeartbeat
    .TimeInterval() // <-- measures the time intervals between two emissions
    .SubscribeConsole("Time from last heartbeat");

Console.ReadLine();