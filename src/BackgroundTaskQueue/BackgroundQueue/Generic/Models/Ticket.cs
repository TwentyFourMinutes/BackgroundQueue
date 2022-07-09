using System.Threading;
using System.Threading.Tasks;

namespace BackgroundQueue.Generic.Models
{
    /// <summary>
    /// Inherit from this class, if you want to create a new Ticket, which should get enqueued in a BackgroundResultQueue.
    /// </summary>
    public abstract class Ticket : TicketBase
    {
        private readonly TaskCompletionSource<object?> _completionSource;
        internal Task<object?> SourceTask
        {
            get => _completionSource.Task;
        }

        public Ticket()
        {
            _completionSource = new TaskCompletionSource<object?>();
        }

        /// <summary>
        /// Contains the core logic of the Ticket.
        /// </summary>
        public abstract Task ExecuteAsync(CancellationToken ct);

        internal override async Task ProccessAsync(CancellationToken ct)
        {
            try
            {
                await ExecuteAsync(ct);
            }
            catch
            {
                throw;
            }
            finally
            {
                _completionSource.TrySetResult(default);
            }
        }
    }

    /// <summary>
    /// Inherit from this class, if you want to create a new Ticket, which should get enqueued in a BackgroundResultQueue.
    /// </summary>
    public abstract class Ticket<T> : TicketBase
    {
        private readonly TaskCompletionSource<T> _completionSource;
        internal Task<T> SourceTask
        {
            get => _completionSource.Task;
        }

        public Ticket()
        {
            _completionSource = new TaskCompletionSource<T>();
        }

        public abstract Task<T> ExecuteAsync(CancellationToken ct);

        internal override async Task ProccessAsync(CancellationToken ct)
        {
            T result = default!;

            try
            {
                result = await ExecuteAsync(ct);
            }
            catch
            {
                throw;
            }
            finally
            {
                _completionSource.TrySetResult(result);
            }
        }
    }
}
