using System.Reactive.Linq;

internal static class IObservableExtensions
{
    public static IObservable<T> LogWithThread<T>(this IObservable<T> observable, string title = "")
    {
        return Observable.Defer(() =>
        {
            Console.WriteLine($"{title} Subscription happened on Thread: {Environment.CurrentManagedThreadId}");

            return observable.Do(
                x => Console.WriteLine($"{title} - OnNext({x}) Thread: {Environment.CurrentManagedThreadId}"),
                ex =>
                {
                    Console.WriteLine($"{title} - OnError() Thread: {Environment.CurrentManagedThreadId}");
                    Console.WriteLine($"\t {ex}");
                },
                () => Console.WriteLine($"{title} - OnCompleted() Thread: {Environment.CurrentManagedThreadId}"));
        });
    }
}