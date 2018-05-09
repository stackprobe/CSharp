using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Charlotte.Tools
{
	public class ProcessTools
	{
		public enum WindowStyle_e
		{
			INVISIBLE = 1,
			MINIMIZED,
			NORMAL,
		};

		public static Process Start(string file, string args, string workingDir = "", WindowStyle_e winStyle = WindowStyle_e.INVISIBLE)
		{
			ProcessStartInfo psi = new ProcessStartInfo();

			psi.FileName = file;
			psi.Arguments = args;
			psi.WorkingDirectory = workingDir; // 既定値 == ""

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

			return Process.Start(psi);
		}

		public static string[] Batch(string[] commands, string workingDir = "", WindowStyle_e winStyle = WindowStyle_e.INVISIBLE)
		{
			using (WorkingDir wd = new WorkingDir())
			{
				string batFile = wd.MakePath() + ".bat";
				string outFile = wd.MakePath() + ".out";
				string callBatFile = wd.MakePath() + ".bat";

				File.WriteAllLines(batFile, commands, StringTools.ENCODING_SJIS);
				File.WriteAllText(callBatFile, "> " + outFile + " CALL " + batFile, StringTools.ENCODING_SJIS);

				Start("cmd", "/c " + callBatFile, workingDir, winStyle).WaitForExit();

				return File.ReadAllLines(outFile, StringTools.ENCODING_SJIS);
			}
		}
	}
}
