using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Charlotte.Tools
{
	// バッチなので、"%" -> "%%"

	public static class ProcessTools
	{
		public static void runOnBatch(string line, string dir = null, WindowStyle_e winStyle = WindowStyle_e.INVISIBLE)
		{
			runOnBatch(new string[] { line }, dir, winStyle);
		}

		public static void runOnBatch(string[] lines, string dir = null, WindowStyle_e winStyle = WindowStyle_e.INVISIBLE)
		{
			using (WorkingDir wd = WorkingDir.root.create())
			{
				string batch = wd.makePath() + ".bat";

				File.WriteAllLines(batch, lines, StringTools.ENCODING_SJIS);

				if (dir == null)
				{
					dir = wd.makePath();
					Directory.CreateDirectory(dir);
				}

#if true
				start("cmd.exe", "/C " + batch, dir, winStyle).WaitForExit();
#else // old
				{
					ProcessStartInfo psi = new ProcessStartInfo();

					psi.FileName = "cmd.exe";
					psi.Arguments = "/C " + batch;
					psi.CreateNoWindow = true;
					psi.UseShellExecute = false;
					psi.WorkingDirectory = dir;

					Process.Start(psi).WaitForExit();
				}
#endif
			}
		}

		public enum WindowStyle_e
		{
			INVISIBLE, // 0
			MINIMIZED, // 1
			NORMAL, // 2
		};

		public static Process start(string file, string args, string workingDir = null, WindowStyle_e winStyle = WindowStyle_e.INVISIBLE)
		{
			ProcessStartInfo psi = new ProcessStartInfo();

			psi.FileName = file;
			psi.Arguments = args;

			switch (winStyle)
			{
				case WindowStyle_e.INVISIBLE:
					psi.CreateNoWindow = true;
					psi.UseShellExecute = false;
					break;

				case WindowStyle_e.MINIMIZED:
					psi.CreateNoWindow = false;
					psi.UseShellExecute = true;
					psi.WindowStyle = ProcessWindowStyle.Minimized;
					break;

				case WindowStyle_e.NORMAL:
					break;

				default:
					throw null;
			}
			if (workingDir != null)
				psi.WorkingDirectory = workingDir; // WorkingDirectoryの説明 == 既定値は、空の文字列 ("") です。

			return Process.Start(psi);
		}
	}
}
