using System.Reactive.Linq;
using System.Threading;
using System;
using ExtensionsLibrary;

namespace WeakObserverExample
{
    class Program
    {
        static void Main()
        {
            IDisposable subscription = Observable.Interval(TimeSpan.FromSeconds(1))
                .AsWeakObservable()
                .SubscribeConsole("Interval");

            Console.WriteLine("Collecting");
            GC.Collect();
            Thread.Sleep(TimeSpan.FromSeconds(2));

            GC.KeepAlive(subscription);
            Console.WriteLine("Done sleeping");
            Console.WriteLine("Collecting");

            subscription = null;
            GC.Collect();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("Done sleeping");
        }
    }
}