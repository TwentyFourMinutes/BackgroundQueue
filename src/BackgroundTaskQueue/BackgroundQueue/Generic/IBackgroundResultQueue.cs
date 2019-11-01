using BackgroundQueue.Generic.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundQueue.Generic
{
	public interface IBackgroundResultQueue : IDisposable
	{
		Task<T> ProcessInQueue<T>(Func<CancellationToken, Task<T>> task);

		Task<T> ProcessInQueue<T>(Ticket<T> ticket);

		Task<TicketBase> DequeueAsync(CancellationToken ct);
	}
}
