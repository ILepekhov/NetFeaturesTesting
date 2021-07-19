using Polly;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PollyExample
{
    class Program
    {
        static int _counter;

        static async Task Main()
        {
            _counter = 0;

            var policy = Policy
                .Handle<TimeoutException>()
                .RetryAsync(4, (exception, retryCount, context) =>
                {
                    Console.WriteLine($"{DateTime.Now.ToLongTimeString()} try: {retryCount}, Exception: {exception.Message}");
                });

            var result = await policy.ExecuteAsync(async () => await AsyncOperation());

            Console.WriteLine($"{DateTime.Now.ToLongTimeString()} {result}");
        }

        static async Task<string> AsyncOperation()
        {
            if (_counter < 3)
            {
                var index = Interlocked.Increment(ref _counter);

                await Task.Delay(index * 5 * 1000);

                throw new TimeoutException("Blech.");
            }

            return await Task.FromResult("Yep!");
        }
    }
}
