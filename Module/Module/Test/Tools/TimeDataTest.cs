using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Test.Tools
{
	public class TimeDataTest
	{
		public static void Test01()
		{
			DebugTools.WriteLog(TimeData.Now.ToString());
			DebugTools.WriteLog(new TimeData(0L).ToString());
			DebugTools.WriteLog(new TimeData(long.MaxValue).ToString());
		}
	}
}
