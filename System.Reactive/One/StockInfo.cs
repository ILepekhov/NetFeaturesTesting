using System;

namespace One
{
    public sealed class StockInfo
    {
        #region Properties

        public string Symbol { get; }

        public decimal PrevPrice { get; set; }

        #endregion

        #region Constructor

        public StockInfo(string symbol, decimal prevPrice)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentException($"'{nameof(symbol)}' cannot be null or whitespace.", nameof(symbol));
            }

            if (prevPrice <= 0)
            {
                throw new ArgumentException($"'{nameof(prevPrice)}' cannot be less or equals 0.", nameof(prevPrice));
            }

            Symbol = symbol;
            PrevPrice = prevPrice;
        }

        #endregion
    }
}
