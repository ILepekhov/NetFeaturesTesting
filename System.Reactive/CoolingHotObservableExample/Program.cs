using ExtensionsLibrary;
using System;
using System.Reactive.Linq;
using System.Threading;

namespace CoolingHotObservableExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var publishedObservable = Observable.Interval(TimeSpan.FromSeconds(1)) // <- the hot Observable imitation
                .Take(10)
                .Timestamp()
                .Replay(window: TimeSpan.FromSeconds(7)); // <- stores emissions for the last 7 seconds and replays them to the new observers

            publishedObservable.Connect();

            var subscription1 = publishedObservable.SubscribeConsole("First");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            var subscription2 = publishedObservable.SubscribeConsole("Second");

            Console.ReadLine();
        }
    }
}
