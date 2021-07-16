using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace LinkingDataflowBlocks
{
    class Program
    {
        static async Task Main()
        {
            //await LinkingExample();
            //await ErrorPropagatingExample();
            //await MoreCorrectErrorPropagatingExample();
            //await MultipleBlocksExceptionHandlingExample();
            //UnlinkingExample();

            // TODO: дополнить реальным примером использования с возвратом данных
            // в книге пока не было такого
        }

        static async Task LinkingExample()
        {
            var multiplyBlock = new TransformBlock<int, int>(item => item * 2);
            var substractBlock = new TransformBlock<int, int>(item => item - 2);

            DataflowLinkOptions options = new() { PropagateCompletion = true };

            // after linking, values that exit mutiplyBlock will enter substractBlock.
            multiplyBlock.LinkTo(substractBlock, options);

            // ... some kind of code

            multiplyBlock.Complete();
            await substractBlock.Completion;
        }

        static async Task ErrorPropagatingExample()
        {
            var block = new TransformBlock<int, int>(item =>
            {
                if (item == 1)
                    throw new InvalidOperationException("Blech.");
                return item * 2;
            });
            block.Post(1); // brings block in a faulted state (all data will be dropped, block ignore all new data)
            block.Post(2); // block will drop these data

            await block.Completion;
        }

        static async Task MoreCorrectErrorPropagatingExample()
        {
            try
            {
                var block = new TransformBlock<int, int>(item =>
                {
                    if (item == 1)
                        throw new InvalidOperationException("Blech.");
                    return item * 2;
                });
                block.Post(1);

                await block.Completion;
            }
            catch (InvalidOperationException)
            {
                // The exception is caught here.
            }
        }

        static async Task MultipleBlocksExceptionHandlingExample()
        {
            try
            {
                var multiplyBlock = new TransformBlock<int, int>(item =>
                {
                    if (item == 1)
                        throw new ArgumentNullException("Blech.");
                    return item * 2;
                });

                var substractBlock = new TransformBlock<int, int>(item => item - 2);

                multiplyBlock.LinkTo(substractBlock, new DataflowLinkOptions { PropagateCompletion = true });

                multiplyBlock.Post(1);

                await substractBlock.Completion;
            }
            catch (AggregateException)
            {
                // The exception is caught here.
                // The AggregateException.Flatten method simplifies error handling in this scenario.
            }
        }

        static void UnlinkingExample()
        {
            var multiplyBlock = new TransformBlock<int, int>(item => item * 2);
            var substractBlock = new TransformBlock<int, int>(item => item - 2);

            IDisposable link = multiplyBlock.LinkTo(substractBlock);

            multiplyBlock.Post(1);
            multiplyBlock.Post(2);

            // Unlink the blocks.
            // The data posted above may or not may have already gone through the link.
            // In real-world code, consider a using block after than calling Dispose.
            link.Dispose();
        }
    }
}
