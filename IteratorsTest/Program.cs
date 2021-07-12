using System;
using System.Collections;
using System.Collections.Generic;

namespace IteratorsTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var x = new
            {
                Items = new List<int> { 1, 2, 3 }
            };

            ForEachIEnumerable(x.Items);

            var y = new
            {
                Items = new List<int> { 1, 2, 3 }.GetEnumerator()
            };

            while (y.Items.MoveNext())
            {
                Console.WriteLine(y.Items.Current);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void ForEachIEnumerable(IEnumerable sequence)
        {
            IEnumerator enumerator = sequence.GetEnumerator();

            object current = null;

            try
            {
                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    Console.WriteLine(current);
                }
            }
            finally
            {
                if (enumerator is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
