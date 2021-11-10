using ExtensionsLibrary;
using Microsoft.Reactive.Testing;
using System.Reactive;

var testScheduler = new TestScheduler();

ITestableObservable<int> coldObservable = testScheduler.CreateColdObservable<int>(
    new Recorded<Notification<int>>(20, Notification.CreateOnNext<int>(1)),
    new Recorded<Notification<int>>(40, Notification.CreateOnNext<int>(2)),
    new Recorded<Notification<int>>(60, Notification.CreateOnCompleted<int>()));

coldObservable.SubscribeConsole("test");

testScheduler.Start();

Console.ReadLine();