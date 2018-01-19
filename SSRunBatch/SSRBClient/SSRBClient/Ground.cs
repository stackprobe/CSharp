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

				this.FirstLineComment = lines[c++];
				this.ServerDomain = lines[c++];
				this.ServerPortNo = int.Parse(lines[c++]);
				// 新しい項目をここへ追加...
			}
			catch
			{ }
		}

		public void Save(string file)
		{
			{
				List<string> lines = new List<string>();

				lines.Add(this.FirstLineComment);
				lines.Add(this.ServerDomain);
				lines.Add("" + this.ServerPortNo);
				// 新しい項目をここへ追加...

				File.WriteAllLines(file, lines, Encoding.UTF8);
			}
		}

		// 設定ここから

		public string FirstLineComment = "このファイルは " + Program.APP_TITLE + " のデータ・ファイルです。妄りに編集しないで下さい。";

		public string ServerDomain = "localhost";
		public int ServerPortNo = 55985;

		// 設定ここまで

		public List<string> SendFiles = new List<string>();
		public List<string> RecvFiles = new List<string>();

		public string[] Response = new string[] { };

		public string Status = "";
		public bool StatusErrorFlag = false;
	}
}
