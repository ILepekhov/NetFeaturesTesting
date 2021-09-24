using ExtensionsLibrary;
using System;
using System.Reactive.Linq;

namespace SkipWhileTakeWhileExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Observable.Range(1, 10)
                .SkipWhile(x => x < 2)
                .TakeWhile(x => x < 7)
                .SubscribeConsole();

            Console.ReadLine();
        }
    }
}
