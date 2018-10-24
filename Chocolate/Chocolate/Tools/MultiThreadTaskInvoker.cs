using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	public class MultiThreadTaskInvoker : IDisposable
	{
		public Action<Exception> ExHandler = e => { };
		public int ThreadCountMax = Environment.ProcessorCount;

		// <---- prop

		private object SYNCROOT = new object();
		private List<Thread> Ths = new List<Thread>();
		private Queue<Action> Tasks = new Queue<Action>();
		private NamedEventPair ThreadEndEv = new NamedEventPair(Guid.NewGuid().ToString("B"));

		public void AddTask(Action task)
		{
			lock (SYNCROOT)
			{
				this.Tasks.Enqueue(task);

				if (this.Ths.Count < this.ThreadCountMax)
				{
					Thread th = new Thread(() =>
					{
						for (; ; )
						{
							Action nextTask;

							lock (SYNCROOT)
							{
								if (this.Tasks.Count <= 0)
								{
									this.Ths.Remove(Thread.CurrentThread);
									this.ThreadEndEv.Set();
									break;
								}
								nextTask = this.Tasks.Dequeue();
							}

							try
							{
								nextTask();
							}
							catch (Exception e)
							{
								try
								{
									this.ExHandler(e);
								}
								catch
								{ }
							}
						}
					});

					th.Start();

					this.Ths.Add(th);
				}
			}
		}

		public bool IsEnded()
		{
			lock (SYNCROOT)
			{
				return this.Ths.Count <= 0;
			}
		}

		public void WaitToEnd()
		{
			while (this.IsEnded() == false)
			{
				this.ThreadEndEv.WaitForMillis(2000);
			}
		}

		public void Dispose()
		{
			this.WaitToEnd();
		}
	}
}
