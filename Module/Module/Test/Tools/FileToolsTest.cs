using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Test.Tools
{
	public class FileToolsTest
	{
		public void Test01()
		{
			Console.WriteLine(FileTools.MakeFullPath(@"C:\temp\1.txt"));

			Test01_a(null);
			Test01_a("");
			Test01_a("*");
			Test01_a(@"C:\\\");

			Console.WriteLine(FileTools.MakeFullPath(@"C:\a\PRN"));
		}

		private void Test01_a(string path)
		{
			try
			{
				FileTools.CheckFullPath(path);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
