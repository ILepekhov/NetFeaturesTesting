using ExtensionsLibrary;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace ReplaySubjectExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ReplaySubject<int> sbj = new ReplaySubject<int>(bufferSize: 5, window: TimeSpan.FromSeconds(6));

            GetHeartRateObservable().Subscribe(sbj);

            // After the user selected to show the heart rate on the screen
            Thread.Sleep(TimeSpan.FromSeconds(10));
            sbj.SubscribeConsole("HeartRate Graph");

            Console.ReadLine();
        }

        static IObservable<int> GetHeartRateObservable()
        {
            return Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(x => (int)x + 70)
                .Take(9);
        }
    }
}
