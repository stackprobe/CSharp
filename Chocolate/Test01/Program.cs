using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Tests;
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
			//new Test01().Main04();
			//new StringToolsTest().Test01();
			//new StringToolsTest().Test02();
			//new FileToolsTest().Test01();
			//new FileToolsTest().Test02();
			//new SortedListTest().Test01();
			//new SecurityToolsTest.AESTest().Test01();
			//new SecurityToolsTest().Test01();
			//new DebugToolsTest().Test01();
			//new HTTPClientTest().Test01();
			//new HTTPClientTest().Test02();
			//new HTTPServerTest().Test01();
			new HTTPServerTest().Test02();
			//new ReflectToolsTest().Test01();
			//new ReflectToolsTest().Test02();
			//new ReflectToolsTest().Test02b();
			//new ReflectToolsTest().Test03();
			//new ReflectToolsTest().Test03b();
			//new ReflectToolsTest().Test04();
			//new ReflectToolsTest().Test05();
			//new BatchClientTest().Test01();
			//new JsonToolsTest().Test01();
			//new ArrayToolsTest().Test01();
			//new BinToolsTest().Test01();
			//new BinToolsTest().Test02();
			//new SharedQueueTest().Test01();
			//new ThreadExTest().Test01();
			//new ThreadExTest().Test02();
			//new MultiThreadExTest().Test01();
			//new SyncListTest().Test01();
			//new MultiThreadTaskInvokerTest().Test01();
			//new MultiThreadTaskInvokerTest().Test02();
			//new MultiThreadTaskInvokerTest().Test03();
			//new HugeQueueTest().Test01();
			//new HugeQueueTest().Test02();
			//new EnumerableListTest().Test01();
			//new WrapperTest().Test01();
			//new XmlNodeTest().Test01();
			//new CriticalTest().Test01();
		}
	}
}
