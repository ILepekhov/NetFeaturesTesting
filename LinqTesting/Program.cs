using System;
using System.Linq;

namespace LinqTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            DistinctTesting();

            UnionVSConcatTesting();

            IntersectTesting();

            ExceptTesting();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void DistinctTesting()
        {
            Console.WriteLine("==== Distinct ====");

            var initialArray = new[] { 1, 1, 2, 2, 3, 3, 3, 4, 5, 1, 2 };
            var uniqueItemsArray = initialArray.Distinct();

            Console.WriteLine("Initial array: " + string.Join(", ", initialArray));
            Console.WriteLine("Unique items array: " + string.Join(", ", uniqueItemsArray));

            Console.WriteLine();
        }

        private static void UnionVSConcatTesting()
        {
            Console.WriteLine("==== Union vs Concat ====");

            var cars = new[] { "Alfa Romeo", "Aston Martin", "Audi", "Nissan", "Chevrolet",  "Chrysler", "Dodge", "BMW",
                            "Ferrari",  "Bentley", "Ford", "Lexus", "Mercedes", "Toyota", "Volvo", "Subaru", "Жигули :)"};

            var firstArray = cars.Take(5).ToArray();
            var secondArray = cars.Skip(4).ToArray();

            var concat = firstArray.Concat(secondArray).ToArray();
            var union = firstArray.Union(secondArray).ToArray();

            Console.WriteLine("cars length: " + cars.Length);

            Console.WriteLine("firstArray length: " + firstArray.Length);
            Console.WriteLine("secondArray length: " + secondArray.Length);

            Console.WriteLine("concat length: " + concat.Length);
            Console.WriteLine("union length: " + union.Length);

            Console.WriteLine();
        }

        private static void IntersectTesting()
        {
            Console.WriteLine("==== Intersect ====");

            var cars = new[] { "Alfa Romeo", "Aston Martin", "Audi", "Nissan", "Chevrolet",  "Chrysler", "Dodge", "BMW",
                            "Ferrari",  "Bentley", "Ford", "Lexus", "Mercedes", "Toyota", "Volvo", "Subaru", "Жигули :)"};

            var firstArray = cars.Take(6).ToArray();
            var secondArray = cars.Skip(4).ToArray().Take(4);

            var intersection = firstArray.Intersect(secondArray).ToArray();

            Console.WriteLine("firstArray: " + string.Join(", ", firstArray));
            Console.WriteLine("secondArray: " + string.Join(", ", secondArray));
            Console.WriteLine("Intersection: " + string.Join(", ", intersection));

            Console.WriteLine();
        }

        private static void ExceptTesting()
        {
            Console.WriteLine("==== Except ====");

            var firstArray = new[] { 1, 2, 3, 4, 5, 5, 5, 6, 7, 8, 9, 10 };
            var secondArray = firstArray[^5..]; // ого, да я же применил знания о Range!

            var exceptResult = firstArray.Except(secondArray).ToArray();

            Console.WriteLine("firstArray: " + string.Join(", ", firstArray));
            Console.WriteLine("secondArray: " + string.Join(", ", secondArray));

            Console.WriteLine("exceptResult: " + string.Join(", ", exceptResult));

            Console.WriteLine();
        }
    }
}
