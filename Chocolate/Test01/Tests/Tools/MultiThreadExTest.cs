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
					mte.Add(() => { throw new Exception("例外01"); });
					mte.Add(() => { throw new Exception("例外02"); });
					mte.Add(() => { throw new Exception("例外03"); });

					mte.RelayThrow();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("キャッチした例外：" + e);
			}
		}
	}
}
