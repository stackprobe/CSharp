using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Charlotte
{
	public class MRecver
	{
		// ---- ここから

		public bool MRecvEnd;
		public void MRecv(string ident, Action<string> recved)
		{
			Mutex[] hdls = new Mutex[6];
			try
			{
				for (int i = 0; i < hdls.Length; i++)
					hdls[i] = new Mutex(false, ident + i);

				hdls[3].WaitOne();

				MemoryStream mem = new MemoryStream();
				byte chr = 0;
				int waitCount = 1;

				for (int i = 0, c = 0; !this.MRecvEnd; )
				{
					int n = (c + 1) % 3;

					hdls[3 + n].WaitOne();
					hdls[3 + c].ReleaseMutex();

					bool nBit = hdls[n].WaitOne(0);

					if (nBit)
						hdls[n].ReleaseMutex();

					if (waitCount <= 0)
					{
						if (!nBit)
							chr |= (byte)(1 << i);

						if (8 <= ++i)
						{
							if (chr == 0)
							{
								recved(Encoding.UTF8.GetString(
									mem.GetBuffer()
									));
								mem = new MemoryStream();
								waitCount = 1;
							}
							else
								mem.WriteByte(chr);

							i = chr = 0;
						}
					}
					else
					{
						if (nBit)
						{
							i = 0;
							Thread.Sleep(waitCount);
							if (waitCount < 100) waitCount++;
						}
						else if (8 <= ++i)
							i = waitCount = 0;
					}
					c = n;
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

		// ---- ここまで
	}
}
