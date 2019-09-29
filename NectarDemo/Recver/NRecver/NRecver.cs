using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Charlotte
{
	public class NRecver
	{
		// ---- ここから

		public void NRecv(string ident, Action<string> recved)
		{
			using (var s = new EventWaitHandle(
				false, EventResetMode.AutoReset, ident + "S"))
			using (var k = new EventWaitHandle(
				false, EventResetMode.AutoReset, ident + "K"))
			using (var b = new EventWaitHandle(
				false, EventResetMode.AutoReset, ident + "B"))
			using (var r = new EventWaitHandle(
				false, EventResetMode.AutoReset, ident + "R"))
			{
				MemoryStream mem = new MemoryStream();
				byte chr = 0;
				bool recving = false;

				// cleanup
				k.WaitOne(0); // 2bs
				b.WaitOne(0);

				for (int i = 0; ; )
				{
					s.WaitOne();
					if (k.WaitOne(0)) break;
					bool bit = b.WaitOne(0);
					r.Set();

					if (recving)
					{
						if (bit)
							chr |= (byte)(1 << i);

						if (8 <= ++i)
						{
							if (chr == 0)
							{
								recved(Encoding.UTF8.GetString(
									mem.GetBuffer()
									));
								mem = new MemoryStream();
								recving = false;
							}
							else
								mem.WriteByte(chr);

							i = chr = 0;
						}
					}
					else
						recving = bit;
				}
			}
		}
		public void NRecvEnd(string ident)
		{
			using (var s = new EventWaitHandle(
				false, EventResetMode.AutoReset, ident + "S"))
			using (var k = new EventWaitHandle(
				false, EventResetMode.AutoReset, ident + "K"))
			{
				k.Set();
				s.Set();
			}
		}

		// ---- ここまで
	}
}
