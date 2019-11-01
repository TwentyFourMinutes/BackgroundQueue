using BackgroundQueue.Generic.Models;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundQueue.Generic
{
	public class BackgroundResultQueue : IBackgroundResultQueue
	{
		public bool IsDisposed { get; private set; }
		private readonly ConcurrentQueue<TicketBase> _taskQueue;
		private readonly SemaphoreSlim _signal;

		public BackgroundResultQueue()
		{
			_taskQueue = new ConcurrentQueue<TicketBase>();
			_signal = new SemaphoreSlim(0);
		}

		public Task<T> ProcessInQueue<T>(Func<CancellationToken, Task<T>> task)
			=> ProcessInQueue(task, ct => { });

		public Task<T> ProcessInQueue<T>(Func<CancellationToken, Task<T>> task, Action<Exception> exception)
			=> ProcessInQueue(new BaseTicket<T>(task, exception));

		public Task<T> ProcessInQueue<T>(Ticket<T> ticket)
		{
			_taskQueue.Enqueue(ticket);
			_signal.Release();

			return ticket.SourceTask;
		}

		public async Task<TicketBase> DequeueAsync(CancellationToken ct)
		{
			await _signal.WaitAsync(ct);
			_taskQueue.TryDequeue(out var ticket);

			return ticket;
		}

		public void Dispose()
		{
			if (!IsDisposed)
			{
				_signal.Dispose();
				IsDisposed = true;
			}
		}
	}
}
