using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundQueue.Models
{
	internal class BaseTicket : Ticket
	{
		private readonly Func<CancellationToken, Task> _task;
		private readonly Action<Exception> _exception;

		internal BaseTicket(Func<CancellationToken, Task> task, Action<Exception> exception)
		{
			_task = task;
			_exception = exception;
		}

		public override Task ExecuteAsync(CancellationToken ct)
			=> _task(ct);

		public override void OnException(Exception ex)
			=> _exception(ex);
	}
}
