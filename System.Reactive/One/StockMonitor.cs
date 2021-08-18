using System;
using System.Collections.Generic;

namespace One
{
    public sealed class StockMonitor : IDisposable
    {
        #region Fields

        private readonly IStockTimer _stockTimer;
        private readonly Dictionary<string, StockInfo> _stockInfos;
        private readonly object _stockInfosLock;

        private bool _disposed;

        #endregion

        #region Constructor

        public StockMonitor(IStockTimer stockTimer)
        {
            _stockTimer = stockTimer ?? throw new ArgumentNullException(nameof(stockTimer));

            _stockInfos = new Dictionary<string, StockInfo>();
            _stockInfosLock = new object();

            _stockTimer.StockTick += HandleStockTick;
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            if (!_disposed)
            {
                _stockTimer.StockTick -= HandleStockTick;

                lock (_stockInfosLock)
                {
                    _stockInfos.Clear();
                }

                _disposed = true;
            }
        }

        #endregion

        #region Helpers

        private void HandleStockTick(object sender, StockTick stockTick)
        {
            const decimal maxChangeRatio = 0.1m;

            lock (_stockInfosLock)
            {
                if (_stockInfos.TryGetValue(stockTick.QuoteSymbol, out var stockInfo))
                {
                    var priceDiff = stockTick.Price - stockInfo.PrevPrice;
                    var changeRatio = Math.Abs(priceDiff / stockInfo.PrevPrice);

                    if (changeRatio > maxChangeRatio)
                    {
                        Console.WriteLine($"Stock '{stockTick.QuoteSymbol}' has changed with {changeRatio:0.00} ratio. Old price: {stockInfo.PrevPrice}, new price: {stockTick.Price}");
                    }

                    _stockInfos[stockTick.QuoteSymbol].PrevPrice = stockTick.Price;
                }
                else
                {
                    _stockInfos.Add(stockTick.QuoteSymbol, new StockInfo(stockTick.QuoteSymbol, stockTick.Price));
                }
            }
        }

        #endregion
    }
}
