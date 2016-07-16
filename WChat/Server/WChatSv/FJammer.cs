using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte
{
	public class FJammer
	{
		public static void Decode(string file)
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
				File.Delete(encFile);
			}
		}
	}
}
