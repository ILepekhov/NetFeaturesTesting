using ExtensionsLibrary;
using System;
using System.Reactive.Linq;
using System.Threading;

namespace SkipWhileTakeWhileExample
{
    class Program
    {
        static void Main(string[] args)
        {
            bool state = false;

            IDisposable observer = Observable.Interval(TimeSpan.FromMilliseconds(400))
                .SkipWhile(_ => state == false)
                .TakeWhile(_ => state)
                .Repeat()
                .SubscribeConsole();

            for (int i = 0; i < 8; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));

                state = !state;

                Console.WriteLine("State has changed to {0}", state);
            }

            observer.Dispose();

            Console.ReadLine();
        }
    }
}
