using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class DebugTools
	{
		private static FileStream LogStrm;

		public static void WriteLog(string line)
		{
			if (LogStrm == null)
				LogStrm = new FileStream(@"C:\temp\Module.log", FileMode.Create, FileAccess.Write);

			FileTools.Write(
				LogStrm,
				StringTools.ENCODING_SJIS.GetBytes("[" + DateTimeTools.GetCommonString(DateTime.Now) + "] " + line + "\r\n")
				);
		}
	}
}
