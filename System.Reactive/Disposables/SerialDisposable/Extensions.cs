using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace SerialDisposable
{
    internal static class Extensions
    {
        public static IObservable<TSource> MySubscribeOn<TSource>(this IObservable<TSource> source, IScheduler scheduler)
        {
            return Observable.Create<TSource>(observer =>
            {
                var d = new System.Reactive.Disposables.SerialDisposable();

                d.Disposable = scheduler.Schedule(() =>
                {
                    Console.WriteLine("Dispose One");

                    d.Disposable = new System.Reactive.Disposables.ScheduledDisposable(scheduler, System.Reactive.Disposables.Disposable.Create(() =>
                    {
                        Console.WriteLine("Dispose Two");
                        source.SubscribeSafe(observer);
                    }));
                });

                return d;
            });
        }
    }
}
