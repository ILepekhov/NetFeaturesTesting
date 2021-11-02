using ExtensionsLibrary;
using System.Reactive.Linq;

var fast = Observable.Interval(TimeSpan.FromSeconds(1)).Select(x => x * 10);
var slow = Observable.Interval(TimeSpan.FromSeconds(2));

slow.Zip(fast, (x, y) => x + y)
    .SubscribeConsole("Zip");

Console.ReadLine();