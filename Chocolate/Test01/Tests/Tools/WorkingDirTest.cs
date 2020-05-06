using System;
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
			MessageBox.Show("0");

			using (WorkingDir wd = new WorkingDir())
			{
				MessageBox.Show("1.0");
				wd.GetPath("ABC");
				MessageBox.Show("1.1");
				wd.GetPath("DEF");
				MessageBox.Show("1.2");
			}
			MessageBox.Show("2");
		}
	}
}
