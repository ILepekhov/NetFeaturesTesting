using ExtensionsLibrary;
using SerialDisposable;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

IScheduler scheduler = ImmediateScheduler.Instance;

var subscription = Observable.Timer(TimeSpan.FromSeconds(1))
    .MySubscribeOn(scheduler)
    .SubscribeConsole("SerialDisposable");

subscription.Dispose();

Console.ReadLine();