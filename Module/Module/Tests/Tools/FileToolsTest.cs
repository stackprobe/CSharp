using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class FileToolsTest
	{
		public void test01()
		{
			foreach (string file in FileTools.lsFiles("."))
			{
				Console.WriteLine(file); // ".\\～" -- フルパスにはならない。
			}

			test01_a("."); // カレントDIR
			test01_a("*"); // ex
			test01_a("abc "); // 空白削られる。
			test01_a(" abc"); // 空白削られない。
			test01_a("PRN.txt"); // ex
		}

		private void test01_a(string path)
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

		public void test02()
		{
			foreach (string line in FileTools.readAllLines(@"C:\var\report_20160417162109_2.txt", Encoding.UTF8))
			{
				Console.WriteLine(line);
			}
		}
	}
}
