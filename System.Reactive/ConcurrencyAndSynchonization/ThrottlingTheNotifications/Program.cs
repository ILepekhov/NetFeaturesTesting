using ExtensionsLibrary;
using System.Reactive.Linq;

//PlainThrottling();

VariableThrottling();

Console.ReadLine();

void PlainThrottling()
{
    var obserbable = Observable
        .Return("Update A")
        .Concat(Observable.Timer(TimeSpan.FromSeconds(2)).Select(_ => "Update B"))
        .Concat(Observable.Timer(TimeSpan.FromSeconds(1)).Select(_ => "Update C"))
        .Concat(Observable.Timer(TimeSpan.FromSeconds(1)).Select(_ => "Update D"))
        .Concat(Observable.Timer(TimeSpan.FromSeconds(3)).Select(_ => "Update E"));

    obserbable.Throttle(TimeSpan.FromSeconds(2))
        .Timestamp()
        .SubscribeConsole("Throttle");
}

void VariableThrottling()
{
    var observable = Observable
        .Return("Msg A")
        .Concat(Observable.Timer(TimeSpan.FromSeconds(2)).Select(_ => "Msg B"))
        .Concat(Observable.Timer(TimeSpan.FromSeconds(1)).Select(_ => "Immediate Update"))
        .Concat(Observable.Timer(TimeSpan.FromSeconds(1)).Select(_ => "Msg D"))
        .Concat(Observable.Timer(TimeSpan.FromSeconds(3)).Select(_ => "Msg E"));

    observable.Throttle(x => x == "Immediate Update" ? Observable.Empty<long>() : Observable.Timer(TimeSpan.FromSeconds(2)))
        .SubscribeConsole("Variable Throttling");
}
