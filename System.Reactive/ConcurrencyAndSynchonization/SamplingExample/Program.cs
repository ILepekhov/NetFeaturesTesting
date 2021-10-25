using ExtensionsLibrary;
using System.Reactive.Linq;

Observable
    .Interval(TimeSpan.FromSeconds(1))
    .Sample(TimeSpan.FromSeconds(3.5))
    .Take(3)
    .SubscribeConsole("Sample");

Console.ReadLine();