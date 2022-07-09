using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackgroundQueue
{
    public class BackgroundTaskQueueService : BackgroundService
    {
        public IBackgroundTaskQueue TaskQueue { get; }
        private readonly ILogger<BackgroundTaskQueueService> _logger;

        public BackgroundTaskQueueService(
            ILogger<BackgroundTaskQueueService> logger,
            IBackgroundTaskQueue taskQueue
        )
        {
            _logger = logger;
            TaskQueue = taskQueue;
        }

        public override Task StartAsync(CancellationToken ct)
        {
            _logger.LogInformation(
                $"Background Service {nameof(BackgroundTaskQueueService)} is starting..."
            );

            return base.StartAsync(ct);
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            _logger.LogInformation(
                $"Background Service {nameof(BackgroundTaskQueueService)} is running."
            );

            while (!ct.IsCancellationRequested)
            {
                var ticket = await TaskQueue.DequeueAsync(ct);

                try
                {
                    await ticket.ExecuteAsync(ct);
                }
                catch (Exception ex)
                {
                    ticket.OnException(ex);
                    _logger.LogError(ex, $"Error occurred while executing {nameof(ticket)}.");
                }
            }
        }

        public override Task StopAsync(CancellationToken ct)
        {
            _logger.LogInformation(
                $"Background Service {nameof(BackgroundTaskQueueService)} is stopping..."
            );

            return base.StopAsync(ct);
        }

        public override void Dispose()
        {
            TaskQueue.Dispose();
            base.Dispose();
        }
    }
}
