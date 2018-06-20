using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class LogQueueTest
	{
		public void Test01()
		{
			LogQueue.I.Enqueue("a1");
			LogQueue.I.Enqueue("a2");
			LogQueue.I.Enqueue("a3");

			foreach (string line in LogQueue.I.DequeueAll().Select(e => e.ToString()))
			{
				Console.WriteLine(line);
			}
		}
	}
}
