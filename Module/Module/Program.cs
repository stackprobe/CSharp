using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Charlotte.Test;
using Charlotte.Tools;
using Charlotte.Test.Tools;

namespace Module
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Main2();
			}
			catch (Exception e)
			{
				DebugTools.WriteLog("e: " + e);
			}
			Process.Start(@"C:\temp");
		}

		private static void Main2()
		{
			NectarTest.Test01();
			//TimeDataTest.Test01();
			//CsvDataTest.Test01();
		}
	}
}
