using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Charlotte
{
	public class Logger
	{
		private static bool _wrote = false;

		public static void WriteLine(object obj)
		{
			try
			{
				using (StreamWriter sw = new StreamWriter(@"C:\temp\FatCalc.log", _wrote, Encoding.UTF8))
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
	}
}
