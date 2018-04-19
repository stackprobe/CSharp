using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Tests.Tools;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			Common.WriteLog = message => Console.WriteLine("[TRACE] " + message);

			Common.APP_IDENT = "{993e47b9-4f3f-46dc-8449-b2427ee426de}";
			Common.APP_TITLE = "Test01";

			Common.OnBoot();

			WorkingDir.Root = WorkingDir.CreateRoot();

			try
			{
				new Program().Main2();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			Console.WriteLine("Press ENTER");
			Console.ReadLine();

			WorkingDir.Root.Dispose();
			WorkingDir.Root = null;
		}

		private void Main2()
		{
			new FileToolsTest().Test01();
		}
	}
}
