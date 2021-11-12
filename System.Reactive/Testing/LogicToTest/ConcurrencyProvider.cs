using System.Reactive.Concurrency;

namespace LogicToTest
{
    internal sealed class ConcurrencyProvider : IConcurrencyProvider
    {
        #region Properties

        public IScheduler TimeBasedOperations { get; }

        public IScheduler Task { get; }

        public IScheduler Thread { get; }

        public IScheduler Dispatcher { get; }

        #endregion

        #region Constructor

        public ConcurrencyProvider()
        {
            TimeBasedOperations = DefaultScheduler.Instance;
            Task = TaskPoolScheduler.Default;
            Thread = NewThreadScheduler.Default;

#if HAS_DISPATCHER
            Dispatcher = DispatcherScheduler.Current;
#else
            Dispatcher = CurrentThreadScheduler.Instance;
#endif
        }

        #endregion
    }
}
