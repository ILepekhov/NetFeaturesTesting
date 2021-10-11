using ExtensionsLibrary;
using System.Reactive.Linq;
using System.Reactive.Subjects;

Subject<int> numbers = new Subject<int>();

numbers.Aggregate(
    new SortedSet<int>(),
    (largest, item) =>
    {
        largest.Add(item);

        if (largest.Count() > 2)
        {
            largest.Remove(largest.First());
        }

        return largest;
    },
    largest => largest.FirstOrDefault())
    .SubscribeConsole();

var numbersCollection = new[] { 3, 1, 4, 2, 5 };

foreach (var number in numbersCollection)
{
    Console.WriteLine($"numbers.OnNext({number});");
    numbers.OnNext(number);
}

Console.WriteLine("numbers.OnCompleted()");
numbers.OnCompleted();

Console.ReadLine();
