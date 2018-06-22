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
			ProcMain.WriteLog = message => Console.WriteLine("[TRACE] " + message);

			ProcMain.APP_IDENT = "{993e47b9-4f3f-46dc-8449-b2427ee426de}";
			ProcMain.APP_TITLE = "Test01";

			ProcMain.OnBoot();

			WorkingDir.Root = new WorkingDir.DirWrapper(@"C:\temp\Chocolate_Test01");

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
			//new FilingCase3Test().Test01();
			//new WorkingDirTest().Test01();
			//new DateTimeToSecTest().Test01();
			//new Test01().Main01();
			//new Test01().Main02();
			//new Test01().Main03();
			//new Test01().Main03b();
			//new StringToolsTest().Test01();
			//new StringToolsTest().Test02();
			//new ArrayToolsTest().Test01();
			new ArrayToolsTest().Test02();
			//new FileToolsTest().Test01();
			//new FileToolsTest().Test02();
			//new SortedListTest().Test01();
			//new SecurityToolsTest.AESTest().Test01();
			//new SecurityToolsTest().Test01();
			//new DebugToolsTest().Test01();
			//new HTTPClientTest().Test01();
			//new ReflecToolsTest().Test01();
			//new ReflecToolsTest().Test02();
			//new ReflecToolsTest().Test02b();
			//new ReflecToolsTest().Test03();
			//new ReflecToolsTest().Test03b();
			//new ReflecToolsTest().Test04();
		}
	}
}
