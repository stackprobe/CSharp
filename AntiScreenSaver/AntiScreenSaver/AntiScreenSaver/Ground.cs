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
		public static Icon[] Icons = new Icon[11];

		public class XYPoint
		{
			public int X;
			public int Y;
		}

		public static List<XYPoint> MouseShakeRoute = new List<XYPoint>();

		private static string GetConfFile()
		{
			string file = "AntiScreenSaver.conf";

			if (File.Exists(file) == false)
				file = @"..\..\..\..\doc\AntiScreenSaver.conf";

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

		public static void LoadConf()
		{
			string[] lines = File.ReadAllLines(GetConfFile(), Encoding.GetEncoding(932));
			lines = RemoveCommentEmptyLine(lines);
			int c = 0;

			for (; ; )
			{
				string line = lines[c++];

				if (line == "-")
					break;

				string[] tokens = line.Split(',');

				MouseShakeRoute.Add(new XYPoint()
				{
					X = int.Parse(tokens[0]),
					Y = int.Parse(tokens[1]),
				});
			}
		}
	}
}
