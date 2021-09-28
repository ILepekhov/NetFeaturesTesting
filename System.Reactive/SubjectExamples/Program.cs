using ExtensionsLibrary;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace SubjectExamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintCommands();

            while (true)
            {
                var key = Console.ReadKey().Key;

                Console.WriteLine();

                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        SimpleBroagcastingExample();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        IncorrectMergeWithSubjectExample();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        CorrectMergeExample();
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        break;
                    default:
                        PrintCommands();
                        break;
                }

                if (key == ConsoleKey.D4 || key == ConsoleKey.NumPad4)
                {
                    break;
                }
            }
        }

        private static void PrintCommands()
        {
            Console.WriteLine("Select a command (press a key):");
            Console.WriteLine("1 - run " + nameof(SimpleBroagcastingExample));
            Console.WriteLine("2 - run " + nameof(IncorrectMergeWithSubjectExample));
            Console.WriteLine("3 - run " + nameof(CorrectMergeExample));
            Console.WriteLine("4 - exit");
        }

        private static void SimpleBroagcastingExample()
        {
            Console.WriteLine(nameof(SimpleBroagcastingExample));

            Subject<long> subject = new Subject<long>();

            subject.SubscribeConsole("Subscriber 1");
            subject.SubscribeConsole("Subscriber 2");

            Observable.Interval(TimeSpan.FromSeconds(1))
                .Take(3)
                .Subscribe(subject);
        }

        private static void IncorrectMergeWithSubjectExample()
        {
            Console.WriteLine(nameof(IncorrectMergeWithSubjectExample));

            Subject<string> subject= new Subject<string>();            

            Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(x => $"First: {x}")
                .Take(5)
                .Subscribe(subject);

            Observable.Interval(TimeSpan.FromSeconds(2))
                .Select(x => $"Second: {x}")
                .Take(5)
                .Subscribe(subject);

            subject.SubscribeConsole();
        }

        private static void CorrectMergeExample()
        {
            Console.WriteLine(nameof(CorrectMergeExample));

            var observable1 = Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(x => $"First: {x}")
                .Take(5);

            var observable2 = Observable.Interval(TimeSpan.FromSeconds(2))
                .Select(x => $"Second: {x}")
                .Take(5);

            observable1
                .Merge(observable2)
                .SubscribeConsole();
        }
    }
}
