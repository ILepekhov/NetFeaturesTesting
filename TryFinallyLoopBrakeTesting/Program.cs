using System;

namespace TryFinallyLoopBrakeTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var intArray = new[] { 1, 2, 3 };

            foreach (var item in intArray)
            {
                Console.WriteLine($"Next item is '{item}'");

                try
                {
                    Console.WriteLine($"Item '{item}'. Enter into a try..finally body");

                    if (item > 2)
                    {
                        Console.WriteLine($"Item '{item}'. Breaking a loop");
                        break;
                    }
                }
                finally
                {
                    Console.WriteLine($"Item '{item}'. Finally branch executed");
                }
            }

            Console.WriteLine("Loop finished");
        }
    }
}
