using System;
using System.Reactive.Disposables;

namespace NaiveObservable
{
    public sealed class NubmersObservable : IObservable<int>
    {
        #region Fields

        private readonly int _amount;

        #endregion

        #region Constructor

        public NubmersObservable(int amount)
        {
            _amount = amount;
        }

        #endregion

        #region Methods

        public IDisposable Subscribe(IObserver<int> observer)
        {
            for (int i = 0; i < _amount; i++)
            {
                observer.OnNext(i);
            }

            observer.OnCompleted();

            return Disposable.Empty;
        }

        #endregion
    }
}
