using BackgroundQueue.Generic.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundQueue.Generic
{
	public interface IBackgroundResultQueue : IDisposable
	{
		/// <summary>
		/// Adds a new <see cref="Task"/> to the Queue, which will get processed in a background thread. This method will return when the task got processed.
		/// </summary>
		/// <param name="task">The Task which will get enqueued.</param>
		Task ProcessInQueueAsync(Func<CancellationToken, Task> task);

		/// <summary>
		/// Adds a new <see cref="Task"/> to the Queue, which will get processed in a background thread. This method will return when the task got processed.
		/// </summary>
		/// <param name="task">The Task which will get enqueued.</param>
		/// <param name="exception">A action which will get called, if the task fails.</param>
		Task ProcessInQueueAsync(Func<CancellationToken, Task> task, Action<Exception> exception);
		/// <summary>
		/// Adds a new <see cref="Ticket"/> to the Queue, which will get processed in a background thread. This method will return when the ticket got processed.
		/// </summary>
		/// <param name="ticket">The Ticket which will get enqueued.</param>
		Task ProcessInQueueAsync(Ticket ticket);

		/// <summary>
		/// Adds a new <see cref="Task"/> to the Queue, which will get processed in a background thread. This method will return when the task got processed.
		/// </summary>
		/// <param name="task">The Task which will get enqueued.</param>
		Task<T> ProcessInQueueAsync<T>(Func<CancellationToken, Task<T>> task);

		/// <summary>
		/// Adds a new <see cref="Task"/> to the Queue, which will get processed in a background thread. This method will return when the task got processed.
		/// </summary>
		/// <param name="task">The Task which will get enqueued.</param>
		/// <param name="exception">A action which will get called, if the task fails.</param>
		Task<T> ProcessInQueueAsync<T>(Func<CancellationToken, Task<T>> task, Action<Exception> exception);

		/// <summary>
		/// Adds a new <see cref="Ticket"/> to the Queue, which will get processed in a background thread. This method will return when the ticket got processed.
		/// </summary>
		/// <param name="ticket">The Ticket which will get enqueued.</param>
		Task<T> ProcessInQueueAsync<T>(Ticket<T> ticket);

		/// <summary>
		/// Dequeues a <see cref="TicketBase"/> from the <see cref="IBackgroundTaskQueue"/>.
		/// </summary>
		/// <returns>Returns the enqueued <see cref="TicketBase"/>.</returns>
		Task<TicketBase> DequeueAsync(CancellationToken ct);
	}
}
