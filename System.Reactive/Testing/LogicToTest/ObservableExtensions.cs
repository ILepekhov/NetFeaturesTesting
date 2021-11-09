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
    }
}