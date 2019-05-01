using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace Charlotte
{
	public class Gnd
	{
		private static string GetConfFile()
		{
			string file = "NoScreenSaver.conf";

			if (File.Exists(file) == false)
				file = @"..\..\..\..\res\NoScreenSaver_Test.conf";

			return file;
		}

		private static string[] RemoveCommentEmptyLine(string[] src)
		{
			List<string> dest = new List<string>();

			foreach (string src_line in src)
			{
				string line = src_line.Trim();

				if (line != "" && line.StartsWith(";") == false)
				{
					dest.Add(line);
				}
			}
			return dest.ToArray();
		}

		// ---- Items ----

		public static int WakeupPeriodMillis;

		// ----

		public static void LoadConf()
		{
			string[] lines = File.ReadAllLines(GetConfFile(), Encoding.GetEncoding(932));
			lines = RemoveCommentEmptyLine(lines);
			int c = 0;

			// ---- Items ----

			WakeupPeriodMillis = int.Parse(lines[c++]);

			// ----

			if (lines[c++] != "\\e")
				throw new Exception("no \\e");
		}
	}
}
