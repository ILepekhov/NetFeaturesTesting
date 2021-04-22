namespace HashSetTesting
{
    public sealed class ObjectWithCustomGetHashCodeMethod
    {
        #region Properties

        public int Number { get; set; }

        #endregion

        #region Constructor

        public ObjectWithCustomGetHashCodeMethod(int number)
        {
            Number = number;
        }

        #endregion

        #region Methods

        public override int GetHashCode()
        {
            return Number;
        }

        public override bool Equals(object obj)
        {
            if (obj is ObjectWithCustomGetHashCodeMethod objectWithCustomGetHashCodeMethod)
            {
                return Number == objectWithCustomGetHashCodeMethod.Number;
            }

            return false;
        }

        #endregion
    }
}
