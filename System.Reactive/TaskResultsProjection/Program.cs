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
            var subscription = Observable.Range(1, 10)
                .SelectMany((number) => IsPrimeAsync(number, TimeSpan.FromMilliseconds(number * 100)),
                            (number, isPrime) => new { number, isPrime })
                .Where(x => x.isPrime)
                .Select(x => x.number)
                .SubscribeConsole("prime async detector");

            Console.ReadLine();
        }

        private static async Task<bool> IsPrimeAsync(int number, TimeSpan delay)
        {
            await Task.Delay(delay);

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
