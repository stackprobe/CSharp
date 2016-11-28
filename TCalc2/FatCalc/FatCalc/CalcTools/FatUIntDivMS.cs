using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Charlotte.CalcTools
{
	class FatUIntDivMS
	{
		public static void Div(FatUInt a, FatUInt b, FatUInt ret)
		{
			BigInteger l = new BigInteger(ToBytes(a.Figures));
			BigInteger r = new BigInteger(ToBytes(b.Figures));

			BigInteger ans = BigInteger.Divide(l, r);

			ToFigures(ans.ToByteArray(), ret.Figures);

			{
				FatUInt rem = FatUInt.Red(a, FatUInt.Mul(ret, b));

				if (rem.IsZero() == false)
					ret.Rem = rem;
			}
		}

		private static byte[] ToBytes(List<uint> src)
		{
			byte[] dest = new byte[src.Count * 4 + 1];

			for (int index = 0; index < src.Count; index++)
			{
				dest[index * 4 + 0] = (byte)((src[index] >> 0) & 0xff);
				dest[index * 4 + 1] = (byte)((src[index] >> 8) & 0xff);
				dest[index * 4 + 2] = (byte)((src[index] >> 16) & 0xff);
				dest[index * 4 + 3] = (byte)((src[index] >> 24) & 0xff);
			}
			dest[src.Count * 4] = 0;
			return dest;
		}

		private static void ToFigures(byte[] src, List<uint> dest)
		{
			dest.Clear();

			for (int index = 0; index + 4 <= src.Length; index += 4)
			{
				dest.Add(
					((uint)src[index + 0] << 0) |
					((uint)src[index + 1] << 8) |
					((uint)src[index + 2] << 16) |
					((uint)src[index + 3] << 24)
					);
			}
			switch (src.Length % 4)
			{
				case 0:
					break;
				case 1:
					dest.Add(
						(uint)src[src.Length - 1]
						);
					break;
				case 2:
					dest.Add(
						((uint)src[src.Length - 2] << 0) |
						((uint)src[src.Length - 1] << 8)
						);
					break;
				case 3:
					dest.Add(
						((uint)src[src.Length - 3] << 0) |
						((uint)src[src.Length - 2] << 8) |
						((uint)src[src.Length - 1] << 16)
						);
					break;
				default:
					throw null;
			}
		}
	}
}
