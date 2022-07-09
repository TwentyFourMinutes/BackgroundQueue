using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundQueue.Models
{
    /// <summary>
    /// Inherit from this class, if you want to create a new Ticket, which should get enqueued in a BackgroundTaskQueue.
    /// </summary>
    public abstract class Ticket
    {
        /// <summary>
        /// Gets called when the <see cref="BaseTicket"/> gets enqueued.
        /// </summary>
        public virtual void Enqueued() { }

        /// <summary>
        /// Gets called when the <see cref="ExecuteAsync"/> method errors out.
        /// </summary>
        public virtual void OnException(Exception ex) { }

        /// <summary>
        /// Contains the core logic of the Ticket.
        /// </summary>
        public abstract Task ExecuteAsync(CancellationToken ct);
    }
}
