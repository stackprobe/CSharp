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

		public void Test01_a(int th_num)
		{
			// TODO
		}

		public void Test02()
		{
			{
				WorkingBench wb = new WorkingBench("0123456789-0123456789-aa-bb-cc");
				Console.WriteLine(wb.MakePath());
			}

			{
				WorkingBench wb = new WorkingBench("いろはにほへと");
				Console.WriteLine(wb.MakePath());
			}

			{
				WorkingBench wb = new WorkingBench("チリヌルヲワカ");
				Console.WriteLine(wb.MakePath());
			}

			{
				WorkingBench wb = new WorkingBench(null);
				Console.WriteLine(wb.MakePath());
			}

			{
				WorkingBench wb = new WorkingBench(""); // null と同じ Ident になる。
				Console.WriteLine(wb.MakePath());
			}
		}
	}
}
