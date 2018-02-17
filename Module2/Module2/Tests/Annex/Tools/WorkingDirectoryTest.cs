using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Charlotte.Annex.Tools;
using System.IO;

namespace Charlotte.Tests.Annex.Tools
{
	public class WorkingDirectoryTest
	{
		public void Test01()
		{
			Test01_a(1);
			Test01_a(2);
			Test01_a(3);
			Test01_a(10);
			Test01_a(30);

			Test01_a2(1);
			Test01_a2(2);
			Test01_a2(3);
			Test01_a2(10);
			Test01_a2(30);
		}

		private static object Test01_a_SYNCROOT = new object();
		private static Exception Test01_a_Ex;

		public void Test01_a(int th_num)
		{
			Thread[] ths = new Thread[th_num];

			Test01_a_Ex = null;

			for (int c = 0; c < th_num; c++)
			{
				ths[c] = new Thread(() =>
				{
					try
					{
						using (WorkingDirectory wd = new WorkingDirectory("Module2_Test01_a_WorkingDirectory"))
						{
							for (int d = 0; d < 100; d++)
							{
								string file = wd.MakePath();

								File.WriteAllText(file, d.ToString(), Encoding.ASCII);

								Thread.Sleep(d);

								if (File.ReadAllText(file, Encoding.ASCII) != d.ToString())
									throw new Exception("内容が合わない。");

								File.Delete(file);
							}
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

			if (Test01_a_Ex != null)
			{
				throw new Exception("Relay", Test01_a_Ex);
			}
		}

		public void Test01_a2(int th_num)
		{
			Thread[] ths = new Thread[th_num];

			Test01_a_Ex = null;

			for (int c = 0; c < th_num; c++)
			{
				ths[c] = new Thread(() =>
				{
					try
					{
						for (int d = 0; d < 20; d++)
						{
							using (WorkingDirectory wd = new WorkingDirectory("Module2_Test01_a_WorkingDirectory", 5))
							{
								string file = wd.MakePath();

								File.WriteAllText(file, d.ToString(), Encoding.ASCII);

								Thread.Sleep(d * 100);

								if (File.ReadAllText(file, Encoding.ASCII) != d.ToString())
									throw new Exception("内容が合わない。");

								//File.Delete(file);
							}
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

			if (Test01_a_Ex != null)
			{
				throw new Exception("Relay", Test01_a_Ex);
			}
		}

		public void Test02()
		{
			Console.WriteLine("---- 01");

			using (WorkingDirectory wb = new WorkingDirectory("0123456789-0123456789-aa-bb-cc"))
			{
				Console.WriteLine(wb.MakePath());
			}

			using (WorkingDirectory wb = new WorkingDirectory("いろはにほへと"))
			{
				Console.WriteLine(wb.MakePath());
			}

			using (WorkingDirectory wb = new WorkingDirectory("チリヌルヲワカ"))
			{
				Console.WriteLine(wb.MakePath());
			}

			using (WorkingDirectory wb = new WorkingDirectory(null))
			{
				Console.WriteLine(wb.MakePath());
			}

			using (WorkingDirectory wb = new WorkingDirectory("")) // null と同じ Ident になる。
			{
				Console.WriteLine(wb.MakePath());
			}

			Console.WriteLine("---- 02");

			using (WorkingDirectory wb = new WorkingDirectory("ほげほげ"))
			{
				Console.WriteLine(wb.MakePath());
			}

			using (WorkingDirectory wb = new WorkingDirectory("ほげほげ"))
			{
				Console.WriteLine(wb.MakePath());
			}

			Console.WriteLine("---- 03");
		}
	}
}
