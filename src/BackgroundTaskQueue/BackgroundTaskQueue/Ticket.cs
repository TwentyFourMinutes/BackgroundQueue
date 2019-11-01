using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTaskQueue.Core
{
	public abstract class Ticket
	{
		public virtual void Enqueued() { }

		public abstract Task ExecuteAsync(CancellationToken ct);
	}
}
