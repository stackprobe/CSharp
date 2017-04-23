using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Test.Tools
{
	public class FailedOperationTest
	{
		public void test01()
		{
			try
			{
				throw new Exception("E-001", new Exception("InnerException_01", new FailedOperation()));
			}
			catch (Exception e)
			{
				FailedOperation.caught(e);
			}
		}
	}
}
