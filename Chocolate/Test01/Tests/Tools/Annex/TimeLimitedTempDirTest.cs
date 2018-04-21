using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using Charlotte.Tools.Annex;

namespace Charlotte.Tests.Tools.Annex
{
	public class TimeLimitedTempDirTest
	{
		public void Test01()
		{
			Console.WriteLine("---- 01");

			Test01_a(1);
			Test01_a(2);
			Test01_a(3);
			Test01_a(10);
			Test01_a(30);

			Console.WriteLine("---- 02");

			Test01_a2(1);
			Test01_a2(2);
			Test01_a2(3);
			Test01_a2(10);
			Test01_a2(30);

			Console.WriteLine("---- 03");
		}

		private static object Test01_a_SYNCROOT = new object();
		private static Exception Test01_a_Ex;

		public void Test01_a(int th_num)
		{
			Console.WriteLine("th_num: " + th_num);
			DateTime sttm = DateTime.Now;

			Thread[] ths = new Thread[th_num];

			Test01_a_Ex = null;

			for (int c = 0; c < th_num; c++)
			{
				ths[c] = new Thread(() =>
				{
					try
					{
						TimeLimitedTempDir wd = new TimeLimitedTempDir("TimeLimitedTempDirTest_Test01_a_tmp");

						for (int d = 0; d < 10; d++)
						{
							string file = wd.MakePath();

							File.WriteAllText(file, d.ToString(), Encoding.ASCII);

							Thread.Sleep(d * 100);

							if (File.ReadAllText(file, Encoding.ASCII) != d.ToString())
								throw new Exception("内容が合わない。");

							File.Delete(file);
						}
					}
					catch (Exception e)
					{
						lock (Test01_a_SYNCROOT)
						{
							Test01_a_Ex = e;
						}
					}
				});
			}

			foreach (Thread th in ths)
				th.Start();

			foreach (Thread th in ths)
				th.Join();

			DateTime edtm = DateTime.Now;
			Console.WriteLine("elapsed: " + (edtm - sttm).TotalMilliseconds + " millis");

			if (Test01_a_Ex != null)
			{
				throw new Exception("Relay", Test01_a_Ex);
			}
		}

		public void Test01_a2(int th_num)
		{
			Console.WriteLine("th_num: " + th_num);
			DateTime sttm = DateTime.Now;

			Thread[] ths = new Thread[th_num];

			Test01_a_Ex = null;

			for (int c = 0; c < th_num; c++)
			{
				ths[c] = new Thread(() =>
				{
					try
					{
						for (int d = 0; d < 10; d++)
						{
							TimeLimitedTempDir wd = new TimeLimitedTempDir("TimeLimitedTempDirTest_Test01_a2_tmp", 2);
							string file = wd.MakePath();

							File.WriteAllText(file, d.ToString(), Encoding.ASCII);

							Thread.Sleep(d * 100);

							if (File.ReadAllText(file, Encoding.ASCII) != d.ToString())
								throw new Exception("内容が合わない。");

							//File.Delete(file);
						}
					}
					catch (Exception e)
					{
						lock (Test01_a_SYNCROOT)
						{
							Test01_a_Ex = e;
						}
					}
				});
			}

			foreach (Thread th in ths)
				th.Start();

			foreach (Thread th in ths)
				th.Join();

			DateTime edtm = DateTime.Now;
			Console.WriteLine("elapsed: " + (edtm - sttm).TotalMilliseconds + " millis");

			if (Test01_a_Ex != null)
			{
				throw new Exception("Relay", Test01_a_Ex);
			}
		}

		public void Test02()
		{
			Console.WriteLine("---- 01");

			{
				TimeLimitedTempDir wb = new TimeLimitedTempDir("0123456789-0123456789-aa-bb-cc");
				Console.WriteLine(wb.MakePath());
			}

			{
				TimeLimitedTempDir wb = new TimeLimitedTempDir("いろはにほへと");
				Console.WriteLine(wb.MakePath());
			}

			{
				TimeLimitedTempDir wb = new TimeLimitedTempDir("チリヌルヲワカ");
				Console.WriteLine(wb.MakePath());
			}

			{
				TimeLimitedTempDir wb = new TimeLimitedTempDir(null);
				Console.WriteLine(wb.MakePath());
			}

			{
				TimeLimitedTempDir wb = new TimeLimitedTempDir(""); // null と同じ Ident になる。
				Console.WriteLine(wb.MakePath());
			}

			Console.WriteLine("---- 02");

			{
				TimeLimitedTempDir wb = new TimeLimitedTempDir("ほげほげ");
				Console.WriteLine(wb.MakePath());
			}

			{
				TimeLimitedTempDir wb = new TimeLimitedTempDir("ほげほげ");
				Console.WriteLine(wb.MakePath());
			}

			Console.WriteLine("---- 03");
		}

		public void Test03()
		{
			DateTime sttm = DateTime.Now;

			for (int c = 0; c < 10000; c++)
				new TimeLimitedTempDir("{614155e1-f0cf-46cf-afea-f37021ff912c}");

			DateTime edtm = DateTime.Now;
			Console.WriteLine((edtm - sttm).TotalMilliseconds + " millis");
		}
	}
}
