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

		public string ConfFile = Path.Combine(Program.SelfDir, Path.GetFileNameWithoutExtension(Program.SelfFile) + ".conf");

		public void LoadConf(string file)
		{
			try
			{
				string[] lines = File.ReadAllLines(file, Encoding.UTF8).Where(line => line != "" && line.StartsWith(";") == false).ToArray();
				int c = 0;

				this.DummyConf = lines[c++];
				// ここへ追加...
			}
			catch
			{ }
		}

		// Conf設定ここから

		public string DummyConf = "＠＠";

		// Conf設定ここまで

		public string SettingFile = Path.Combine(Program.SelfDir, Path.GetFileNameWithoutExtension(Program.SelfFile) + ".dat");

		public void Load(string file)
		{
			try
			{
				string[] lines = File.ReadAllLines(file, Encoding.UTF8);
				int c = 0;

				this.Dummy = lines[c++];
				// ここへ追加...
			}
			catch
			{ }
		}

		public void Save(string file)
		{
			{
				List<string> lines = new List<string>();

				lines.Add(this.Dummy);
				// ここへ追加...

				File.WriteAllLines(file, lines, Encoding.UTF8);
			}
		}

		// 設定ここから

		public string Dummy = "＠～";

		// 設定ここまで
	}
}
