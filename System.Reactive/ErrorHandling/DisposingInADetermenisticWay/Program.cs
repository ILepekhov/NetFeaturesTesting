using ExtensionsLibrary;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

Subject<int> subject = new();
var observable = Observable.Using(() => Disposable.Create(() => { Console.WriteLine("DISPOSED"); }), _ => subject);

Console.WriteLine("Disposed when completed");
observable.SubscribeConsole();
subject.OnCompleted();

Console.WriteLine("Disposed when error occurs");
subject = new();
observable.SubscribeConsole();
subject.OnError(new Exception("error"));

Console.WriteLine("Disposed when subscription disposed");
subject = new();
var subscription = observable.SubscribeConsole();
subscription.Dispose();