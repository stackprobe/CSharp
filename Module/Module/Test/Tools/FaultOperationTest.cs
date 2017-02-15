using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Test.Tools
{
	public class FaultOperationTest
	{
		public void test01()
		{
			try
			{
				throw new Exception("E-001", new Exception("InnerException_01", new FaultOperation()));
			}
			catch (Exception e)
			{
				FaultOperation.caught(e);
			}
		}
	}
}
