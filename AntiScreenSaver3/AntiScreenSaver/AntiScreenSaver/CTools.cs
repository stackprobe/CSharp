using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Charlotte
{
	public class CTools
	{
		private static string _CToolsFile = null;

		private static string CToolsFile
		{
			get
			{
				if (_CToolsFile == null)
				{
					string file = "CTools.exe";

					if (File.Exists(file) == false)
						file = @"..\..\..\..\Tools\CTools.exe";

					_CToolsFile = file;
				}
				return _CToolsFile;
			}
		}

		public static void Perform(string args)
		{
			ProcessStartInfo psi = new ProcessStartInfo();

			psi.FileName = CToolsFile;
			psi.Arguments = args;
			psi.CreateNoWindow = true;
			psi.UseShellExecute = false;

			using (Process p = Process.Start(psi))
			{
				p.WaitForExit();
			}
		}
	}
}
