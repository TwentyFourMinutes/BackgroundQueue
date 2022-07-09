using System;
using System.Threading;
using System.Threading.Tasks;
using BackgroundQueue.Models;

namespace BackgroundQueue
{
    public interface IBackgroundTaskQueue : IDisposable
    {
        /// <summary>
        /// Adds a new <see cref="Task"/> to the Queue, which will get processed in a background thread. This method will return immediately.
        /// </summary>
        /// <param name="task">The Task which will get enqueued.</param>
        void Enqueue(Func<CancellationToken, Task> task);

        /// <summary>
        /// Adds a new <see cref="Task"/> to the Queue, which will get processed in a background thread. This method will return immediately.
        /// </summary>
        /// <param name="task">The Task which will get enqueued.</param>
        /// <param name="exception">A action which will get called, if the task fails.</param>
        void Enqueue(Func<CancellationToken, Task> task, Action<Exception> exception);

        /// <summary>
        /// Adds a new <see cref="Ticket"/> to the Queue, which will get processed in a background thread. This method will return immediately.
        /// </summary>
        /// <param name="ticket">The ticket which will get enqueued.</param>
        void Enqueue(Ticket ticket);

        /// <summary>
        /// Dequeues a <see cref="Ticket"/> from the <see cref="IBackgroundTaskQueue"/>.
        /// </summary>
        /// <returns>Returns the enqueued <see cref="Ticket"/>.</returns>
        Task<Ticket> DequeueAsync(CancellationToken ct);
    }
}
