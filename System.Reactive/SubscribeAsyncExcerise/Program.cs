using ExtensionsLibrary;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;

namespace SubscribeAsyncExcerise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var disposable = Search2("abc")
                .Timestamp()
                .SubscribeConsole("search");

            while (true)
            {
                Console.WriteLine("Press 'C' to dispose observer or 'Enter' to close app");

                var key = Console.ReadKey().Key;

                if (key == ConsoleKey.C)
                {
                    disposable.Dispose();
                    break;
                }
                else if (key == ConsoleKey.Enter)
                {
                    break;
                }
            }

            Thread.Sleep(2000);
        }

        private static IObservable<string> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException($"'{nameof(query)}' cannot be null or whitespace.", nameof(query));
            }

            return Observable.Create<string>(async (o, ct) =>
            {
                var searchEngineA = new SearchEngine("Engine A");
                var searchEngineB = new SearchEngine("Engine B");

                var resultsA = await searchEngineA.SearchAsync(query, ct);
                foreach (var result in resultsA)
                {
                    ct.ThrowIfCancellationRequested();

                    o.OnNext(result);
                }

                var resultsB = await searchEngineB.SearchAsync(query, ct);
                foreach (var result in resultsB)
                {
                    ct.ThrowIfCancellationRequested();

                    o.OnNext(result);
                }

                ct.ThrowIfCancellationRequested();

                o.OnCompleted();
            });
        }

        private static IObservable<string> Search2(string query)
        {
            var searchEngineA = new SearchEngine("Engine A");
            var searchEngineB = new SearchEngine("Engine B");

            IObservable<IEnumerable<string>> resultsA = searchEngineA.SearchAsync(query, default).ToObservable();
            IObservable<IEnumerable<string>> resultsB = searchEngineB.SearchAsync(query, default).ToObservable();

            return resultsA
                .Concat(resultsB)
                .SelectMany(results => results);
        }
    }
}
