using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Charlotte.Tools
{
	public class NamedEventTools
	{
		public static EventWaitHandle Create(string ident)
		{
			return new EventWaitHandle(false, EventResetMode.AutoReset, ident);
		}

		public static EventWaitHandle CreateGlobal(string ident)
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
			return new EventWaitHandle(false, EventResetMode.AutoReset, @"Global\Global_" + ident, out createdNew, security);
		}
	}
}
