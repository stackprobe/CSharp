using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Labo
{
	public class LaboTest01
	{
		public void Test01()
		{
			Console.WriteLine(Path.GetFullPath("C:/temp/abc.txt")); // -> "C:\\temp\\abc.txt"
		}
	}
}
