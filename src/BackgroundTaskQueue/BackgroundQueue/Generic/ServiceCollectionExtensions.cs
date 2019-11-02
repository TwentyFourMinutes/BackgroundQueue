using Microsoft.Extensions.DependencyInjection;

namespace BackgroundQueue.Generic
{
	public static partial class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds the required BackgroundResultQueue services.
		/// </summary>
		public static IServiceCollection AddBackgroundResultQueue(this IServiceCollection services)
			=> services.AddSingleton<IBackgroundResultQueue, BackgroundResultQueue>()
					   .AddHostedService<BackgroundResultQueueService>();
	}
}
