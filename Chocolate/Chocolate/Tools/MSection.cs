using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	public class MSection : IDisposable
	{
		private Mutex Handle;
		private bool Binding;

		public MSection(string ident)
			: this(new Mutex(false, ident), true)
		{ }

		public MSection(Mutex handle, bool binding = false)
		{
			this.Handle = handle;
			this.Handle.WaitOne();
			this.Binding = binding;
		}

		public void Dispose()
		{
			if (this.Handle != null)
			{
				this.Handle.ReleaseMutex();

				if (this.Binding)
					this.Handle.Dispose();

				this.Handle = null;
			}
		}
	}
}
