using System;
using System.Collections.Generic;

namespace IndicesAndRanges
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowIndicesExample();

            ShowRandesExample();

            Console.WriteLine("Press any key to exit...");

            Console.ReadKey();
        }

        private static void ShowIndicesExample()
        {
            Console.WriteLine("Indicies example");

            Index indexOne = 0;
            Index indexTwo = new Index(1);
            Index indexThree = new Index(2, true);
            Index indexFour = ^1;

            var array = new[] { 1, 2, 3, 4 };
            var list = new List<int> { 1, 2, 3, 4 };
            var @string = "1234";

            Write(array, list, @string, indexOne);
            Write(array, list, @string, indexTwo);
            Write(array, list, @string, indexThree);
            Write(array, list, @string, indexFour);

            Console.WriteLine();
        }

        private static void Write(int[] array, List<int> list, string @string, Index index)
        {
            var strIndex = index.ToString().PadLeft(2);

            Console.WriteLine($"array[{strIndex}] = {array[index]}; list[{strIndex}] = {list[index]}; string[{strIndex}] = {@string[index]}");
        }

        private static void ShowRandesExample()
        {
            var array = new[] { 1, 2, 3, 4 };

            Write(array, 0..4);
            Write(array, 0..);
            Write(array, ..^0);
            Write(array, 0..^0);
            Write(array, ..);
            Write(array, ^2..^0);
            Write(array, 0..3);
            Write(array, ..3);
            Write(array, ..0);
            Write(array, ..^7);

        }

        private static void Write(int[] array, Range range)
        {
            var rangeStr = $"{range.Start.ToString().PadLeft(2)}..{range.End.ToString().PadLeft(2)}";

            try
            {
                Console.WriteLine($"list[{rangeStr}] = {string.Join(", ", array[range])}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid range {rangeStr}. {ex.Message}");
            }
        }
    }
}
