using ExtensionsLibrary;
using System.Reactive.Linq;
using System.Reactive.Subjects;

ExampleOne();

Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();

ExampleTwo();

Console.ReadLine();

void ExampleOne()
{
    Subject<double> speedReadingsSubject = new();
    IObservable<double> speedReadings = speedReadingsSubject.AsObservable();

    double timeDelta = 0.0002777777777777778; //1 second in hours unit

    var accelerations =
        from buffer in speedReadings.Buffer(2, 1)
        where buffer.Count == 2
        let speedDelta = buffer[1] - buffer[0]
        select speedDelta / timeDelta;

    accelerations.SubscribeConsole("Acceleration");

    speedReadingsSubject.OnNext(50d);
    speedReadingsSubject.OnNext(51d);
    speedReadingsSubject.OnNext(51.5d);
    speedReadingsSubject.OnNext(53d);
    speedReadingsSubject.OnNext(52d);
    speedReadingsSubject.OnCompleted();
}

void ExampleTwo()
{
    IObservable<string> coldMessages = Observable.Interval(TimeSpan.FromMilliseconds(50))
        .Take(4)
        .Select(x => "Message " + x);

    IObservable<string> messages = coldMessages.Concat(coldMessages.DelaySubscription(TimeSpan.FromMilliseconds(200)))
        .Publish()
        .RefCount();

    IObservable<string> signalObservable = messages.Throttle(TimeSpan.FromMilliseconds(100));

    messages.Buffer(signalObservable)
        .SelectMany((b, i) => b.Select(m => $"Buffer {i} - {m}"))
        .SubscribeConsole("Hi-Rate Messages");
}
