using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ExtensionsLibrary;

namespace TaskResultsProjection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var subscription1 = Observable.Range(1, 10)
                .SelectMany((number) => IsPrimeAsync(number),
                            (number, isPrime) => new { number, isPrime })
                .Where(x => x.isPrime)
                .Select(x => x.number)
                .SubscribeConsole("no ordering");

            var subscription2 = Observable.Range(1, 10)
                .Select(async (number) => new { number, isPrime = await IsPrimeAsync(number) })
                .Concat()
                .Where(x => x.isPrime)
                .Select(x => x.number)
                .SubscribeConsole("with ordering");

            Console.ReadLine();
        }

        private static async Task<bool> IsPrimeAsync(int number)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));

            if (number == 2 || number == 3)
            {
                return true;
            }

            if (number <= 1 || number % 2 == 0 || number % 3 == 0)
            {
                return false;
            }

            for (int i = 5; i * i <= number; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
