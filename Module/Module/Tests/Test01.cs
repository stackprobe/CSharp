﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte.Tests
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

			//Directory.Delete(@"C:\temp\123");
			//Directory.Delete(@"C:\temp\123"); // 存在しないので、-> ex
			Directory.CreateDirectory(@"C:\temp\123");
			Directory.CreateDirectory(@"C:\temp\123"); // 存在したら何もせず続行
			Directory.Delete(@"C:\temp\123");

			Console.WriteLine("" + Directory.Exists(null)); // -> false
			Console.WriteLine("" + Directory.Exists("")); // -> false
			Console.WriteLine("" + File.Exists(null)); // -> false
			Console.WriteLine("" + File.Exists("")); // -> false

			Console.WriteLine("" + Path.GetExtension("abc.txt.extent")); // -> ".extent"
			Console.WriteLine("" + Path.GetExtension("abc.txt")); // -> ".txt"
			Console.WriteLine("" + Path.GetExtension("abc")); // -> ""
			Console.WriteLine("" + Path.GetExtension(".abc")); // -> ".abc"
			Console.WriteLine("" + Path.GetExtension("")); // -> ""
			Console.WriteLine("" + Path.GetExtension(null)); // -> null
			Console.WriteLine("" + Path.GetExtension(@"C:\abc.def\")); // -> ""
			Console.WriteLine("" + Path.GetExtension(@"C:\abc.def\.")); // -> ""
			Console.WriteLine("" + Path.GetExtension(@"C:\abc.def\.abc")); // -> ".abc"

			test01(false, false);
			test01(false, true);
			test01(true, false);
			test01(true, true);

			//test02(); // ie

			Console.WriteLine("" + Path.GetFileName(@"C:\abc\def"));
			Console.WriteLine("" + Path.GetFileName(@"C:\abc"));
			Console.WriteLine("" + Path.GetFileName(@"C:\"));
			Console.WriteLine("" + Path.GetFileName(@"\\host\abc\def"));
			Console.WriteLine("" + Path.GetFileName(@"\\host\abc"));
			Console.WriteLine("" + Path.GetFileName(@"\\host\"));
			Console.WriteLine("" + Path.GetFileName(@"\\host"));

			//File.Copy(@"C:\var\月姫リメイクop.mp3", @"C:\temp\111\222\333\aaa\bbb\ccc\def.mp3"); // コピー咲の親dir無し -> ex -- コピー咲の親dirまでは作ってくれなかった。

			Console.WriteLine(StringTools.toHex(Encoding.UTF8.GetBytes("ABC\0def")));
			Console.WriteLine(StringTools.toHex(Encoding.UTF8.GetBytes("ABC\0def".Replace("\0",""))));
		}

		private void test01(bool f, bool g)
		{
			Console.WriteLine(f + ", " + g);
			f |= g;
			Console.WriteLine("f |= g; -> " + f);
		}

		private void test02()
		{
			IEnumerator<object> ie = test02_getIE().GetEnumerator();

			MessageBox.Show("go");
			MessageBox.Show("ret_1: " + ie.MoveNext());
			MessageBox.Show("ret_2: " + ie.MoveNext());
			MessageBox.Show("ret_3: " + ie.MoveNext());
			MessageBox.Show("ret_4: " + ie.MoveNext());

			ie = test02_getIE_B().GetEnumerator();

			try
			{
				MessageBox.Show("go_B");
				MessageBox.Show("ret_1: " + ie.MoveNext());
				MessageBox.Show("ret_2: " + ie.MoveNext());
				MessageBox.Show("ret_3: " + ie.MoveNext());
				MessageBox.Show("ret_4: " + ie.MoveNext());
			}
			catch
			{
				MessageBox.Show("caught");
			}

			MessageBox.Show("ret_e1: " + ie.MoveNext());
			MessageBox.Show("ret_e2: " + ie.MoveNext());
		}

		private IEnumerable<object> test02_getIE()
		{
			MessageBox.Show("a");
			yield return null;
			MessageBox.Show("b");
			yield return null;
			MessageBox.Show("c");
		}

		private IEnumerable<object> test02_getIE_B()
		{
			MessageBox.Show("aa");
			yield return null;
			throw null;
		}
	}
}
