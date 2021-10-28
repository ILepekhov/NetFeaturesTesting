using ExtensionsLibrary;
using System.Reactive.Linq;

IObservable<string> weatherReportA =
    Observable.Throw<string>(new OutOfMemoryException());

IObservable<string> weatherReportB =
    Observable.Return("The weather B is OK");

weatherReportA
    .OnErrorResumeNext(weatherReportB)
    .SubscribeConsole("OnErrorResumeNext (source throws)");

weatherReportB
    .OnErrorResumeNext(weatherReportB)
    .SubscribeConsole("OnErrorResumeNext (source completed)");

// The difference between the 'Catch' and the 'OnErrorResumeNext' is that the 'OnErrorResumeNext' operator
// will switch to fallback observable both on error and on completed states, so 'Catch' - only on error.
// 'OnErrorResumeNext' is the mix of the 'Catch' and the 'Concat' operators.