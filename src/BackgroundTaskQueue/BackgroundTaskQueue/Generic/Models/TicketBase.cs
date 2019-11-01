using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTaskQueue.Core.Generic.Models
{
	public abstract class TicketBase
	{
		public virtual void Enqueued() { }
		public virtual void OnException(Exception ex) { }
		internal abstract Task ProccessAsync(CancellationToken ct);
	}
}
