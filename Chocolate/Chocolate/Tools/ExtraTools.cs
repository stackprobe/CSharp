using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Charlotte.Tools
{
	public class ExtraTools
	{
		public static void AntiWindowsDefenderSmartScreen()
		{
			// Chocolate.dll と同じ場所でないと実行出来ないので、CheckAloneExe()は不要と判断した。@ 201x.x.x

			ProcMain.WriteLog("awdss_1");

			if (Is初回起動())
			{
				ProcMain.WriteLog("awdss_2");

				foreach (string exeFile in Directory.GetFiles(ProcMain.SelfDir, "*.exe", SearchOption.TopDirectoryOnly))
				{
					try
					{
						ProcMain.WriteLog("awdss_exeFile: " + exeFile);

						if (StringTools.EqualsIgnoreCase(exeFile, ProcMain.SelfFile))
						{
							ProcMain.WriteLog("awdss_self_noop");
						}
						else
						{
							byte[] exeData = File.ReadAllBytes(exeFile);
							File.Delete(exeFile);
							File.WriteAllBytes(exeFile, exeData);
						}
						ProcMain.WriteLog("awdss_OK");
					}
					catch (Exception e)
					{
						ProcMain.WriteLog(e);
					}
				}
				ProcMain.WriteLog("awdss_3");
			}
			ProcMain.WriteLog("awdss_4");
		}

		private static bool? _初回起動 = null;

		public static bool Is初回起動()
		{
			if (_初回起動 == null)
			{
				string sigFile = ProcMain.SelfFile + ".awdss.sig";

				_初回起動 = File.Exists(sigFile) == false;

				File.WriteAllBytes(sigFile, BinTools.EMPTY);
			}
			return _初回起動.Value;
		}

		public static void PostShown(Form f)
		{
			List<Control.ControlCollection> controlTable = new List<Control.ControlCollection>();

			controlTable.Add(f.Controls);

			for (int index = 0; index < controlTable.Count; index++)
			{
				foreach (Control control in controlTable[index])
				{
					GroupBox gb = control as GroupBox;

					if (gb != null)
					{
						controlTable.Add(gb.Controls);
					}
					TextBox tb = control as TextBox;

					if (tb != null)
					{
						if (tb.ContextMenuStrip == null)
						{
							ContextMenuStrip menu = new ContextMenuStrip();

							//menu.Items.Add("noop", null, (sender, e) => { });

							tb.ContextMenuStrip = menu;
						}
					}
				}
			}
		}
	}
}
