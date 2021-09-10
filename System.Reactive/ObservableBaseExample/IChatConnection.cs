using System;

namespace ObservableBaseExample
{
    public interface IChatConnection
    {
        #region Events

        event Action<string> Received;

        event Action Closed;

        event Action<Exception> Error;

        #endregion

        #region Methods

        void Disconnect();

        #endregion
    }
}
