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

			if (File.Exists(encFile))
			{
				if (File.Exists(file) == false)
				{
					ProcessMan pm = new ProcessMan();
					pm.Start("FJammer.exe", "/D \"" + encFile + "\" \"" + file + "\"");
					pm.End();
					pm = null;

					if (File.Exists(file) == false)
						throw new Exception("ファイル出力エラー：" + file);
				}
				DecFiles.Add(Path.GetFullPath(file));
			}
			return file;
		}

		public static void Clear()
		{
			foreach (string file in DecFiles)
				File.Delete(file);

			DecFiles.Clear();
		}
	}
}
