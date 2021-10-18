using ExtensionsLibrary;
using System.Reactive.Linq;
using System.Reactive.Subjects;

Subject<decimal> donationsSubject = new();
IObservable<decimal> donations = donationsSubject.AsObservable();

Console.WriteLine("Shift 1: 50$, 55$, 60$");
Console.WriteLine("Shift 2: 49$, 48$, 45$");

var windows = donations.Window(TimeSpan.FromSeconds(1));

var donationsSums =
    from window in windows.Do(_ => Console.WriteLine("New window"))
    from sum in window.Scan((prevSum, donation) => prevSum + donation)
    select sum;

donationsSums.SubscribeConsole("donations in shift");

donationsSubject.OnNext(50);
donationsSubject.OnNext(55);
donationsSubject.OnNext(60);

Thread.Sleep(TimeSpan.FromMilliseconds(1100));

donationsSubject.OnNext(49);
donationsSubject.OnNext(48);
donationsSubject.OnNext(45);

donationsSubject.OnCompleted();

Console.ReadLine();