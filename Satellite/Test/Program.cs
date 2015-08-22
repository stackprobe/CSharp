using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Satellite.Tools;
using System.Diagnostics;
using Charlotte.Satellite;
using System.Threading;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Main2(args);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			Console.WriteLine("### 終了しました。エンターキーを押してね ###");
			Console.ReadLine();
		}

		private void Main2(string[] args)
		{
			if (1 <= args.Length)
			{
				if (args[0] == "/T1C")
				{
					Test01_Client();
					return;
				}
				if (args[0] == "/T1S")
				{
					Test01_Server();
					return;
				}
			}
			Test01();
		}

		private const string SELF_FILE = @"C:\Dev\CSharp\Satellite\Test\bin\Debug\Test.exe";

		private void Test01()
		{
			Process.Start(SELF_FILE, "/T1S");
			Test01_Client();
		}

		private void Test01_Client()
		{
			Thread[] thl = new Thread[20];

			for (int cc = 0; cc < 20; cc++)
			{
				int c = cc;

				thl[c] = new Thread((ThreadStart)delegate
				{
					using (Satellizer stllzr = new Satellizer("TEST_GROUP", "CLIENT"))
					{
						for (int d = 0; d < 50; d++)
						{
							string testData = "TEST_STRING_" + c + "_" + d;

							Console.WriteLine("testData: " + testData);

							while (stllzr.Connect(2000) == false)
							{
								Console.WriteLine("接続ナシ_リトライします。" + c);
							}
							stllzr.Send(testData);

							for (; ; )
							{
								string retData = (string)stllzr.Recv(2000);

								if (retData != null)
								{
									string assumeData = testData + "_RET";

									Console.WriteLine("retData: " + retData);
									Console.WriteLine("assumeData: " + assumeData);

									if (retData != assumeData)
										throw new Exception("想定したデータと違う。");

									break;
								}
							}
							stllzr.Disconnect();
						}
					}
				});
			}
			for (int c = 0; c < 20; c++)
				thl[c].Start();

			for (int c = 0; c < 20; c++)
				thl[c].Join();
		}

		private void Test01_Server()
		{
			Satellizer.Listen("TEST_GROUP", "SERVER", 2000, new Server_Test01());
		}

		private class Server_Test01 : Satellizer.Server
		{
			private object SYNCROOT = new object();
			private int _c;

			public bool Interlude()
			{
				lock (SYNCROOT)
				{
					return _c < 20 * 50;
				}
			}

			public void ServiceTh(Satellizer stllzr)
			{
				for (; ; )
				{
					string retData = (string)stllzr.Recv(2000);

					if (retData != null)
					{
						Console.WriteLine("retData: " + retData);

						stllzr.Send(retData + "_RET");

						lock (SYNCROOT)
						{
							_c++;
						}
						break;
					}
				}
			}
		}
	}
}
