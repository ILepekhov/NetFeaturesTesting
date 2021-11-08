using System.Reactive.Disposables;

var inner = Disposable.Create(() => Console.WriteLine("Disposing inner-disposable"));

var refCountDisposable = new RefCountDisposable(inner);

var referenceOne = refCountDisposable.GetDisposable();
var referenceTwo = refCountDisposable.GetDisposable();

refCountDisposable.Dispose();

Console.WriteLine("Disposing referenceOne");
referenceOne.Dispose();

Console.WriteLine("Disposing referenceTwo");
referenceTwo.Dispose();

Console.ReadLine();