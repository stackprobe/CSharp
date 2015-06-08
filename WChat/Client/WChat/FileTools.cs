using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte
{
	public class FileTools
	{
		public static string Correct(string path)
		{
			string prefix = "";

			if (StringTools.ToFormat(path).ToUpper().StartsWith("A:\\"))
			{
				prefix = path.Substring(0, 3).ToUpper();
				path = path.Substring(3);
			}
			if (path.StartsWith("\\\\"))
			{
				prefix = "\\\\";
				path = path.Substring(2);
			}
			path = StringTools.ReplaceLoop(path, "\\\\", "\\", 10);

			if (path.StartsWith("\\"))
				path = path.Substring(1);

			if(path.EndsWith("\\"))
				path = path.Substring(0, path.Length - 1);

			path = prefix + path;
			return path;
		}
	}
}
