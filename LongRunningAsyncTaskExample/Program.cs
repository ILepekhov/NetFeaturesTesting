using System;
using System.Threading;
using System.Threading.Tasks;

namespace LongRunningAsyncTaskExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Message from the main thread. CurrentManagedThreadId = {Environment.CurrentManagedThreadId}, ManagedThreadId = {Thread.CurrentThread.ManagedThreadId}");

            var cts = new CancellationTokenSource(10000);

            var task = Task.Factory.StartNew(
                () => StartBackgroundAsyncLoop(cts.Token),
                cts.Token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default).Unwrap();

            task.Wait();

            Console.WriteLine("Completed");
        }

        private static async Task StartBackgroundAsyncLoop(CancellationToken token)
        {
            while (token.IsCancellationRequested is false)
            {
                Console.WriteLine($"Message from the bg task. CurrentManagedThreadId = {Environment.CurrentManagedThreadId}, ManagedThreadId = {Thread.CurrentThread.ManagedThreadId}");

                await Task.Delay(1000).ConfigureAwait(false);
            }

            Console.WriteLine("Got a cancellation sighal! Waiting for 1 sec before exit");

            await Task.Delay(1000).ConfigureAwait(false);
        }
    }
}
