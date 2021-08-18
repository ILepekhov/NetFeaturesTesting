using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace One
{
    public sealed class StockMonitorWithObservable : IDisposable
    {
        #region Const

        private const decimal MaxChangeRatio = 0.1m;

        #endregion

        #region Fields

        private readonly IDisposable _subscription;

        private bool _disposed;

        #endregion

        #region Constructor

        public StockMonitorWithObservable(IStockTimer stockTimer)
        {
            if (stockTimer is null)
            {
                throw new ArgumentNullException(nameof(stockTimer));
            }

            var ticks = Observable.FromEventPattern<EventHandler<StockTick>, StockTick>(
                    handler => stockTimer.StockTick += handler,
                    handler => stockTimer.StockTick -= handler)
                .Select(tickEvent => tickEvent.EventArgs) // unwraping the EventArgs
                .Synchronize(); // dealing with concurrency

            var drasticChanges =
                from tick in ticks
                group tick by tick.QuoteSymbol into company // grouping stocks by symbol
                from tickPair in company.Buffer(2, 1) // finding the difference between ticks
                let changeRatio = Math.Abs((tickPair[1].Price - tickPair[0].Price) / tickPair[0].Price)
                where changeRatio > MaxChangeRatio
                select new DrasticChange(company.Key, changeRatio, tickPair[0].Price, tickPair[1].Price);

            _subscription = drasticChanges.Subscribe(change =>
            {
                Console.WriteLine($"Stock '{change.Symbol}' has changed with {change.ChangeRatio:0.00} ratio. Old price: {change.OldPrice}, new price: {change.NewPrice}");
            },
            ex => { /* code that handles errors */ },
            () => { /* code that handles the observable completeness */ });
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            if (!_disposed)
            {
                _subscription.Dispose();

                _disposed = true;
            }
        }

        #endregion
    }
}
