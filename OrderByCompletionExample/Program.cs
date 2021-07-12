using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderByCompletionExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await PrintDelayedRandomTasksAsync();
        }

        private static async Task PrintDelayedRandomTasksAsync()
        {
            Random rnd = new();
            var values = Enumerable.Range(0, 10).Select(_ => rnd.Next(3000)).ToList();

            Console.WriteLine("Initial order: {0}", string.Join(" ", values));

            var tasks = values.Select(DelayAsync);

            var ordered = tasks.OrderByCompletion();

            Console.WriteLine("In order of completion:");
            await ForEach(ordered, Console.WriteLine);
        }

        /// <summary>
        /// Returns a task which delays (asynchronously) by the given number of milliseconds,
        /// then return that same number back.
        /// </summary>
        private static async Task<int> DelayAsync(int delayMillis)
        {
            await Task.Delay(delayMillis);

            return delayMillis;
        }

        /// <summary>
        /// Executes the given action on each of the tasks in turn, in the order of the sequence.
        /// The action is passed the result of each task.
        /// </summary>
        private static async Task ForEach<T>(IEnumerable<Task<T>> tasks, Action<T> action)
        {
            foreach (var task in tasks)
            {
                T value = await task;

                action(value);
            }
        }
    }
}
