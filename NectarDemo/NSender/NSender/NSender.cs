﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte
{
	public class NSender
	{
		private const int SEND_TIMEOUT_MILLIS = 2000;

		public void NSend(string ident, string message)
		{
			using (var s = new EventWaitHandle(false, EventResetMode.AutoReset, ident + "S"))
			using (var b = new EventWaitHandle(false, EventResetMode.AutoReset, ident + "B"))
			using (var r = new EventWaitHandle(false, EventResetMode.AutoReset, ident + "R"))
			{
				r.WaitOne(100); // HACK cleanup

				foreach (byte[] bMes in new byte[][]
				{
					new byte[] { 0x00, 0x80 },
					Encoding.UTF8.GetBytes(message.Replace("\0", "")),
					new byte[] { 0x00 }
				})
				{
					for (int i = 0; i / 8 < bMes.Length; i++)
					{
						if ((bMes[i / 8] & (1 << (i % 8))) != 0)
							b.Set();

						s.Set();

						if (!r.WaitOne(SEND_TIMEOUT_MILLIS))
							throw new TimeoutException();
					}
				}
			}
		}
	}
}
