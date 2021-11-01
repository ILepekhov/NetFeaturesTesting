using System;

namespace ExtensionsLibrary
{
    internal sealed class WeakObserverProxy<T> : IObserver<T>
    {
        #region Fields

        private IDisposable _subscriptionToSource;
        private readonly WeakReference<IObserver<T>> _weakObserver;

        #endregion

        #region Constructor

        public WeakObserverProxy(IObserver<T> sourceObserver)
        {
            if (sourceObserver is null)
            {
                throw new ArgumentNullException(nameof(sourceObserver));
            }

            _weakObserver = new WeakReference<IObserver<T>>(sourceObserver);
        }

        #endregion

        #region Methods

        internal void SetSubscription(IDisposable subscriptionToSource)
        {
            _subscriptionToSource = subscriptionToSource ?? throw new ArgumentNullException(nameof(subscriptionToSource));
        }

        public void OnNext(T value)
        {
            NotifyObserver(o => o.OnNext(value));
        }

        public void OnError(Exception error)
        {
            NotifyObserver(o => o.OnError(error));
        }

        public void OnCompleted()
        {
            NotifyObserver(o => o.OnCompleted());
        }

        internal IDisposable AsDisposable()
        {
            return _subscriptionToSource;
        }

        #endregion

        #region Helpers

        private void NotifyObserver(Action<IObserver<T>> action)
        {
            IObserver<T> observer;

            if (_weakObserver.TryGetTarget(out observer))
            {
                action(observer);
            }
            else
            {
                _subscriptionToSource?.Dispose();
            }
        }

        #endregion
    }
}
