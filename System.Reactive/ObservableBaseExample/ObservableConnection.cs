﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace ObservableBaseExample
{
    public sealed class ObservableConnection : ObservableBase<string>
    {
        #region Fields

        private readonly IChatConnection _chatConnection;

        #endregion

        #region Constructor

        public ObservableConnection(IChatConnection chatConnection)
        {
            _chatConnection = chatConnection ?? throw new ArgumentNullException(nameof(chatConnection));
        }

        #endregion

        #region Overrides

        protected override IDisposable SubscribeCore(IObserver<string> observer)
        {
            Action<string> received = message => observer.OnNext(message);

            Action closed = () => observer.OnCompleted();

            Action<Exception> error = ex => observer.OnError(ex);

            _chatConnection.Received += received;
            _chatConnection.Closed += closed;
            _chatConnection.Error += error;

            return Disposable.Create(() =>
            {
                _chatConnection.Received -= received;
                _chatConnection.Closed -= closed;
                _chatConnection.Error -= error;

                _chatConnection.Disconnect();
            });
        }

        #endregion
    }
}
