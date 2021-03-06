﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Charlotte.Tools
{
	public static class ProcessTools
	{
		public enum WindowStyle_e
		{
			INVISIBLE = 1,
			MINIMIZED,
			NORMAL,
		};

		public static Process Start(string file, string args, string workingDir = "", WindowStyle_e winStyle = WindowStyle_e.INVISIBLE, Action<ProcessStartInfo> beforeStart = null)
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
			if (beforeStart != null)
				beforeStart(psi);

			return Process.Start(psi);
		}

		public static string[] Batch(string[] commands, string workingDir = "", WindowStyle_e winStyle = WindowStyle_e.INVISIBLE)
		{
			using (WorkingDir wd = new WorkingDir())
			{
				string fileBase = wd.MakePath();
				string batFile = fileBase + "_Run.bat";
				string outFile = fileBase + "_Run.out";
				string callBatFile = fileBase + "_Call.bat";

				File.WriteAllLines(batFile, commands, StringTools.ENCODING_SJIS);
				File.WriteAllText(callBatFile, "> " + outFile + " CALL " + batFile, StringTools.ENCODING_SJIS);

				Start("cmd", "/c " + callBatFile, workingDir, winStyle).WaitForExit();

				return File.ReadAllLines(outFile, StringTools.ENCODING_SJIS);
			}
		}
	}
}
