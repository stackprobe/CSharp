using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Satellizer = Charlotte.Satellite.Satellizer;

namespace Charlotte
{
	public class Test02
	{
		public void TestMain()
		{
			try
			{
				new Thread((ThreadStart)delegate
				{
					Thread.Sleep(100);

					Satellizer.Listen("AAA", "BBB", 2000, new TestServer());
				})
				.Start();

				for (int c = 0; c < 100; c++)
				{
					using (Satellizer stllzr = new Satellizer("AAA", "CCC"))
					{
						stllzr.Connect(2000);
						stllzr.Send("TEST_" + c);
						object obj = stllzr.Recv(2000);
						Console.WriteLine("recvObj: " + obj);
					}
				}
			}
			finally
			{
				TestServer.DeadFlag = true;
			}
		}
	}

	public class TestServer : Satellizer.Server
	{
		public static bool DeadFlag;

		public bool Interlude()
		{
			return DeadFlag == false;
		}

		public void ServiceTh(Satellizer stllzr)
		{
			object obj = stllzr.Recv(2000);

			if (obj != null)
			{
				stllzr.Send("[" + obj + "]");
			}
		}
	}
}
