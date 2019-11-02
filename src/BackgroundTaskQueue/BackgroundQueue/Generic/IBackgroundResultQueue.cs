using BackgroundQueue.Generic.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundQueue.Generic
{
	public interface IBackgroundResultQueue : IDisposable
	{
		Task ProcessInQueueAsync(Func<CancellationToken, Task> task);
		Task ProcessInQueueAsync(Func<CancellationToken, Task> task, Action<Exception> exception);
		Task ProcessInQueueAsync(Ticket ticket);

		Task<T> ProcessInQueueAsync<T>(Func<CancellationToken, Task<T>> task);
		Task<T> ProcessInQueueAsync<T>(Func<CancellationToken, Task<T>> task, Action<Exception> exception);

		Task<T> ProcessInQueueAsync<T>(Ticket<T> ticket);

		Task<TicketBase> DequeueAsync(CancellationToken ct);
	}
}
