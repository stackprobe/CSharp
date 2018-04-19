using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class FileToolsTest
	{
		public void Test01()
		{
			using (WorkingDir wd = WorkingDir.Root.Create())
			{
				string dir = wd.MakePath();

				FileTools.CreateDir(dir);
				FileTools.Delete(dir);
			}
		}
	}
}
