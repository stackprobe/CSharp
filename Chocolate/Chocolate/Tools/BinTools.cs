using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class BinTools
	{
		public static byte[] EMPTY = new byte[0];

		public static int Comp(byte a, byte b)
		{
			if (a < b)
				return -1;

			if (a > b)
				return 1;

			return 0;
		}

		public static int Comp(byte[] a, byte[] b)
		{
			return ArrayTools.Comp(a, b, Comp);
		}

		public class Hex
		{
			public static string ToString(byte chr)
			{
				return ToString(new byte[] { chr });
			}

			public static string ToString(byte[] src)
			{
				StringBuilder buff = new StringBuilder();

				foreach (byte chr in src)
				{
					buff.Append(StringTools.hexadecimal[chr >> 4]);
					buff.Append(StringTools.hexadecimal[chr & 0x0f]);
				}
				return buff.ToString();
			}

			public static byte[] ToBytes(string src)
			{
				if (src.Length % 2 != 0)
					throw new ArgumentException();

				byte[] dest = new byte[src.Length / 2];

				for (int index = 0; index < dest.Length; index++)
					dest[index] = (byte)((To4Bit(src[index * 2]) << 4) | To4Bit(src[index * 2 + 1]));

				return dest;
			}

			private static int To4Bit(char chr)
			{
				int ret = StringTools.hexadecimal.IndexOf(char.ToLower(chr));

				if (ret == -1)
					throw new ArgumentException();

				return ret;
			}
		}

		public static byte[] GetSubBytes(byte[] src, int offset)
		{
			return GetSubBytes(src, offset, src.Length - offset);
		}

		public static byte[] GetSubBytes(byte[] src, int offset, int size)
		{
			byte[] dest = new byte[size];
			Array.Copy(src, offset, dest, 0, size);
			return dest;
		}
	}
}
