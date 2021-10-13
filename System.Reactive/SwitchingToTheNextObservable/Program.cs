using ExtensionsLibrary;
using System.Reactive.Linq;
using System.Reactive.Subjects;

//ExampleOfSwitchingToTheSlowestObservale();

ExampleOfSwitchingToTheFirstObservableToEmit();

Console.ReadLine();

void ExampleOfSwitchingToTheSlowestObservale()
{
    var textsSubject = new Subject<string>();

    IObservable<string> texts = textsSubject.AsObservable();

    texts
        .Select(txt => Observable.Return(txt + "-Result").Delay(TimeSpan.FromMilliseconds(txt == "R1" ? 10 : 0)))
        .Switch()
        .SubscribeConsole("Shitching observables");

    textsSubject.OnNext("R1");
    textsSubject.OnNext("R2"); // the R1 result will be ignored because of the R2 result will apear earlier
    Thread.Sleep(TimeSpan.FromMilliseconds(100));
    textsSubject.OnNext("R3");
}

void ExampleOfSwitchingToTheFirstObservableToEmit()
{
    /*
     * The Amb (short for ambiguity) operator works similarly to the Switch operator,
     * but instead of switching to a new observable each time a new one is emitted, Amb
     * switches only to the first observable to emit. Think of it this way: if all the observables
     * are considered equally fit as the source, you want them to duel, and the first one to
     * shoot wins.
     */

    var server1 = Observable.Interval(TimeSpan.FromSeconds(2))
        .Select(i => "Server1 - " + i);

    var server2 = Observable.Interval(TimeSpan.FromSeconds(1))
        .Select(i => "Server2 - " + i);

    Observable.Amb(server1, server2)
        .Take(3)
        .SubscribeConsole("Ambiguity");
}
