using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;

//SimpleExample();
MoreAdvancedExample();

Console.ReadLine();

void SimpleExample()
{
    IScheduler scheduler = NewThreadScheduler.Default;

    IDisposable scheduling = scheduler.Schedule(
        Unit.Default,
        TimeSpan.FromSeconds(2),
        (scdlr, _) =>
        {
            Console.WriteLine("Hello World, Now {0}", scdlr.Now);

            return Disposable.Empty;
        });
}

void MoreAdvancedExample()
{
    IScheduler scheduler = NewThreadScheduler.Default;

    Func<IScheduler, int, IDisposable> action = null;

    action = (scdlr, callNumber) =>
    {
        Console.WriteLine($"Hello {callNumber}, Now: {scdlr.Now}, Thread: {Thread.CurrentThread.ManagedThreadId}");

        return scdlr.Schedule(callNumber + 1, TimeSpan.FromSeconds(2), action);
    };

    IDisposable scheduling = scheduler.Schedule(0, TimeSpan.FromSeconds(2), action);

    Console.WriteLine("Press Enter to stop");

    Console.ReadLine();

    scheduling.Dispose();
}
