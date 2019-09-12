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

			WorkingDir.Root = new WorkingDir.RootInfo(@"C:\temp\Chocolate_Test01");

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

			WorkingDir.Root.Delete();
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
			//new Test01().Main05();
			//new Test02().Main01();
			//new StringToolsTest().Test01();
			//new StringToolsTest().Test02();
			//new StringToolsTest().Test03();
			//new StringToolsTest().Test04();
			//new StringToolsTest().Test05();
			//new StringToolsTest().Test06();
			//new FileToolsTest().Test01();
			//new FileToolsTest().Test02();
			//new FileToolsTest().Test03();
			//new SortedListTest().Test01();
			//new CipherToolsTest.AESTest().Test01();
			//new SecurityToolsTest().Test01();
			//new SecurityToolsTest().Test02();
			//new SecurityToolsTest().Test03();
			//new DebugToolsTest().Test01();
			//new HTTPClientTest().Test01();
			//new HTTPClientTest().Test02();
			//new HTTPServerTest().Test01();
			//new HTTPServerTest().Test02();
			//new ReflectToolsTest().Test01();
			//new ReflectToolsTest().Test02();
			//new ReflectToolsTest().Test02b();
			//new ReflectToolsTest().Test03();
			//new ReflectToolsTest().Test03b();
			//new ReflectToolsTest().Test04();
			//new ReflectToolsTest().Test05();
			//new ReflectToolsTest().Test06();
			//new JsonToolsTest().Test01();
			//new JsonToolsTest().Test02();
			//new JsonToolsTest().Test03();
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
			//new EnumerableTrainTest().Test01();
			//new EnumerableTrainTest().Test02();
			//new WrapperTest().Test01();
			//new XmlNodeTest().Test01();
			//new CriticalTest().Test01();
			//new CriticalTest().Test02();
			//new CriticalTest().Test01_2();
			//new CriticalTest().Test02_2();
			//new CSemaphoreTest().Test01();
			//new JStringTest().Test01();
			//new FilingCase3Test().Test01();
			//new FilingCase3Test().Test02();
			//new AttachStringTest().Test01();
			//new EscapeStringTest().Test01();
			//new ObjectTreeTest().Test01();
			//new Base64UnitTest().Test01();
			//new Base64UnitTest().Test02();
			//new ZipToolsTest().Test01();
			//new ZipToolsTest().Test02();
			//new EnumeratorCartridgeTest().Test01();
			//new CanvasTest().Test01();
			//new D2PointTest().Test01();
			//new RandomUnitTest().Test01();
			new EnumerableToolsTest().Test01();
		}
	}
}
