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
			foreach (string file in FileTools.lsFiles("."))
			{
				Console.WriteLine(file); // ".\\～" -- フルパスにはならない。
			}

			Test01_a("."); // カレントDIR
			Test01_a("*"); // ex
			Test01_a("abc "); // 空白削られる。
			Test01_a(" abc"); // 空白削られない。
			Test01_a("PRN.txt"); // ex
		}

		private void Test01_a(string path)
		{
			try
			{
				Console.WriteLine("[" + FileTools.makeFullPath(path) + "]");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
