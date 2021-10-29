using ExtensionsLibrary;
using System.Reactive.Linq;
using System.Reactive.Subjects;

Console.WriteLine("Successful complete");
Observable.Interval(TimeSpan.FromSeconds(1))
    .Take(3)
    .Finally(() => Console.WriteLine("Finally Code"))
    .SubscribeConsole("Finally on Complete");

Console.ReadLine();

Console.WriteLine("Error termination");
Observable.Throw<Exception>(new Exception("Error"))
    .Finally(() => Console.WriteLine("Finnaly Code"))
    .SubscribeConsole("Subscription on Error");

Console.ReadLine();

Console.WriteLine("Unsubscribing");
Subject<int> subject = new();
var subscription = subject.AsObservable()
    .Finally(() => Console.WriteLine("Finally Code"))
    .SubscribeConsole("Finally on observer disposing");
subscription.Dispose();

Console.ReadLine();