﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Windows.Forms;
using System.IO;

namespace Charlotte.Tests.Tools
{
	public class WorkingDirTest
	{
		public void Test01()
		{
			using(WorkingDir wd = WorkingDir.Root.Create())
			{
				MessageBox.Show("1");
			}
			MessageBox.Show("2");
		}
	}
}