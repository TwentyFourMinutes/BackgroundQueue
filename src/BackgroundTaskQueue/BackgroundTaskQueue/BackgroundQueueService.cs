using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTaskQueue.Core
{
	public class BackgroundQueueService : BackgroundService
	{
		public BackgroundTaskQueue TaskQueue { get; }
		private readonly ILogger<BackgroundQueueService> _logger;

		public BackgroundQueueService(ILogger<BackgroundQueueService> logger, BackgroundTaskQueue taskQueue)
		{
			_logger = logger;
			TaskQueue = taskQueue;
		}

		public override Task StartAsync(CancellationToken ct)
		{
			_logger.LogInformation($"Background Service {nameof(BackgroundQueueService)} is starting...");

			return base.StartAsync(ct);	
		}

		protected override async Task ExecuteAsync(CancellationToken ct)
		{
			_logger.LogInformation($"Background Service {nameof(BackgroundQueueService)} is running.");

			while (!ct.IsCancellationRequested)
			{
				var ticket = await TaskQueue.DequeueAsync(ct);

				try
				{
					await ticket.ExecuteAsync(ct);
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, $"Error occurred while executing {nameof(ticket)}.");
				}
			}
		}

		public override Task StopAsync(CancellationToken ct)
		{
			_logger.LogInformation($"Background Service {nameof(BackgroundQueueService)} is stopping...");

			return base.StopAsync(ct);	
		}

		public override void Dispose()
		{
			TaskQueue.Dispose();
			base.Dispose();
		}
	}
}
