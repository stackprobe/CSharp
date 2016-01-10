using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class StreamTools
	{
		public static byte[] ReadToEnd(Stream s)
		{
			List<byte[]> buff = new List<byte[]>();
			int size = 1024;

			for (; ; )
			{
				byte[] block = new byte[size];
				int readSize = s.Read(block, 0, block.Length);

				if (readSize <= 0)
					break;

				if (readSize < block.Length)
				{
					block = ArrayTools.SubArray(block, 0, readSize);
					buff.Add(block);
					break;
				}
				buff.Add(block);
				size *= 2;
			}
			return ArrayTools.Join(buff.ToArray());
		}

		public static void Write(Stream s, byte[] block)
		{
			s.Write(block, 0, block.Length);
		}
	}
}
