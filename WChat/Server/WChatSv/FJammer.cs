using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte
{
	public class FJammer
	{
		private static List<string> DecFiles = new List<string>();

		public static string GetFile(string file)
		{
			string encFile = file + ".fkug";

			if (File.Exists(encFile) == false)
				return file;

			string decFile = @".\" + file;

			if (File.Exists(decFile) == false)
			{
				ProcessMan pm = new ProcessMan();
				pm.Start("FJammer.exe", "/D \"" + encFile + "\" \"" + decFile + "\"");
				pm.End();
				pm = null;
			}
			decFile = Path.GetFullPath(decFile);
			DecFiles.Add(decFile);
			return decFile;
		}

		public static void Clear()
		{
			foreach (string file in DecFiles)
				File.Delete(file);

			DecFiles.Clear();
		}
	}
}
