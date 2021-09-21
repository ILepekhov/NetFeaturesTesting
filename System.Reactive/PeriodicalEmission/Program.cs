using ExtensionsLibrary;
using System;
using System.Reactive.Linq;

namespace PeriodicalEmission
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting up at {0}", DateTime.UtcNow);

            IObservable<string> firstObservable = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Select(x => $"firstObservable: {x}");

            IObservable<string> secondObservable = Observable
                .Interval(TimeSpan.FromSeconds(2))
                .Select(x => $"secondObservable: {x}")
                .Take(5);

            IObservable<IObservable<string>> immediateObservable = Observable
                .Return(firstObservable);

            IObservable<IObservable<string>> scheduledObservable = Observable
                .Timer(TimeSpan.FromSeconds(5))
                .Select(x => secondObservable);

            // для первого observable значения будут идти до тех пор, пока не пойдут значения
            // из второго observable, т.е. через 5 секунд в данном случае (строка 26)
            immediateObservable
                .Merge(scheduledObservable)
                .Switch()
                .Timestamp()
                .SubscribeConsole("timer switch");

            Console.ReadLine();
        }
    }
}
