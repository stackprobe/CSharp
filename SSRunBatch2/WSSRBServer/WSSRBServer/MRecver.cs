using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Security.Cryptography;
using Charlotte.Tools;

namespace Charlotte
{
	public class MRecver
	{
		public static void MRecv(string ident, Action<byte[]> recved, Func<bool> isAlive)
		{
			Mutex[] hdls = new Mutex[6];
			try
			{
				for (int i = 0; i < hdls.Length; i++)
					hdls[i] = new Mutex(false, ident + i);

				hdls[3].WaitOne();

				List<byte> buff = new List<byte>();
				byte chr = 0x00;
				int waitCount = 1;

				for (int i = 0, c = 0; isAlive(); )
				{
					int n = (c + 1) % 3;

					hdls[3 + n].WaitOne();
					hdls[3 + c].ReleaseMutex();

					bool nBit = hdls[n].WaitOne(0);

					if (nBit)
						hdls[n].ReleaseMutex();

					if (waitCount <= 0)
					{
						if (nBit == false)
							chr |= (byte)(1 << i);

						if (7 <= i)
						{
							if (chr == 0x00)
							{
								recved(buff.ToArray());
								buff.Clear();
								waitCount = 1;
							}
							else
								buff.Add(chr);

							i = 0;
							chr = 0x00;
						}
						else
							i++;
					}
					else
					{
						if (nBit)
						{
							i = 0;

							Thread.Sleep(waitCount);

							if (waitCount < 100)
								waitCount++;
						}
						else if (7 <= i)
						{
							i = 0;
							waitCount = 0;
						}
						else
							i++;
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

		public static string Deserialize(byte[] bh) // bh == message + sha512_128
		{
			byte[] b = new byte[bh.Length - 32];
			byte[] h = new byte[32];

			Array.Copy(bh, 0, b, 0, b.Length);
			Array.Copy(bh, b.Length, h, 0, 32);

			using (SHA512 ha = SHA512.Create())
			{
				byte[] h2 = new byte[32];

				Array.Copy(Encoding.ASCII.GetBytes(BitConverter.ToString(ha.ComputeHash(b)).Replace("-", "").ToLower()), h2, 32); // FIXME

				if (BinTools.Comp(h, h2) != 0)
				{
					throw new Exception("受信したメッセージは壊れています。");
				}
			}
			return Encoding.UTF8.GetString(b);
		}
	}
}
