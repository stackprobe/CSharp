using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.Test.Tools
{
	public class DirectoryToolsTest
	{
		public static void Test01()
		{
			DebugTools.WriteLog("null=" + Directory.Exists(null)); // false
			DebugTools.WriteLog("[]=" + Directory.Exists("")); // false
			DebugTools.WriteLog("[*]=" + Directory.Exists("*")); // false
			DebugTools.WriteLog("[\\]=" + Directory.Exists("\\")); // true
			DebugTools.WriteLog("[.]=" + Directory.Exists(".")); // true
			DebugTools.WriteLog("[..]=" + Directory.Exists("..")); // true
			DebugTools.WriteLog("[\\.]=" + Directory.Exists("\\.")); // true
			DebugTools.WriteLog("[\\..]=" + Directory.Exists("\\..")); // true <- !?w

			//DebugTools.WriteLog("null=" + Path.GetFullPath(null)); // ex
			//DebugTools.WriteLog("[]=" + Path.GetFullPath("")); // ex
			//DebugTools.WriteLog("[*]=" + Path.GetFullPath("*")); // ex
			DebugTools.WriteLog("[\\]=" + Path.GetFullPath("\\"));
			DebugTools.WriteLog("[.]=" + Path.GetFullPath("."));
			DebugTools.WriteLog("[..]=" + Path.GetFullPath(".."));
			DebugTools.WriteLog("[\\.]=" + Path.GetFullPath("\\."));
			DebugTools.WriteLog("[\\..]=" + Path.GetFullPath("\\.."));

			Directory.CreateDirectory(@"C:\temp\a\b\c\d\e\f");
			File.WriteAllText(@"C:\temp\a\b\c\d\e\f\ro", "abcdef", Encoding.ASCII);
			new FileInfo(@"C:\temp\a\b\c\d\e\f").Attributes = FileAttributes.ReadOnly;
			new FileInfo(@"C:\temp\a\b\c\d\e\f\ro").Attributes = FileAttributes.ReadOnly;
			DirectoryTools.Delete(@"C:\temp\a");

			DebugTools.WriteLog("[C:/tmp_0]: " + Directory.GetDirectories(@"C:\tmp")[0]);
		}
	}
}
