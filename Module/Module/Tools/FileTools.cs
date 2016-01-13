using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class FileTools
	{
		public static void CreateFileIfNotExist(string file)
		{
			if (File.Exists(file) == false)
			{
				CreateFile(file);
			}
		}

		public static void CreateFile(string file)
		{
			file = Path.GetFullPath(file);

			string dir = Path.GetDirectoryName(file);

			if (Directory.Exists(dir) == false)
				Directory.CreateDirectory(dir);

			File.WriteAllBytes(file, new byte[0]);
		}
	}
}
