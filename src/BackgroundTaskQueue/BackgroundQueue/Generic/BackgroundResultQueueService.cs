using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundQueue.Generic
{
	public class BackgroundResultQueueService : BackgroundService
	{
		public IBackgroundResultQueue ResultQueue { get; }
		private readonly ILogger<BackgroundResultQueueService> _logger;

		public BackgroundResultQueueService(ILogger<BackgroundResultQueueService> logger, IBackgroundResultQueue taskQueue)
		{
			_logger = logger;
			ResultQueue = taskQueue;
		}

		public override Task StartAsync(CancellationToken ct)
		{
			_logger.LogInformation($"Background Service {nameof(BackgroundResultQueueService)} is starting...");

			return base.StartAsync(ct);
		}

		protected override async Task ExecuteAsync(CancellationToken ct)
		{
			_logger.LogInformation($"Background Service {nameof(BackgroundResultQueueService)} is running.");

			while (!ct.IsCancellationRequested)
			{
				var ticket = await ResultQueue.DequeueAsync(ct);

				try
				{
					await ticket.ProccessAsync(ct);
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
			_logger.LogInformation($"Background Service {nameof(BackgroundResultQueueService)} is stopping...");

			return base.StopAsync(ct);
		}

		public override void Dispose()
		{
			ResultQueue.Dispose();
			base.Dispose();
		}
	}
}
