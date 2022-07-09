using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using BackgroundQueue.Generic.Models;

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

        /// <inheritdoc/>
        public Task ProcessInQueueAsync(Func<CancellationToken, Task> task) =>
            ProcessInQueueAsync(task, ct => { });

        /// <inheritdoc/>
        public Task ProcessInQueueAsync(
            Func<CancellationToken, Task> task,
            Action<Exception> exception
        ) => ProcessInQueueAsync(new BaseTicket(task, exception));

        /// <inheritdoc/>
        public Task ProcessInQueueAsync(Ticket ticket)
        {
            _taskQueue.Enqueue(ticket);
            _signal.Release();
            ticket.Enqueued();

            return ticket.SourceTask;
        }

        /// <inheritdoc/>
        public Task<T> ProcessInQueueAsync<T>(Func<CancellationToken, Task<T>> task) =>
            ProcessInQueueAsync(task, ct => { });

        /// <inheritdoc/>
        public Task<T> ProcessInQueueAsync<T>(
            Func<CancellationToken, Task<T>> task,
            Action<Exception> exception
        ) => ProcessInQueueAsync(new BaseTicket<T>(task, exception));

        /// <inheritdoc/>
        public Task<T> ProcessInQueueAsync<T>(Ticket<T> ticket)
        {
            _taskQueue.Enqueue(ticket);
            _signal.Release();
            ticket.Enqueued();

            return ticket.SourceTask;
        }

        /// <inheritdoc/>
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
