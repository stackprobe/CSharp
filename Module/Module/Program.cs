using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools.Test;
using Charlotte.Tools;
using System.Diagnostics;

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
			HttpClientTest.Test1();
		}
	}
}
