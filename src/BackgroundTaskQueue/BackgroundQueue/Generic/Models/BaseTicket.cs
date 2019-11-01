using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundQueue.Generic.Models
{
	public class BaseTicket<T> : Ticket<T>
	{
		private readonly Func<CancellationToken, Task<T>> _task;
		private readonly Action<Exception> _exception;


		internal BaseTicket(Func<CancellationToken, Task<T>> task, Action<Exception> exception)
		{
			_task = task;
			_exception = exception;
		}

		public override Task<T> ExecuteAsync(CancellationToken ct)
			=> _task(ct);

		public override void OnException(Exception ex)
			=> _exception(ex);
	}
}
