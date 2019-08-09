using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Test01.Modules
{
	public static class BlockSection
	{
		private const string IDENT = "{765e7fbb-433e-4ccf-9cb1-281f6d8dff20}"; // shared_uuid@ign

		public static object SYNCROOT = new object();
		private static Mutex Mtx;

		public static void Invoke(Action task)
		{
			lock (SYNCROOT)
			{
				if (Mtx == null)
				{
					MutexSecurity security = new MutexSecurity();

					security.AddAccessRule(
						new MutexAccessRule(
							new SecurityIdentifier(
								WellKnownSidType.WorldSid,
								null
								),
							MutexRights.FullControl,
							AccessControlType.Allow
							)
						);

					bool createdNew;
					Mtx = new Mutex(false, @"Global\Global_" + IDENT, out createdNew, security);
				}
				Mtx.WaitOne();
				try
				{
					task();
				}
				finally
				{
					Mtx.ReleaseMutex();
				}
			}
		}
	}
}
