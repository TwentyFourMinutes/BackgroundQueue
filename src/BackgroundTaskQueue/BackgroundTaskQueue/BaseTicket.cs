using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTaskQueue.Core
{
	internal class BaseTicket : Ticket
	{
		private readonly Func<CancellationToken, Task> _task;

		internal BaseTicket(Func<CancellationToken, Task> task)
		{
			_task = task;
		}

		public override Task ExecuteAsync(CancellationToken ct)
			=> _task(ct);
	}
}
