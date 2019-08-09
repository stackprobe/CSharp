using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace Charlotte
{
	public static class Utils
	{
		private static object PostMessage_SYNCROOT = new object();
		private static AntiRecursive PostMessage_AntiRecursive = new AntiRecursive();

		public static void PostMessage(object message)
		{
			lock (PostMessage_SYNCROOT)
			{
				using (PostMessage_AntiRecursive.Enter())
				{
					string line = "[" + DateTime.Now + "] (PID:" + Process.GetCurrentProcess().Id + ") " + message;

					Console.WriteLine(line);

					using (Mutex m = new Mutex(false, "{46a0307c-0be5-40fc-b509-011bafac5329}"))
					using (new MtxSection(m))
					{
						MSender.MSend(Consts.C2W_IDENT, MSender.Serialize(line));
					}
				}
			}
		}

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
