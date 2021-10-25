using ExtensionsLibrary;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;

var eventLoopScheduler = new EventLoopScheduler();

var subscription = Observable
    .Interval(TimeSpan.FromSeconds(1))
    .Do(x => Console.WriteLine("Inside Do ({0})", x))
    .SubscribeOn(eventLoopScheduler)
    .SubscribeConsole();

Thread.Sleep(TimeSpan.FromSeconds(1));

eventLoopScheduler.Schedule(1, (s, state) =>
{
    Console.WriteLine("Before sleep");
    Thread.Sleep(TimeSpan.FromSeconds(3));
    Console.WriteLine("After sleep");

    return Disposable.Empty;
});



subscription.Dispose();

Console.WriteLine("Subscription disposed");
Console.ReadLine();
