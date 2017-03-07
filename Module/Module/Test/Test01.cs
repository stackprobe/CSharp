using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Test
{
	public class Test01
	{
		public void main01()
		{
			Console.WriteLine(Path.GetFullPath("C:/temp/abc.txt")); // -> "C:\\temp\\abc.txt"

			//Console.WriteLine(Path.GetFullPath(null)); // -> ex
			//Console.WriteLine(Path.GetFullPath("")); // -> ex
			Console.WriteLine(Path.GetFullPath(".")); // -> カレント

			//Console.WriteLine(Path.GetFullPath("file://hoge-host/hoge-path")); // -> ex
			//Console.WriteLine(Path.GetFullPath("file:///C:/123")); // -> ex

			Console.WriteLine(Path.GetDirectoryName("C:\\123\\456")); // -> "C:\\123"
			Console.WriteLine(Path.GetDirectoryName("C:\\123")); // -> "C:\\"
			Console.WriteLine(Path.GetDirectoryName(".")); // -> ""
			Console.WriteLine(Path.GetDirectoryName("123")); // -> ""
			Console.WriteLine(Path.GetDirectoryName(".\\123")); // -> "."
			Console.WriteLine(Path.GetDirectoryName(".\\.\\123")); // -> ".\\."

			//Console.WriteLine(Path.GetFullPath("\\\\.")); // -> ex
			//Console.WriteLine(Path.GetFullPath("\\\\mimiko")); // -> ex
			//Console.WriteLine(Path.GetFullPath("\\\\mimiko\\")); // -> ex
			//Console.WriteLine(Path.GetFullPath("\\\\mimiko\\.")); // -> ex
			Console.WriteLine(Path.GetFullPath("\\\\mimiko\\pub")); // -> "\\\\mimiko\\pub"
			Console.WriteLine(Path.GetFullPath("\\\\mimiko\\pub\\")); // -> "\\\\mimiko\\pub\\"
			Console.WriteLine(Path.GetFullPath("\\\\mimiko\\pub\\.")); // -> "\\\\mimiko\\pub"
			Console.WriteLine(Path.GetFullPath("\\\\mimiko\\pub\\..")); // -> "\\\\mimiko\\pub"
			Console.WriteLine(Path.GetFullPath("\\\\mimiko\\pub\\\\.")); // -> "\\\\mimiko\\pub"
			Console.WriteLine(Path.GetFullPath("\\\\mimiko\\pub\\\\..")); // -> "\\\\mimiko\\pub"
			Console.WriteLine(Path.GetFullPath("\\\\mimiko\\pub\\.\\")); // -> "\\\\mimiko\\pub\\"
			Console.WriteLine(Path.GetFullPath("\\\\mimiko\\pub\\..\\")); // -> "\\\\mimiko\\pub\\"
			Console.WriteLine(Path.GetFullPath("\\\\mimiko\\pub\\abc\\.")); // -> "\\\\mimiko\\pub\\abc"
			Console.WriteLine(Path.GetFullPath("\\\\mimiko\\pub\\abc\\..")); // -> "\\\\mimiko\\pub"
			Console.WriteLine(Path.GetFullPath("\\\\mimiko\\pub\\abc\\\\.")); // -> "\\\\mimiko\\pub\\abc"
			Console.WriteLine(Path.GetFullPath("\\\\mimiko\\pub\\abc\\\\..")); // -> "\\\\mimiko\\pub"
			Console.WriteLine(Path.GetFullPath("\\\\mimiko\\pub\\abc\\.\\")); // -> "\\\\mimiko\\pub\\abc\\"
			Console.WriteLine(Path.GetFullPath("\\\\mimiko\\pub\\abc\\..\\")); // -> "\\\\mimiko\\pub\\"

			//Directory.GetFiles(@"C:\存在しないDir_wefojwefowefuvkjnsdvl"); // -> ex
			//Directory.GetDirectories(@"C:\存在しないDir_wefojwefowefuvkjnsdvl"); // -> ex
		}
	}
}
