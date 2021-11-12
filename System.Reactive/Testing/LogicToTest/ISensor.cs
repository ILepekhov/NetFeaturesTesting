namespace LogicToTest
{
    public interface ISensor<T>
    {
        #region Properties

        IObservable<T> Readings { get; }

        #endregion
    }
}
