using ExtensionsLibrary;
using System;
using System.Reactive.Linq;

namespace SkipUntilExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting up at {0}", DateTime.UtcNow);

            var mainChannel = Observable
                .Interval(TimeSpan.FromSeconds(1));
            var controllChannel = Observable
                .Interval(TimeSpan.FromSeconds(1));

            mainChannel
                //.Where(x => x > 3 && x < 10) тут прекратится выпуск значений после x = 9
                .SkipUntil(controllChannel.Where(x => x > 3 && x < 10)) // а тут - нет
                .Timestamp()
                .SubscribeConsole();

            Console.ReadLine();
        }
    }
}
