using ExtensionsLibrary;
using System.Reactive.Linq;

// MergeAllObservablesSimultaneously();
MergeObservablesWithConcurrencyLimitations();

Console.ReadLine();

void MergeAllObservablesSimultaneously()
{
    IObservable<string> texts = new[] { "Hello", "World" }.ToObservable();

    texts
        .Select(txt => Observable.Return(txt + "-Result"))
        .Merge() // IObservable<IObservale<string>> -> IObservable<string>
        .SubscribeConsole();
}

void MergeObservablesWithConcurrencyLimitations()
{
    IObservable<string> first = Observable.Interval(TimeSpan.FromSeconds(1))
        .Select(i => "First " + i)
        .Do(x => Console.WriteLine("DO " + x))
        .Take(2);

    IObservable<string> second = Observable.Interval(TimeSpan.FromSeconds(1))
        .Select(i => "Second " + i)
        .Do(x => Console.WriteLine("DO " + x))
        .Take(2);

    IObservable<string> third = Observable.Interval(TimeSpan.FromSeconds(1))
        .Select(i => "Third " + i)
        .Do(x => Console.WriteLine("DO " + x))
        .Take(2);

    new[] { first, second, third }.ToObservable()
        .Merge(2) // setting limit for the simultaneous emissions
        .SubscribeConsole("Merge with 2 concurrent subscriptions");
}
