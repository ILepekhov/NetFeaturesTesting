using ExtensionsLibrary;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

//RangeWithBlocking();
RangeWithNoBlocking();

Console.WriteLine("Press Enter to stop");
Console.ReadLine();

void RangeWithBlocking()
{
    Console.WriteLine($"RangeWithBlocking, threadId {Thread.CurrentThread.ManagedThreadId}");

    var subscription = Observable.Range(1, 5)
        .Repeat()
        .SubscribeConsole($"RangeWithNoBlocking, threadId {Thread.CurrentThread.ManagedThreadId}");

    // won't come here

    subscription.Dispose();
}

void RangeWithNoBlocking()
{
    Console.WriteLine($"RangeWithNoBlocking, threadId {Thread.CurrentThread.ManagedThreadId}");

    var subscription = Observable.Range(1, 5, NewThreadScheduler.Default)
        .Repeat()
        .SubscribeConsole($"RangeWithNoBlocking, threadId {Thread.CurrentThread.ManagedThreadId}");

    subscription.Dispose();
}
