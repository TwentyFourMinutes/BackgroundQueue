using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundQueue.Generic.Models
{
	public abstract class TicketBase
	{
		public virtual void Enqueued() { }
		public virtual void OnException(Exception ex) { }
		internal abstract Task ProccessAsync(CancellationToken ct);
	}
}
