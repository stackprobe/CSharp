using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class ExtraToolsTest
	{
		public void Test01()
		{
			Console.WriteLine(ExtraTools.MakeFreeDir());
			Console.WriteLine(ExtraTools.MakeFreeDir());
			Console.WriteLine(ExtraTools.MakeFreeDir());
		}
	}
}
