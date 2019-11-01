using Microsoft.Extensions.DependencyInjection;

namespace BackgroundQueue
{
	public static partial class ServiceCollectionExtensions
	{
		public static IServiceCollection AddBackgroundTaskQueue(this IServiceCollection services)
			=> services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>()
					   .AddHostedService<BackgroundTaskQueueService>();
	}
}
