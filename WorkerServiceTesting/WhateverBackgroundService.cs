using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerServiceTesting
{
    public sealed class WhateverBackgroundService : IDisposable
    {
        #region Fields

        private readonly ILogger<WhateverBackgroundService> _logger;
        private readonly CancellationTokenSource _cts;

        #endregion

        #region Constructor

        public WhateverBackgroundService(ILogger<WhateverBackgroundService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cts = new CancellationTokenSource();
        }

        #endregion

        #region Methods

        public void Start()
        {
            Task.Factory.StartNew(() => Loop(_cts.Token), _cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void Stop()
        {
            _cts.Cancel();
        }

        public void Dispose()
        {
            _cts.Dispose();
        }

        #endregion

        #region Helpers

        private async Task Loop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Current time is {0}", DateTime.Now);

                await Task.Delay(1000, cancellationToken);
            }
        }

        #endregion
    }
}
