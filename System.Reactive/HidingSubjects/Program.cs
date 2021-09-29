using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace HidingSubjects
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Subject<int> sbj = new Subject<int>();

            var proxy = sbj.AsObservable(); // now i can expose proxy as IObservable<int> property in a class for example

            var castToSubject = proxy as Subject<int>;
            var castToObserver = proxy as IObserver<int>;

            Console.WriteLine("proxy as Subject<int> is '{0}'", castToSubject is null ? "null" : "not null");
            Console.WriteLine("proxy as IObserver<int> is '{0}'", castToObserver is null ? "null" : "not null");
        }
    }
}
