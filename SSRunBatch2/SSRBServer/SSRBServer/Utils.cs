using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Utils
	{
		public class AntiRecursive
		{
			private object SYNCROOT = new object();
			private bool Entered = false;

			public Leaveable Enter()
			{
				lock (this.SYNCROOT)
				{
					if (this.Entered)
						throw new InvalidOperationException();

					this.Entered = true;

					return new Leaveable(() =>
					{
						lock (this.SYNCROOT)
						{
							this.Entered = false;
						}
					});
				}
			}
		}

		public class Leaveable : IDisposable
		{
			private Action Leave;

			public Leaveable(Action leave)
			{
				this.Leave = leave;
			}

			public void Dispose()
			{
				this.Leave();
			}
		}
	}
}
