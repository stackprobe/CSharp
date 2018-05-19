﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Charlotte.Tools
{
	public class MutexTools
	{
		public static Mutex Create(string name)
		{
			return new Mutex(false, name);
		}

		public static Mutex CreateGlobal(string ident)
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
			return new Mutex(false, @"Global\Global_" + ident, out createdNew, security);
		}
	}
}
