using BackgroundTaskQueue.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTaskQueue.Core
{
	public interface IBackgroundTaskQueue : IDisposable
	{
		void Enqueue(Func<CancellationToken, Task> task);

		void Enqueue(Ticket ticket);

		Task<Ticket> DequeueAsync(CancellationToken ct);
	}
}
