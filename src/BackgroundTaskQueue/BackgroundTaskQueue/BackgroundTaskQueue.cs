using BackgroundTaskQueue.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTaskQueue.Core
{
	public class BackgroundTaskQueue : IBackgroundTaskQueue, IDisposable
	{
		public bool IsDisposed { get; private set; }
		private readonly ConcurrentQueue<Ticket> _taskQueue;
		private readonly SemaphoreSlim _signal;

		public BackgroundTaskQueue()
		{
			_taskQueue = new ConcurrentQueue<Ticket>();
			_signal = new SemaphoreSlim(0);
		}

		public void Enqueue(Func<CancellationToken, Task> task)
			=> Enqueue(task, exception => { });

		public void Enqueue(Func<CancellationToken, Task> task, Action<Exception> exception)
		{
			_taskQueue.Enqueue(new BaseTicket(task, exception));
			_signal.Release();
		}

		public void Enqueue(Ticket ticket)
		{
			ticket.Enqueued();
			_taskQueue.Enqueue(ticket);
			_signal.Release();
		}

		public async Task<Ticket> DequeueAsync(CancellationToken ct)
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
