using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte
{
	public class Gnd
	{
		public static Gnd I;

		public string SettingFile = Path.Combine(Program.SelfDir, Path.GetFileNameWithoutExtension(Program.SelfFile) + ".dat");

		public void Load(string file)
		{
			try
			{
				string[] lines = File.ReadAllLines(file, Encoding.UTF8);
				int c = 0;

				this.PortNo = int.Parse(lines[c++]);
				this.MainWin_Minimized = int.Parse(lines[c++]) != 0;
				// 新しい項目をここへ追加...
			}
			catch
			{ }
		}

		public void Save(string file)
		{
			{
				List<string> lines = new List<string>();

				lines.Add("" + this.PortNo);
				lines.Add("" + (this.MainWin_Minimized ? 1 : 0));
				// 新しい項目をここへ追加...

				File.WriteAllLines(file, lines, Encoding.UTF8);
			}
		}

		// 設定ここから

		public int PortNo = 55985;
		public bool MainWin_Minimized = false;

		// 設定ここまで
	}
}
