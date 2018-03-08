using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Security.Cryptography;

namespace Charlotte
{
	public class MSender
	{
		public static void MSend(string ident, byte[] message)
		{
			Mutex[] hdls = new Mutex[6];
			try
			{
				for (int i = 0; i < hdls.Length; i++)
					hdls[i] = new Mutex(false, ident + i);

				hdls[3].WaitOne();

				bool[] flgs = new bool[3];
				int c = 0;

				foreach (byte[] bMes in new byte[][] { new byte[] { 0x00, 0xff }, message, new byte[] { 0x00 } })
				{
					for (int i = 0; i < bMes.Length; i++)
					{
						for (int bit = 0; bit < 8; bit++)
						{
							int n = (c + 1) % 3;

							hdls[3 + n].WaitOne();
							hdls[3 + c].ReleaseMutex();

							if ((bMes[i] & (1 << (bit))) != 0)
							{
								if (flgs[n] == false)
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

		public static byte[] Serialize(string message)
		{
			//if (message.IndexOf('\0') != -1) throw new ArgumentException();

			byte[] b = Encoding.UTF8.GetBytes(message);
			byte[] bh = new byte[b.Length + 4]; // message + sha512_28

			Array.Copy(b, bh, b.Length);

			using (SHA512 ha = SHA512.Create())
			{
				byte[] h = ha.ComputeHash(b);

				bh[bh.Length - 4] = (byte)(h[0] | 0x80);
				bh[bh.Length - 3] = (byte)(h[1] | 0x80);
				bh[bh.Length - 2] = (byte)(h[2] | 0x80);
				bh[bh.Length - 1] = (byte)(h[3] | 0x80);
			}
			return bh;
		}
	}
}
