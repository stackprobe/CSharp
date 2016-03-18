using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Charlotte.Tools
{
	public class FileTools
	{
		public static void Create(string file)
		{
			using (new FileStream(file, FileMode.Create, FileAccess.Write))
			{ }
		}

		public static byte[] ReadToEnd(Stream s, bool readZeroKeepReading = false)
		{
			List<byte[]> buff = new List<byte[]>();
			int size = 1024;
			byte[] block = new byte[size];
			int waitMillis = 0;

			for (; ; )
			{
				int readSize = s.Read(block, 0, block.Length);

				if (readSize < 0)
					break;

				if (readSize == 0)
				{
					if (readZeroKeepReading == false)
						break;

					if (waitMillis < 200)
						waitMillis++;

					Thread.Sleep(waitMillis);
				}
				else
				{
					waitMillis = 0;

					if (readSize < block.Length)
					{
						block = ArrayTools.GetPart(block, 0, readSize);

						if (1024 < size)
							size /= 2;
					}
					else
						size *= 2;

					buff.Add(block);
					block = new byte[size];
				}
			}
			return ArrayTools.Join(buff.ToArray());
		}

		public static void Write(Stream s, byte[] block)
		{
			s.Write(block, 0, block.Length);
		}

		public static string MakeTempPath()
		{
			return StringTools.Combine(Environment.GetEnvironmentVariable("TMP"), Guid.NewGuid().ToString("B"));
		}
	}
}
