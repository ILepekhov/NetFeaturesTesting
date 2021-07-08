using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace BlockingCollectionTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            BlockingCollection<ConsoleKey> collection = new();

            RunLoop(collection);

            RunLoop(collection);

            Console.WriteLine("Пока!");

            collection.CompleteAdding();
            collection.Dispose();
        }

        private static void RunLoop(BlockingCollection<ConsoleKey> collection)
        {
            CancellationTokenSource cts = new();

            var loopTask = ForeachLoop(collection, cts.Token);

            while (true)
            {
                Console.Write("Нажмите любую клавишу (Esc - выйти): ");
                var keyInfo = Console.ReadKey();

                Console.WriteLine();

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    cts.Cancel();
                    break;
                }

                collection.Add(keyInfo.Key);
            }

            loopTask.GetAwaiter().GetResult();
            cts.Dispose();

            Console.WriteLine("Завершили RunLoop()!");
        }

        private static Task ForeachLoop(BlockingCollection<ConsoleKey> collection, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (collection.TryTake(out var key))
                    {
                        Console.WriteLine("Вы ввели символ: {0}", key);
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Опа, вышли из foreach");
            });
        }
    }
}
