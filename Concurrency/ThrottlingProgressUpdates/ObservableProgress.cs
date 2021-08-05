using System;
using System.Reactive.Linq;
using System.Threading;

namespace ThrottlingProgressUpdates
{
    public static class ObservableProgress
    {
        private sealed class EventProgress<T> : IProgress<T>
        {
            void IProgress<T>.Report(T value) => OnReport?.Invoke(value);
            public event Action<T> OnReport;
        }

        public static (IObservable<T>, IProgress<T>) Create<T>()
        {
            var progress = new EventProgress<T>();
            var observable = Observable.FromEvent<T>(
                handler => progress.OnReport += handler,
                handler => progress.OnReport -= handler);

            return (observable, progress);
        }

        // Note: this must be called from the UI thread
        public static (IObservable<T>, IProgress<T>) CreateForUi<T>(TimeSpan? sampleInterval = null)
        {
            var (observable, progress) = Create<T>();

            observable = observable
                .Sample(sampleInterval ?? TimeSpan.FromMilliseconds(100))
                .ObserveOn(SynchronizationContext.Current);

            return (observable, progress);
        }
    }
}
