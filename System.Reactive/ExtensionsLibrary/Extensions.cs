using System;

namespace ExtensionsLibrary
{
    public static class Extensions
    {
        #region Extension methods

        public static IDisposable SubscribeConsole<T>(this IObservable<T> observable, string name = "")
        {
            return observable.Subscribe(new ConsoleObserver<T>(name));
        }

        #endregion
    }
}
