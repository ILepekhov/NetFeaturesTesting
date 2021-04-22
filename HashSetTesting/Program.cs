using System;
using System.Collections.Generic;

namespace HashSetTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            TestHashSetWithIntegers();

            TestHashSetWithObjectsWithDefaultGetHashCodeMethod();

            TestHashSetWithObjectsWithCustomGetHashCodeMethod();

            Console.Write("Press Enter to exit...");
            Console.ReadLine();
        }

        private static void TestHashSetWithIntegers()
        {
            Console.WriteLine("TestHashSetWithIntegers");

            var hashset = new HashSet<int>();

            Console.WriteLine("Adding the numbers 0, 1, 2 and 3 to the hashset");

            for (int i = 0; i <= 3; i++)
            {
                if (hashset.Add(i))
                {
                    Console.WriteLine($"Number of {i} was added to the hashset");
                }
                else
                {
                    Console.WriteLine($"Number of {i} has already present in the hashset");
                }
            }

            Console.WriteLine("Trying to add the numbers 0, 1, 2 and 3 to the hashset second time");

            for (int i = 0; i <= 3; i++)
            {
                if (hashset.Add(i))
                {
                    Console.WriteLine($"Number of {i} was added to the hashset");
                }
                else
                {
                    Console.WriteLine($"Number of {i} has already present in the hashset");
                }
            }

            Console.WriteLine();
        }

        private static void TestHashSetWithObjectsWithDefaultGetHashCodeMethod()
        {
            Console.WriteLine("TestHashSetWithObjectsWithDefaultGetHashCodeMethod");

            var hashset = new HashSet<ObjectWithDefaultGetHashCodeMethod>();

            Console.WriteLine("Creating two objects with the same 'number' constructor argument");

            var obj1 = new ObjectWithDefaultGetHashCodeMethod(2);
            var obj2 = new ObjectWithDefaultGetHashCodeMethod(2);

            Console.WriteLine("Adding obj1 to the hashset");
            hashset.Add(obj1);

            Console.WriteLine("Trying to add obj2 to the hashset");

            if (hashset.Add(obj2))
            {
                Console.WriteLine("obj2 added to the hashset");
            }
            else
            {
                Console.WriteLine("obj2 doesn't added to the hashset");
            }

            Console.WriteLine();
        }

        private static void TestHashSetWithObjectsWithCustomGetHashCodeMethod()
        {
            Console.WriteLine("TestHashSetWithObjectsWithCustomGetHashCodeMethod");

            var hashset = new HashSet<ObjectWithCustomGetHashCodeMethod>();

            Console.WriteLine("Creating two objects with the same 'number' constructor argument");

            var obj1 = new ObjectWithCustomGetHashCodeMethod(2);
            var obj2 = new ObjectWithCustomGetHashCodeMethod(2);

            Console.WriteLine("Adding obj1 to the hashset");
            hashset.Add(obj1);

            Console.WriteLine("Trying to add obj2 to the hashset");

            if (hashset.Add(obj2))
            {
                Console.WriteLine("obj2 added to the hashset");
            }
            else
            {
                Console.WriteLine("obj2 doesn't added to the hashset");
            }

            Console.WriteLine();
        }
    }
}
