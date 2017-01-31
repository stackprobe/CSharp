using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Test
{
	public class Test01
	{
		public void Main01()
		{
			Console.WriteLine(Path.GetFullPath("C:/temp/abc.txt")); // -> "C:\\temp\\abc.txt"

			//Console.WriteLine(Path.GetFullPath("file://hoge-host/hoge-path")); // -> ex
			//Console.WriteLine(Path.GetFullPath("file:///C:/123")); // -> ex

			Console.WriteLine(Path.GetDirectoryName("C:\\123\\456")); // -> "C:\\123"
			Console.WriteLine(Path.GetDirectoryName("C:\\123")); // -> "C:\\"
			Console.WriteLine(Path.GetDirectoryName(".")); // -> ""
			Console.WriteLine(Path.GetDirectoryName("123")); // -> ""
			Console.WriteLine(Path.GetDirectoryName(".\\123")); // -> "."
			Console.WriteLine(Path.GetDirectoryName(".\\.\\123")); // -> ".\\."
		}
	}
}
