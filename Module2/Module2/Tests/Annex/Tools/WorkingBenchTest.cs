using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Charlotte.Annex.Tools;
using System.IO;

namespace Charlotte.Tests.Annex.Tools
{
	public class WorkingBenchTest
	{
		public void Test01()
		{
			Test01_a(1);
			Test01_a(2);
			Test01_a(3);
			Test01_a(10);
			Test01_a(30);
		}

		public void Test02()
		{
			using (WorkingBench wb = new WorkingBench("0123456789-0123456789-aa-bb-cc"))
			{
				Console.WriteLine(wb.MakePath());
			}
			using (WorkingBench wb = new WorkingBench("いろはにほへと"))
			{
				Console.WriteLine(wb.MakePath());
			}
			using (WorkingBench wb = new WorkingBench("チリヌルヲワカ"))
			{
				Console.WriteLine(wb.MakePath());
			}
			using (WorkingBench wb = new WorkingBench(null))
			{
				Console.WriteLine(wb.MakePath());
			}
			using (WorkingBench wb = new WorkingBench("")) // null と同じ Ident になる。
			{
				Console.WriteLine(wb.MakePath());
			}
		}

		private static object Test01_a_SYNCROOT = new object();
		private static Exception Test01_a_Ex;

		public void Test01_a(int th_num)
		{
			Console.WriteLine("th_num: " + th_num);

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
							using (WorkingBench wb = new WorkingBench("{71714f16-6409-4db7-ac0f-2314f2e19bd8}"))
							{
								string file = wb.MakePath();

								File.WriteAllText(file, d.ToString(), Encoding.ASCII);

								Thread.Sleep(d * 10);

								if (File.ReadAllText(file, Encoding.ASCII) != d.ToString())
								{
									throw new Exception("合わない！");
								}
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

			Console.WriteLine("*1");

			foreach (Thread th in ths)
				th.Start();

			Console.WriteLine("*2");

			foreach (Thread th in ths)
				th.Join();

			Console.WriteLine("*3");

			if (Test01_a_Ex != null)
			{
				throw new Exception("Relay", Test01_a_Ex);
			}
		}
	}
}
