using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte
{
	public class Conf
	{
		// doc\HechimaClient2.conf の修正も忘れずに！

		public string MessageTextFontFamily = Consts.S_DUMMY; // S_DUMMY == 無効
		public int MessageTextFontSize = 0; // 0 == 無効
		public int MessageText_H = 0; // 0 == 無効

		public int RemarksTextMaxLength = 3000000;
		public int RemarksTextClearPct = 20;

		public int MemberVisibleTimeMax = 9;

		public int MemberFontMax = 30;

		// ---- 読み込み

		private string GetConfFile()
		{
			return Path.Combine(Program.selfDir, Path.GetFileNameWithoutExtension(Program.selfFile) + ".conf");
		}

		public void Load()
		{
			if (File.Exists(GetConfFile()) == false)
				return;

			string[] lines = File.ReadAllLines(GetConfFile(), StringTools.ENCODING_SJIS);
			lines = RemoveComments(lines);
			int c = 0;

			// ----

			this.MessageTextFontFamily = lines[c++];
			this.MessageTextFontSize = int.Parse(lines[c++]);
			this.MessageText_H = int.Parse(lines[c++]);

			this.RemarksTextMaxLength = int.Parse(lines[c++]);
			this.RemarksTextClearPct = int.Parse(lines[c++]);

			this.MemberVisibleTimeMax = int.Parse(lines[c++]);

			this.MemberFontMax = int.Parse(lines[c++]);

			// 新しい項目_ここへ追加..

			// ----
		}

		private string[] RemoveComments(string[] lines)
		{
			List<string> dest = new List<string>();

			foreach (string line in lines)
				if (line != "" && line[0] != ';')
					dest.Add(line);

			return dest.ToArray();
		}
	}
}
