using ExtensionsLibrary;

namespace NaiveObservable
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = new NubmersObservable(5);
            var subscription = numbers.SubscribeConsole(nameof(numbers));
        }
    }
}
