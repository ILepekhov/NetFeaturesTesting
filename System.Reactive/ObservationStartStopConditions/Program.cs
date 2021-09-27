using System;
using System.Reactive;
using System.Reactive.Linq;
using ExtensionsLibrary;

namespace ObservationStartStopConditions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DelayedSubscriptionExample(TimeSpan.FromSeconds(2))
                .SubscribeConsole();

            Console.ReadLine();

            LimitedBySignalSubscriptionExample().SubscribeConsole();

            Console.ReadLine();
        }

        private static IObservable<Timestamped<int>> DelayedSubscriptionExample(TimeSpan delay)
        {
            Console.WriteLine("Startup time is {0}", DateTime.Now);

            return Observable.Range(1, 5)
                .Log("Range next")
                .Timestamp()
                .Log("Timestamp")
                .DelaySubscription(delay);
        }

        private static IObservable<DateTimeOffset> LimitedBySignalSubscriptionExample()
        {
            return Observable.Timer(DateTimeOffset.Now, TimeSpan.FromSeconds(1))
                .Select(t => DateTimeOffset.Now)
                .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(5)));
        }
    }
}
