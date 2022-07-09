using Microsoft.Extensions.DependencyInjection;

namespace BackgroundQueue
{
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the required BackgroundTaskQueue services.
        /// </summary>
        public static IServiceCollection AddBackgroundTaskQueue(this IServiceCollection services) =>
            services
                .AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>()
                .AddHostedService<BackgroundTaskQueueService>();
    }
}
