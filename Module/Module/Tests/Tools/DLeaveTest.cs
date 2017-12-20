using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class DLeaveTest
	{
		public void test01()
		{
			try
			{
				using (new DLeave(delegate
				{
					Console.WriteLine("DLeaverTest_test01"); // ちゃんと表示される。
				}
				))
				{
					throw null;
				}
			}
			catch
			{ }
		}
	}
}
