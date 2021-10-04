using ExtensionsLibrary;
using System;
using System.Reactive.Linq;
using System.Threading;

namespace RefCountExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // heating cool observable with authomatic disconnection
            // when all observers will be desposed
            var publishedObservable = Observable.Interval(TimeSpan.FromSeconds(1))
                .Do(x => Console.WriteLine("Generating {0}", x))
                .Publish()
                .RefCount();

            var subscription1 = publishedObservable.SubscribeConsole("First");
            var subscription2 = publishedObservable.SubscribeConsole("Second");

            Thread.Sleep(3000);
            subscription1.Dispose();
            Thread.Sleep(3000);
            subscription2.Dispose();

            Console.ReadLine();
        }
    }
}
