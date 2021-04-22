namespace HashSetTesting
{
    public sealed class ObjectWithDefaultGetHashCodeMethod
    {
        #region Properties

        public int Number { get; set; }

        #endregion

        #region Constructor

        public ObjectWithDefaultGetHashCodeMethod(int number)
        {
            Number = number;
        }

        #endregion
    }
}
