using Microsoft.Extensions.DependencyInjection;

namespace BackgroundTaskQueue.Core.Generic
{
	public static partial class ServiceCollectionExtensions
	{
		public static IServiceCollection AddBackgroundResultQueue(this IServiceCollection services)
			=> services.AddSingleton<IBackgroundResultQueue, BackgroundResultQueue>()
					   .AddHostedService<BackgroundResultQueueService>();
	}
}
