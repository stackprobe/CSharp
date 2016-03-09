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
			DebugTools.WriteLog(TimeData.Now().ToString());
			DebugTools.WriteLog(new TimeData(0L).ToString());
			DebugTools.WriteLog(new TimeData(long.MaxValue).ToString());

			TimeData td = TimeData.Now();
			DebugTools.WriteLog("" + td); td.s++;
			DebugTools.WriteLog("" + td); td.s++;
			DebugTools.WriteLog("" + td); td.s++;
			DebugTools.WriteLog("" + td); td.m++;
			DebugTools.WriteLog("" + td); td.m++;
			DebugTools.WriteLog("" + td); td.m++;
			DebugTools.WriteLog("" + td); td.h++;
			DebugTools.WriteLog("" + td); td.h++;
			DebugTools.WriteLog("" + td); td.h++;
			DebugTools.WriteLog("" + td); td.D++;
			DebugTools.WriteLog("" + td); td.D++;
			DebugTools.WriteLog("" + td); td.D++;
			DebugTools.WriteLog("" + td); td.M++;
			DebugTools.WriteLog("" + td); td.M++;
			DebugTools.WriteLog("" + td); td.M++;
			DebugTools.WriteLog("" + td); td.Y++;
			DebugTools.WriteLog("" + td); td.Y++;
			DebugTools.WriteLog("" + td); td.Y++;
		}
	}
}
