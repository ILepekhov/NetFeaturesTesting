using ExtensionsLibrary;
using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace AsyncSubjectExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tcs = new TaskCompletionSource<bool>();
            var task = tcs.Task;

            AsyncSubject<bool> sbj = new AsyncSubject<bool>();

            task.ContinueWith(t =>
            {
                switch (t.Status)
                {
                    case TaskStatus.RanToCompletion:
                        sbj.OnNext(t.Result);
                        sbj.OnCompleted();
                        break;
                    case TaskStatus.Faulted:
                        sbj.OnError(t.Exception.InnerException);
                        break;
                    case TaskStatus.Canceled:
                        sbj.OnError(new TaskCanceledException(t));
                        break;
                }
            }, TaskContinuationOptions.ExecuteSynchronously);

            tcs.SetResult(true); // sets the Task completion before the observer subscribes

            // AsyncSubject emits only last one value
            sbj.SubscribeConsole("AsyncSubject");

            Console.ReadLine();
        }
    }
}
