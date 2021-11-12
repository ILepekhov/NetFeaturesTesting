using System.Reactive.Concurrency;

namespace LogicToTest
{
    public interface IConcurrencyProvider
    {
        #region Properties

        IScheduler TimeBasedOperations { get; }

        IScheduler Task { get; }

        IScheduler Thread { get; }

        IScheduler Dispatcher { get; }

        #endregion
    }
}
