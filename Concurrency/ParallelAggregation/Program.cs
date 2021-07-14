using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelAggregation
{
    class Program
    {
        static void Main()
        {
            var intValues = Enumerable.Range(1, 1000);

            Console.WriteLine("ParallelSum result: {0}", ParallelSum(intValues));
            Console.WriteLine("ParallelSumWithLinqSum result: {0}", ParallelSumWithLinqSum(intValues));
            Console.WriteLine("ParallelSumWithLinqAggregate result: {0}", ParallelSumWithLinqAggregate(intValues));
        }

        static int ParallelSum(IEnumerable<int> values)
        {
            object mutex = new object();

            int result = 0;

            Parallel.ForEach(source: values,
                localInit: () => 0,
                body: (item, state, localValue) => localValue + item,
                localFinally: localValue =>
                {
                    lock (mutex)
                    {
                        result += localValue;
                    }
                });

            return result;
        }

        static int ParallelSumWithLinqSum(IEnumerable<int> values)
        {
            return values.AsParallel().Sum();
        }

        static int ParallelSumWithLinqAggregate(IEnumerable<int> values)
        {
            return values.AsParallel().Aggregate(
                seed: 0,
                func: (sum, item) => sum + item);
        }
    }
}
