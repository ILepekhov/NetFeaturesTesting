using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace One
{
    public sealed class FakeStockTimer : IStockTimer
    {
        #region Fields

        private CancellationTokenSource _cts;

        #endregion

        #region Events

        public event EventHandler<StockTick> StockTick;

        #endregion

        #region Methods

        public void StartPolling(List<string> quoteSymbols, TimeSpan pollFrequency = default)
        {
            if (quoteSymbols is null)
            {
                throw new ArgumentNullException(nameof(quoteSymbols));
            }

            CancelPreviousTask();

            _cts = new CancellationTokenSource();

            if (pollFrequency == default)
            {
                pollFrequency = TimeSpan.FromMilliseconds(500);
            }

            foreach (var symbol in quoteSymbols)
            {
                _ = Task.Factory.StartNew(() => PollingLoop(symbol, pollFrequency, _cts.Token), _cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }
        }

        public void StopPolling()
        {
            CancelPreviousTask();
        }

        #endregion

        #region Helpers

        private void CancelPreviousTask()
        {
            if (_cts is not null)
            {
                if (_cts.IsCancellationRequested == false)
                {
                    _cts.Cancel();
                }

                _cts.Dispose();

                _cts = null;
            }
        }

        private async Task PollingLoop(string quoteSymbol, TimeSpan pollFrequency, CancellationToken cancellationToken)
        {
            Console.WriteLine($"FakeStockTimer -> PollingLoop() started for '{quoteSymbol}'");

            Random generator = new(quoteSymbol.GetHashCode());

            try
            {
                while (true)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    decimal nextPrice = generator.Next(80, 101);

                    FireStockTick(quoteSymbol, nextPrice);

                    await Task.Delay(pollFrequency, cancellationToken);
                }
            }
            finally
            {
                Console.WriteLine($"FakeStockTimer -> PollingLoop() stopped for '{quoteSymbol}'");
            }
        }

        private void FireStockTick(string quoteSymbol, decimal price)
        {
            if (string.IsNullOrWhiteSpace(quoteSymbol))
            {
                throw new ArgumentException($"'{nameof(quoteSymbol)}' cannot be null or whitespace.", nameof(quoteSymbol));
            }

            if (price <= 0)
            {
                throw new ArgumentException($"'{nameof(price)}' cannot be less or equals 0.", nameof(price));
            }

            _ = Task.Run(() => StockTick?.Invoke(this, new StockTick(quoteSymbol, price)));
        }

        #endregion
    }
}
