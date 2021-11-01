using System;
using System.Reactive.Linq;

namespace ExtensionsLibrary
{
    public static class Extensions
    {
        #region Extension methods

        public static IDisposable SubscribeConsole<T>(this IObservable<T> observable, string name = "")
        {
            return observable.Subscribe(new ConsoleObserver<T>(name));
        }

        public static IObservable<T> Log<T>(this IObservable<T> observable, string message = "")
        {
            return observable.Do(
                x => Console.WriteLine("{0} - OnNext({1})", message, x),
                ex =>
                {
                    Console.WriteLine("{0} - OnError():", message);
                    Console.WriteLine("\t {0}", ex);
                },
                () => Console.WriteLine("{0} - OnCompleted():", message));
        }

        public static IObservable<T> AsWeakObservable<T>(this IObservable<T> source)
        {
            return Observable.Create<T>(o =>
            {
                var weakObserverProxy = new WeakObserverProxy<T>(o);
                var subscription = source.Subscribe(weakObserverProxy);
                weakObserverProxy.SetSubscription(subscription);
                return weakObserverProxy.AsDisposable();
            });
        }

        #endregion
    }
}
