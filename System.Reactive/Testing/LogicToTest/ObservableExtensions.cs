using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace LogicToTest
{
    public static class ObservableExtensions
    {
        public static IObservable<T> FilterBursts<T>(this IObservable<T> source, int burstSize)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (burstSize <= 0)
            {
                throw new ArgumentException($"{nameof(burstSize)} must be greater than 0", nameof(burstSize));
            }

            return source
                .Window(burstSize)
                .SelectMany(window => window.Take(1));
        }

        public static IObservable<T> FilterBursts<T>(this IObservable<T> source, TimeSpan maximumDistance)
        {
            return source.FilterBursts(maximumDistance, DefaultScheduler.Instance);
        }

        public static IObservable<T> FilterBursts<T>(this IObservable<T> source, TimeSpan maximumDistance, IScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.Publish(xs =>
            {
                var windowBoundaries = xs.Throttle(maximumDistance, scheduler);

                return xs.Window(windowBoundaries).SelectMany(window => window.Take(1));
            });
        }

        public static IObservable<T> FilterBursts<T>(this IObservable<T> source,
            TimeSpan maximalDistance,
            TimeSpan maximalBurstDuration,
            IScheduler scheduler)
        {
            return source.Publish(xs =>
            {
                var maxDurationPassed = xs.Delay(maximalBurstDuration, scheduler).Take(1);
                var windowBoundary = xs.Throttle(maximalDistance, scheduler).Merge(maxDurationPassed);

                return xs.Window(() => windowBoundary).SelectMany(window => window.Take(1));
            });
        }
    }
}