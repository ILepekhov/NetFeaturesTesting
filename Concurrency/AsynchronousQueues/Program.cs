using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AsynchronousQueues
{
    class Program
    {
        static async Task Main()
        {
            CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            Channel<int> queue = Channel.CreateUnbounded<int>();

            var producerTask = Task.Run(async () =>
            {
                int counter = 0;

                ChannelWriter<int> writer = queue.Writer;

                while (!cts.Token.IsCancellationRequested)
                {
                    await writer.WriteAsync(++counter);

                    Console.WriteLine("Pushed {0}", counter);

                    await Task.Delay(200);
                }

                writer.Complete();
            });

            var consumerTask = Task.Run(async () =>
            {
                ChannelReader<int> reader = queue.Reader;
                await foreach (int value in reader.ReadAllAsync())
                {
                    Console.WriteLine("Poped {0}", value);
                };
            });

            await Task.WhenAll(producerTask, consumerTask);

            Console.WriteLine("That's all");
        }
    }
}
