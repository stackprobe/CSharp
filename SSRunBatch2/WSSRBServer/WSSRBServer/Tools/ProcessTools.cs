﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Charlotte.Tools
{
	public static class ProcessTools
	{
		/// <summary>
		/// この値をSSRBServerに渡す。
		/// SSRBServerと同じ定義であること。
		/// </summary>
		public enum WindowStyle_e
		{
			INVISIBLE = 1,
			MINIMIZED,
			NORMAL,
		};

		public static Process Start(string file, string args, string workingDir = "", WindowStyle_e winStyle = WindowStyle_e.INVISIBLE)
		{
			Utils.PostMessage("Command: " + file + " " + args); // app固有

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
	}
}
