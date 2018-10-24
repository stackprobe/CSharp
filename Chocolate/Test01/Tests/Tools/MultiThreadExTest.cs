using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class MultiThreadExTest
	{
		public void Test01()
		{
			try
			{
				using (MultiThreadEx mte = new MultiThreadEx())
				{
					mte.Add(() => { throw new Test01_Exception("例外01"); });
					mte.Add(() => { throw new Test01_Exception("例外02"); });
					mte.Add(() => { throw new Test01_Exception("例外03"); });

					mte.RelayThrow();
				}
			}
			catch (Test01_Exception e)
			{
				Console.WriteLine("キャッチした例外：" + e);
			}
		}

		private class Test01_Exception : Exception
		{
			public Test01_Exception(string message)
				: base(message)
			{ }
		}
	}
}
