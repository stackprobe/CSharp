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
			ByteBuffer buff = new ByteBuffer();
			byte[] block = new byte[2000000]; // 2 MB
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
					buff.Add(block, 0, readSize);
				}
			}
			return buff.Join();
		}

		public static void Write(Stream s, byte[] block)
		{
			s.Write(block, 0, block.Length);
		}

		public static string MakeTempPath()
		{
			return StringTools.Combine(Environment.GetEnvironmentVariable("TMP"), Guid.NewGuid().ToString("B"));
		}

		public static bool IsSame(string file1, string file2)
		{
			using (FileStream fs1 = new FileStream(file1, FileMode.Open, FileAccess.Read, FileShare.Read, 1000000))
			using (FileStream fs2 = new FileStream(file2, FileMode.Open, FileAccess.Read, FileShare.Read, 1000000))
			{
				for (; ; )
				{
					int chr1 = fs1.ReadByte();
					int chr2 = fs2.ReadByte();

					if (chr1 != chr2)
					{
						return false;
					}
					if (chr1 == -1)
					{
						break;
					}
				}
			}
			return true;
		}

		public static void TryDelete(string file)
		{
			try
			{
				File.Delete(file);
			}
			catch
			{ }
		}
	}
}
