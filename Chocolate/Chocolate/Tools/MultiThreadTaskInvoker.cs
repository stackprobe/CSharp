using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	public class MultiThreadTaskInvoker : IDisposable
	{
		public int ThreadCountMax = Environment.ProcessorCount;
		public int ExceptionCountMax = 10;

		// <---- prop

		private object SYNCROOT = new object();
		private List<Thread> Ths = new List<Thread>();
		private Queue<Action> Tasks = new Queue<Action>();
		private List<Exception> Exs = new List<Exception>();

		public void AddTask(Action task)
		{
			lock (SYNCROOT)
			{
				this.Tasks.Enqueue(task);

				if (this.Ths.Count < this.ThreadCountMax)
				{
					Thread th = new Thread(() =>
					{
#if true
						for (; ; )
						{
							Action nextTask;

							lock (SYNCROOT)
							{
								if (this.Tasks.Count <= 0)
								{
									this.Ths.Remove(Thread.CurrentThread);
									return;
								}
								nextTask = this.Tasks.Dequeue();
							}

							try
							{
								nextTask();
							}
							catch (Exception e)
							{
								lock (SYNCROOT)
								{
									if (this.Exs.Count < this.ExceptionCountMax)
										this.Exs.Add(e);
								}
							}
						}
#else
						for (; ; )
						{
							try
							{
								for (; ; )
								{
									Action nextTask;

									lock (SYNCROOT)
									{
										if (this.Tasks.Count <= 0)
										{
											this.Ths.Remove(Thread.CurrentThread);
											return;
										}
										nextTask = this.Tasks.Dequeue();
									}
									nextTask();
								}
							}
							catch (Exception e)
							{
								lock (SYNCROOT)
								{
									if (this.Exs.Count < this.ExceptionCountMax)
										this.Exs.Add(e);
								}
							}
						}
#endif
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
			for (; ; )
			{
				Thread th;

				lock (SYNCROOT)
				{
					if (this.Ths.Count <= 0)
						break;

					th = this.Ths[0];
				}
				th.Join();
			}
		}

		public void RelayThrow()
		{
			this.WaitToEnd();

			if (1 <= this.Exs.Count)
				throw new Exception("Relay: " + string.Join("\r\n", this.Exs.Select(e => e + "　<---- 内部の例外ここまで")));
		}

		public void Dispose()
		{
			this.WaitToEnd();
		}
	}
}
