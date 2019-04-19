using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Charlotte.ScrMusHook;

namespace Charlotte.Tests.ScrMusHook
{
	public class ScrMusHook0001Test // -- 0001
	{
		public void Test01()
		{
			ScrMusHook0001 smh = new ScrMusHook0001();

			smh.Hook();
			try
			{
				int waitMillis = 0;

				while (Console.KeyAvailable == false)
				{
					string message = smh.NextMessage();

					if (message == null)
					{
						if (waitMillis < 100)
							waitMillis++;

						Thread.Sleep(waitMillis);
					}
					else
					{
						waitMillis = 0;

						Console.WriteLine(message);
					}
				}
			}
			finally
			{
				smh.Unhook();
				smh = null;
			}
		}
	}
}
