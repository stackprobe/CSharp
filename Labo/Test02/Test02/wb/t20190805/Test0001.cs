using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Diagnostics;

namespace Charlotte.wb.t20190805
{
	public class Test0001
	{
		public void Test01()
		{
			Test01_a((list, index) => ExtraTools.DesertElement(list, index), 30000);
			Test01_a((list, index) => ExtraTools.FastDesertElement(list, index), 30000);
			Test01_a((list, index) => ExtraTools.FastDesertElement(list, index), 100000);
			Test01_a((list, index) => ExtraTools.FastDesertElement(list, index), 1000000);
			Test01_a((list, index) => ExtraTools.FastDesertElement(list, index), 10000000);
		}

		private void Test01_a(Action<List<string>, int> desert, int count)
		{
			Stopwatch sw = new Stopwatch();

			for (int c = 0; c < 10; c++)
			{
				List<string> lines = new List<string>();

				for (int index = 0; index < count; index++)
				{
					lines.Add(SecurityTools.CRandom.GetReal() < 0.5 ? "ALPHA" : "BETA");
				}

				sw.Restart();

				for (int index = 0; index < lines.Count; index++)
				{
					if (lines[index][0] == 'A')
					{
						desert(lines, index);
						index--;
					}
				}

				sw.Stop();

				Console.WriteLine(sw.ElapsedMilliseconds + " (" + lines.Count + ")");
			}
		}
	}
}
