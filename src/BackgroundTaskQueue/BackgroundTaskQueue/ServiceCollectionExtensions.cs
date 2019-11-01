using Microsoft.Extensions.DependencyInjection;

namespace BackgroundTaskQueue.Core
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddBackgroundTaskQueue(this IServiceCollection services)
			=> services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>()
					   .AddHostedService<BackgroundQueueService>();
	}
}
