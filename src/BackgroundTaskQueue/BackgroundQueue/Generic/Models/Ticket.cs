using System.Threading;
using System.Threading.Tasks;

namespace BackgroundQueue.Generic.Models
{
	public abstract class Ticket : TicketBase
	{
		private readonly TaskCompletionSource<object?> _completionSource;
		internal Task<object?> SourceTask { get => _completionSource.Task; }

		public Ticket()
		{
			_completionSource = new TaskCompletionSource<object?>();
		}

		public abstract Task ExecuteAsync(CancellationToken ct);

		internal override async Task ProccessAsync(CancellationToken ct)
		{
			await ExecuteAsync(ct);

			_completionSource.TrySetResult(default);
		}
	}

	public abstract class Ticket<T> : TicketBase
	{
		private readonly TaskCompletionSource<T> _completionSource;
		internal Task<T> SourceTask { get => _completionSource.Task; }

		public Ticket()
		{
			_completionSource = new TaskCompletionSource<T>();
		}

		public abstract Task<T> ExecuteAsync(CancellationToken ct);

		internal override async Task ProccessAsync(CancellationToken ct)
		{
			var result = await ExecuteAsync(ct);

			_completionSource.TrySetResult(result);
		}
	}
}
