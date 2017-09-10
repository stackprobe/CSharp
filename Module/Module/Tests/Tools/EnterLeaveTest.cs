using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class EnterLeaveTest
	{
		public void test01()
		{
			try
			{
				using (new EnterLeave(null, delegate
				{
					Console.WriteLine("ELT_T-01_Leave"); // ちゃんと実行される。
				}))
				{
					throw new Exception();
				}
			}
			catch
			{ }
		}
	}
}
