using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Charlotte
{
	public class Gnd
	{
		public static string SettingFile;

		public static void Load(string file)
		{
			if (File.Exists(file) == false)
				return;

			string[] lines = File.ReadAllLines(file, Encoding.UTF8);
			int c = 0;

			PortNo = int.Parse(lines[c++]);
			// 新しい設定項目をここへ追加...
		}

		public static void Save(string file)
		{
			List<string> lines = new List<string>();

			lines.Add("" + PortNo);
			// 新しい設定項目をここへ追加...

			File.WriteAllLines(file, lines, Encoding.UTF8);
		}

		// 設定ここから

		public static int PortNo = 55985;

		// 設定ここまで

		public static Process TSRServerProc;
		public static Process ServerProc;

		public static void StartServer()
		{
			TSRServerProc = SSRBServerProc.StartTSRServer();
			ServerProc = SSRBServerProc.StartServer();
		}

		public static void StopServer()
		{
			using (StopServerDlg f = new StopServerDlg())
			{
				f.ShowDialog();
			}
		}
	}
}
