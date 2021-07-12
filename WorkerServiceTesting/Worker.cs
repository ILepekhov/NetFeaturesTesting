using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerServiceTesting
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly WhateverBackgroundService _whateverBackgroundService;

        public Worker(ILogger<Worker> logger, WhateverBackgroundService whateverBackgroundService)
        {
            _logger = logger;
            _whateverBackgroundService = whateverBackgroundService;

            _logger.LogInformation("Worker -> ctor()");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker -> ExecuteAsync()");

            await Task.CompletedTask;

            // code here must have maximum short execution time or must check stoppingToken cancel state
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker -> StartAsync()");

            _whateverBackgroundService.Start();

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker -> StopAsync()");

            _whateverBackgroundService.Stop();

            return base.StopAsync(cancellationToken);
        }
    }
}
