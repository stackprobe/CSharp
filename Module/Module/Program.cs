using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Charlotte.Test;
using Charlotte.Tools;

namespace Module
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Program2.Main2();
			}
			catch (Exception e)
			{
				DebugTools.WriteLog("e: " + e);
			}
			Process.Start(@"C:\temp");
		}
	}
}
