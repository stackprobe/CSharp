using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Charlotte.Tools
{
	public class NamedEventUnit : IDisposable
	{
		public static NamedEventUnit Create(string ident)
		{
			return new NamedEventUnit(new EventWaitHandle(false, EventResetMode.AutoReset, ident), true);
		}

		public static NamedEventUnit CreateGlobal(string ident)
		{
			EventWaitHandleSecurity security = new EventWaitHandleSecurity();

			security.AddAccessRule(
				new EventWaitHandleAccessRule(
					new SecurityIdentifier(
						WellKnownSidType.WorldSid,
						null
						),
					EventWaitHandleRights.FullControl,
					AccessControlType.Allow
					)
				);

			bool createdNew;
			return new NamedEventUnit(new EventWaitHandle(false, EventResetMode.AutoReset, @"Global\Global_" + ident, out createdNew, security), true);
		}

		private EventWaitHandle Handle;
		private bool Binding;

		public NamedEventUnit(EventWaitHandle handle, bool binding = false)
		{
			this.Handle = handle;
			this.Binding = binding;
		}

		public void Set()
		{
			this.Handle.Set();
		}

		public bool WaitForMillis(int millis)
		{
			return this.Handle.WaitOne(millis);
		}

		public void WaitForever()
		{
			this.Handle.WaitOne();
		}

		public void Dispose()
		{
			if (this.Handle != null)
			{
				if (this.Binding)
					this.Handle.Dispose();

				this.Handle = null;
			}
		}
	}
}
