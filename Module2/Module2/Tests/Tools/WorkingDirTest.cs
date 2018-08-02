using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.Tests.Tools
{
	public class WorkingDirTest
	{
		public void Test01()
		{
			using (WorkingDir wd = new WorkingDir())
			{
				FileTools.CreateDir(wd.MakePath());
				FileTools.CreateDir(wd.MakePath());
				FileTools.CreateDir(wd.MakePath());
			}
		}
	}
}
