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
			private bool Entered = false;

			public Leaveable Enter()
			{
				if (this.Entered)
					throw new InvalidOperationException();

				this.Entered = true;
				return new Leaveable(() => this.Entered = false);
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
