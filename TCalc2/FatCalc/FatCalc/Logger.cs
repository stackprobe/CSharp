using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	public class Logger
	{
		private static bool _wrote = false;

		public static void WriteLine(object obj)
		{
			if (Gnd.DebugMode == false)
				return;

			try
			{
				using (StreamWriter sw = new StreamWriter(LogFile, _wrote, Encoding.UTF8))
				{
					sw.WriteLine("[" + DateTime.Now + "] " + obj);
				}
				_wrote = true;
			}
			catch (Exception e)
			{
				MessageBox.Show("" + e);
			}
		}

		private static string LogFile
		{
			get
			{
				return Path.Combine(BootTools.SelfDir, Path.GetFileNameWithoutExtension(BootTools.SelfFile) + ".log");
			}
		}
	}
}
