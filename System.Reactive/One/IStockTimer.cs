using System;

namespace One
{
    public interface IStockTimer
    {
        #region Events

        event EventHandler<StockTick> StockTick;

        #endregion
    }
}
