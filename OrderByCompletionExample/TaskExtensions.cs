using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderByCompletionExample
{
    public static class TaskExtensions
    {
        #region Extension methods

        /// <summary>
        /// Returns a sequence of tasks which will be observed to complete with the same set of results
        /// as the given input tasks, but in the order in which the original tasks complete.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputTasks"></param>
        /// <returns></returns>
        public static IEnumerable<Task<T>> OrderByCompletion<T>(this IEnumerable<Task<T>> inputTasks)
        {
            // copy the input so we know it'll be stable, and we don't evaluate it twice
            var inputTaskList = inputTasks.ToList();

            var completionSourceList = new List<TaskCompletionSource<T>>(inputTaskList.Count);
            for (int i = 0; i < inputTaskList.Count; i++)
            {
                completionSourceList.Add(new TaskCompletionSource<T>());
            }

            // at any one time, this is "the index of the box we've just filled".
            // it would be nice to make it newxIndex and start with 0, but Intelocked.Increment
            // returns the incremented value...
            int prevIndex = -1;

            // we don't have to create this outside the loop, but it makes it clearer
            // that the continuation is the same for all tasks.
            Action<Task<T>> continuation = completedTask =>
            {
                int index = Interlocked.Increment(ref prevIndex);
                var source = completionSourceList[index];
                PropagateResult(completedTask, source);
            };

            foreach (var inputTask in inputTaskList)
            {
                // TODO: work out whether TaskScheduler.Default is really the right one to use.
                inputTask.ContinueWith(continuation, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
            }

            return completionSourceList.Select(source => source.Task);
        }

        #endregion

        #region Helpers

        private static void PropagateResult<T>(Task<T> completedTask, TaskCompletionSource<T> completionSource)
        {
            switch (completedTask.Status)
            {
                case TaskStatus.Canceled:
                    completionSource.TrySetCanceled();
                    break;
                case TaskStatus.Faulted:
                    completionSource.TrySetException(completedTask.Exception.InnerExceptions);
                    break;
                case TaskStatus.RanToCompletion:
                    completionSource.TrySetResult(completedTask.Result);
                    break;
                default:
                    throw new ArgumentException("Task was not completed");
            }
        }

        #endregion
    }
}
