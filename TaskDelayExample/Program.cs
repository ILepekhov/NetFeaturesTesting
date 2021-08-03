using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskDelayExample
{
    class Program
    {
        static async Task Main()
        {
            Console.Write("Enter delay in seconds: ");
            int delay = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter cancellation timeout in seconds: ");
            int tiemout = Convert.ToInt32(Console.ReadLine());

            CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(tiemout));

            try
            {
                Console.WriteLine("Start waiting");

                await Task.Delay(TimeSpan.FromSeconds(delay), cts.Token);

                Console.WriteLine("It worked out fine");
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Delay task was cancelled");
            }
            catch (Exception)
            {
                Console.WriteLine("Unexpected exception");
            }
        }
    }
}
