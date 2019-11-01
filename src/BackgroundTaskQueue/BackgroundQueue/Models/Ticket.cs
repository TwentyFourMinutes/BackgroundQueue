using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundQueue.Models
{
	public abstract class Ticket
	{
		public virtual void Enqueued() { }
		public virtual void OnException(Exception ex) { }

		public abstract Task ExecuteAsync(CancellationToken ct);
	}
}
