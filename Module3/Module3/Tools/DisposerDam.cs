using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class DisposerDam : IDisposable
	{
		private Stack<Action> Ds = new Stack<Action>();

		public void Using(IDisposable d)
		{
			this.Add(() => d.Dispose());
		}

		public void Add(Action d)
		{
			this.Ds.Push(d);
		}

		public void Dispose()
		{
			ExceptionDam eDam = new ExceptionDam();

			while (1 <= this.Ds.Count)
			{
				eDam.Invoke(() =>
				{
					this.Ds.Pop()();
				});
			}
			eDam.Discharge();
		}
	}
}
