using ExtensionsLibrary;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SubscribeAsyncExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var disposable = GeneratePrimes(10)
                .Timestamp()
                .SubscribeConsole("primes");

            Console.ReadLine();

            disposable.Dispose();

            Sleep();
        }

        private static IObservable<int> GeneratePrimes(int amount)
        {
            /* non appreciated variant commented */

            //var cts = new CancellationTokenSource();
            //return Observable.Create<int>(o =>
            //{
            //    Task.Run(() =>
            //    {
            //        foreach (var prime in GeneratePrimeNumbersSync(amount))
            //        {
            //            cts.Token.ThrowIfCancellationRequested();

            //            o.OnNext(prime);
            //        }

            //        o.OnCompleted();
            //    }, cts.Token);

            //    return new CancellationDisposable(cts);
            //});


            return Observable.Create<int>((o, ct) =>
            {
                return Task.Run(() =>
                {
                    foreach (var prime in GeneratePrimeNumbersSync(amount))
                    {
                        ct.ThrowIfCancellationRequested();

                        o.OnNext(prime);
                    }

                    o.OnCompleted();
                });
            });
        }

        private static IEnumerable<int> GeneratePrimeNumbersSync(int amount)
        {
            List<int> primes = new List<int>();

            primes.Add(2);

            yield return 2;

            Sleep();

            int nextPrime = 3;

            while (primes.Count < amount)
            {
                int sqrt = (int)Math.Sqrt(nextPrime);

                bool isPrime = true;

                for (int i = 0; primes[i] <= sqrt; i++)
                {
                    if (nextPrime % primes[i] == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    primes.Add(nextPrime);

                    yield return nextPrime;

                    Sleep();
                }

                nextPrime += 2;
            }
        }

        private static void Sleep()
        {
            Thread.Sleep(2000);
        }
    }
}
