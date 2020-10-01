using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.Tests.Tools
{
	public class ZipToolsTest
	{
		public void Test01()
		{
			for (int c = 0; c < 1000; c++)
				Test_Random(Test_Random_GetData(1000));

			for (int c = 0; c < 30; c++)
				Test_Random(Test_Random_GetData(1000000));

			for (int c = 0; c < 10; c++)
				Test_Random(Test_Random_GetData(8000000));
		}

		private void Test_Random(byte[] data)
		{
			byte[] encData = ZipTools.Compress(data);
			byte[] decData = ZipTools.Decompress(encData);

			Console.WriteLine("data: " + data.Length);
			Console.WriteLine("encData: " + encData.Length + ", rate: " + (encData.Length * 1.0 / data.Length));
			Console.WriteLine("decData: " + decData.Length);

			if (ArrayTools.Comp(data, decData, BinTools.Comp) != 0)
				throw null; // bugged !!!
		}

		private byte[] Test_Random_GetData(int maxSize)
		{
			return Encoding.ASCII.GetBytes(SecurityTools.MakePassword(StringTools.DECIMAL, SecurityTools.CRandom.GetInt(maxSize)));
		}

		public void Test02()
		{
			Test02_a(BinTools.EMPTY, 0, false);
			Test02_a(BinTools.EMPTY, 1, false);

			for (int c = 0; c < 10; c++)
			{
				byte[] src = Encoding.ASCII.GetBytes(StringTools.Repeat("A", SecurityTools.CRandom.GetRange(1, 1000000)));

				Test02_a(src, 0, true);
				Test02_a(src, src.Length - 1, true);
				Test02_a(src, src.Length, false);
				Test02_a(src, src.Length + 1, false);
			}

			for (int c = 0; c < 100; c++)
			{
				byte[] src = Encoding.ASCII.GetBytes(StringTools.Repeat("A", SecurityTools.CRandom.GetRange(0, 1000000)));
				int limit = SecurityTools.CRandom.GetRange(0, 1000000);
				bool willError = limit < src.Length;

				Test02_a(src, limit, willError);
			}
		}

		private void Test02_a(byte[] src, int limit, bool willError)
		{
			Console.WriteLine(src.Length + ", " + limit + ", " + willError); // test

			Test02_a_mem(src, limit, willError);
			Test02_a_file(src, limit, willError);
		}

		private void Test02_a_mem(byte[] src, int limit, bool willError)
		{
			byte[] mid = ZipTools.Compress(src);
			byte[] dest = null;
			bool errorOccurred = false;

			try
			{
				dest = ZipTools.Decompress(mid, limit);
			}
			catch
			{
				errorOccurred = true;
			}

			if (errorOccurred != willError)
				throw null; // bugged !!!

			if (!errorOccurred && ArrayTools.Comp(src, dest, BinTools.Comp) != 0)
				throw null; // bugged !!!
		}

		private void Test02_a_file(byte[] src, int limit, bool willError)
		{
			using (WorkingDir wd = new WorkingDir())
			{
				String srcFile = wd.MakePath();
				String midFile = wd.MakePath();
				String destFile = wd.MakePath();
				bool errorOccurred = false;

				File.WriteAllBytes(srcFile, src);

				ZipTools.Compress(srcFile, midFile);

				try
				{
					ZipTools.Decompress(midFile, destFile, limit);
				}
				catch
				{
					errorOccurred = true;
				}

				if (errorOccurred != willError)
					throw null; // bugged !!!

				if (!errorOccurred && BinTools.CompFile(srcFile, destFile) != 0)
					throw null; // bugged !!!
			}
		}
	}
}
