using System;

namespace ObservableBaseExample
{
    public static class ChatExtensions
    {
        #region Extension methods

        public static IObservable<string> ToObservable(this IChatConnection chatConnection)
        {
            return new ObservableConnection(chatConnection);
        }

        #endregion
    }
}
