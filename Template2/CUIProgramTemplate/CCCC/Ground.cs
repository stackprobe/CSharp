using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte
{
	public class Gnd
	{
		public static Gnd I;

		public string SettingFile = Path.Combine(Common.SelfDir, Path.GetFileNameWithoutExtension(Common.SelfFile) + ".dat");

		public void Load(string file)
		{
			try
			{
				string[] lines = File.ReadAllLines(file, Encoding.UTF8);
				int c = 0;

				this.FirstLineComment = lines[c++];
				// ここへ追加...
			}
			catch
			{ }
		}

		public void Save(string file)
		{
			{
				List<string> lines = new List<string>();

				lines.Add(this.FirstLineComment);
				// ここへ追加...

				File.WriteAllLines(file, lines, Encoding.UTF8);
			}
		}

		// 設定ここから

		public string FirstLineComment = "＠～";

		// 設定ここまで
	}
}
