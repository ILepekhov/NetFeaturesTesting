using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCompletionSourceCancellation
{
    class Program
    {
        static async Task Main()
        {
            Task cancelTask = Task.Run(() =>
            {
                while (Console.ReadKey().Key != ConsoleKey.Enter)
                {
                    Console.WriteLine("Press the ENTER key to cancel...");
                }

                Console.WriteLine("\nENTER key pressed: cancelling downloads.\n");
            });

            var cts = new CancellationTokenSource(10000);

            var tcs = new TaskCompletionSource<object>();

            Task.Delay(10000).ContinueWith(_ => tcs.TrySetCanceled());

            var finishedTask = await Task.WhenAny(cancelTask, tcs.Task);

            if (finishedTask == cancelTask)
            {
                Console.WriteLine("Вышли по запросу пользователя");
            }
            else if (finishedTask == tcs.Task)
            {
                Console.WriteLine("Вышли по таймеру cts");
            }
            else
            {
                Console.WriteLine("А вот это странно!");
            }
        }
    }
}
