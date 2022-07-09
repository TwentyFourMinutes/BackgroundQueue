using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundQueue.Generic.Models
{
    /// <summary>
    /// This class is only for internal use.
    /// </summary>
    public abstract class TicketBase
    {
        /// <summary>
        /// Gets called when the <see cref="TicketBase"/> gets enqueued.
        /// </summary>
        public virtual void Enqueued() { }

        /// <summary>
        /// Gets called when the <see cref="ExecuteAsync"/> method errors out.
        /// </summary>
        public virtual void OnException(Exception ex) { }

        internal abstract Task ProccessAsync(CancellationToken ct);
    }
}
