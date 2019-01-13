using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte
{
	public class Utils
	{
		public static void antiWindowsDefenderSmartScreen()
		{
			WriteLog("awdss_1");

			if (Is初回起動())
			{
				WriteLog("awdss_2");

				foreach (string exeFile in Directory.GetFiles(Program.selfDir, "*.exe", SearchOption.TopDirectoryOnly))
				{
					try
					{
						WriteLog("awdss_exeFile: " + exeFile);

						if (StringTools.equalsIgnoreCase(exeFile, Program.selfFile))
						{
							WriteLog("awdss_self_noop");
						}
						else
						{
							byte[] exeData = File.ReadAllBytes(exeFile);
							File.Delete(exeFile);
							File.WriteAllBytes(exeFile, exeData);
						}
						WriteLog("awdss_OK");
					}
					catch (Exception e)
					{
						WriteLog(e);
					}
				}
				WriteLog("awdss_3");
			}
			WriteLog("awdss_4");
		}

		public static void WriteLog(object message)
		{
			Gnd.Logger.writeLine(message);
		}

		public static bool Is初回起動()
		{
			string sigFile = Program.selfFile + ".awdss.sig";

			if (File.Exists(sigFile))
				return false;

			FileTools.createFile(sigFile);
			return true;
		}
	}
}
