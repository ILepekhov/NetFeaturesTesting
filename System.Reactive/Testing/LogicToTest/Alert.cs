namespace LogicToTest
{
    public sealed class Alert
    {
        #region Properties

        public string Message { get; }

        public DateTimeOffset Date { get; }

        #endregion

        #region Constructor

        public Alert(string message, DateTimeOffset date)
        {
            Message = message;
            Date = date;
        }

        #endregion
    }
}
