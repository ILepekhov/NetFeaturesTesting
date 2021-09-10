using System;

namespace ExtensionsLibrary
{
    public sealed class ConsoleObserver<T> : IObserver<T>
    {
        #region Fields

        private readonly string _name;

        #endregion

        #region Constructor

        public ConsoleObserver(string name = "")
        {
            _name = name;
        }

        #endregion

        #region Methods

        public void OnCompleted()
        {
            Console.WriteLine("{0} - OnCompleted()", _name);
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("{0} - OnError:", _name);
            Console.WriteLine("\t {0}", error);
        }

        public void OnNext(T value)
        {
            Console.WriteLine("{0} - OnNext({1})", _name, value);
        }

        #endregion
    }
}
