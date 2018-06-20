using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class LogQueue
	{
		public static LogQueue I = new LogQueue();

		public class Entry
		{
			public DateTime Stamp;
			public object Message;

			public override string ToString()
			{
				return "[" + this.Stamp + "] " + this.Message;
			}
		}

		private readonly object SYNCROOT = new object();
		private List<Entry> Entries = new List<Entry>();

		public void Enqueue(object message)
		{
			lock (SYNCROOT)
			{
				this.Entries.Add(new Entry()
				{
					Stamp = DateTime.Now,
					Message = message,
				});
			}
		}

		public Entry[] DequeueAll()
		{
			Entry[] ret;

			lock (SYNCROOT)
			{
				ret = this.Entries.ToArray();
				this.Entries.Clear();
			}
			return ret;
		}
	}
}
