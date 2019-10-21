using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.wb.t20190716
{
	public class Test0001
	{
		public void Test01()
		{
#if false
			for (int a = 0; a <= 255; a++)
			{
				for (int c = 0; c <= 255; c++)
				{
					for (int a2 = 0; a2 <= 255; a2++)
					{
						for (int c2 = 0; c2 <= 255; c2++)
						{
							Test01_a(a, c, a2, c2);
						}
					}
				}
			}
#else
			Console.WriteLine("Ctrl^C to stop this !!!");

			for (; ; )
			{
				int a = SecurityTools.CRandom.GetInt(256);
				int c = SecurityTools.CRandom.GetInt(256);
				int a2 = SecurityTools.CRandom.GetInt(256);
				int c2 = SecurityTools.CRandom.GetInt(256);

				Test01_a(a, c, a2, c2);
			}
#endif
		}

		private void Test01_a(int a, int c, int a2, int c2)
		{
			double ga = a;
			double gc = c;
			double sa = a2;
			double sc = c2;

			ga *= 255.0 - sa;
			ga /= 255.0;

			double ra = sa + ga;
			double rc = (ga * gc + sa * sc) / (ga + sa);

			const double MARGIN = 0.1;

			if (
				ra < 0.0 - MARGIN || 255.0 + MARGIN < ra ||
				rc < 0.0 - MARGIN || 255.0 + MARGIN < rc
				)
				Console.WriteLine(a + " " + c + " " + a2 + " " + c2 + " => " + ra + " " + rc);
		}

		public void Test02()
		{
			for (int a = 0; a <= 255; a++)
			{
				for (int a2 = 0; a2 <= 255; a2++)
				{
					Test02_a(a, a2);
				}
			}
		}

		private void Test02_a(int a, int a2)
		{
			double sa = a;
			double ga = a2;

			ga *= 255 - sa;
			ga /= 255;

			double aa = sa + ga;

			//const double MARGIN = 0.1;
			//const double MARGIN = 0.0;
			const double MARGIN = -0.0001;

			if (255.0 + MARGIN < aa)
				Console.WriteLine(a + " " + a2 + " => " + aa);
		}
	}
}
