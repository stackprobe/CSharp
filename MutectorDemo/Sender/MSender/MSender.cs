﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte
{
	public class MSender
	{
		public void MSend(string ident, string message)
		{
			Mutex[] hdls = new Mutex[6];
			try
			{
				for (int i = 0; i < hdls.Length; i++)
					hdls[i] = new Mutex(false, ident + i);

				hdls[3].WaitOne();

				bool[] flgs = new bool[3];
				int c = 0;

				foreach (byte[] bMes in new byte[][]
				{
					new byte[] { 0xff },
					Encoding.UTF8.GetBytes(message.Replace("\0", "")),
					new byte[] { 0x00 }
				})
				{
					for (int i = 0; i / 8 < bMes.Length; i++)
					{
						int n = (c + 1) % 3;

						hdls[3 + n].WaitOne();
						hdls[3 + c].ReleaseMutex();

						if ((bMes[i / 8] & (1 << (i % 8))) != 0)
						{
							if (!flgs[n])
							{
								hdls[n].WaitOne();
								flgs[n] = true;
							}
						}
						else if (flgs[n])
						{
							hdls[n].ReleaseMutex();
							flgs[n] = false;
						}
						c = n;
					}
				}
			}
			finally
			{
				foreach (Mutex hdl in hdls)
				{
					try { hdl.ReleaseMutex(); }
					catch { }
					try { hdl.Close(); }
					catch { }
				}
			}
		}
	}
}
