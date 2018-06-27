using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Test01.Modules
{
	public class MultiThreadTaskPool : IDisposable
	{
		private int ThreadCountMax;

		public MultiThreadTaskPool(int thCntMax = 3)
		{
			this.ThreadCountMax = thCntMax;
		}

		private List<Thread> Ths = new List<Thread>();
		private object SYNCROOT = new object();
		private Queue<Action> Tasks = new Queue<Action>();
		private Exception Ex = null;
		private int ThreadCount = 0;

		// 各メソッドthread safeでない事に注意！

		public void Add(Action task)
		{
			bool thAdd = false;

			lock (SYNCROOT)
			{
				this.Tasks.Enqueue(task);

				if (this.ThreadCount < this.ThreadCountMax)
				{
					this.ThreadCount++;
					thAdd = true;
				}
			}

			if (thAdd)
			{
				this.Ths = new List<Thread>(this.Ths.Where(t => t.Join(0) == false));

				Thread th = new Thread(() =>
				{
					for (; ; )
					{
						Action t;

						lock (SYNCROOT)
						{
							if (this.Tasks.Count <= 0)
							{
								this.ThreadCount--;
								break;
							}
							t = this.Tasks.Dequeue();
						}
						try
						{
							t();
						}
						catch (Exception e)
						{
							lock (SYNCROOT)
							{
								if (this.Ex == null) // 最初の例外を優先する。
									this.Ex = e;
							}
						}
					}
				});

				th.Start();

				this.Ths.Add(th);
			}
		}

		public void RelayThrow()
		{
			lock (SYNCROOT)
			{
				if (this.Ex != null)
					throw new Exception("Relay", this.Ex);
			}
		}

		public void WaitToEnd()
		{
			foreach (Thread th in this.Ths)
				th.Join();

			this.Ths.Clear();

			if (this.Tasks.Count != 0) // 2bs
				throw null; // never

			if (this.ThreadCount != 0) // 2bs
				throw null; // never
		}

		public void Dispose()
		{
			this.WaitToEnd();
		}
	}
}
