using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

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
			CallAllControl(f.Controls, control =>
			{
				TextBox tb = control as TextBox;

				if (tb != null)
				{
					if (tb.ContextMenuStrip == null)
					{
						ToolStripMenuItem item = new ToolStripMenuItem();

						item.Text = "項目なし";
						item.Enabled = false;

						ContextMenuStrip menu = new ContextMenuStrip();

						menu.Items.Add(item);

						tb.ContextMenuStrip = menu;
					}
				}
			});
		}

		public static void CallAllControl(Control.ControlCollection controls, Action<Control> rtn)
		{
			foreach (Control control in controls)
			{
				rtn(control);

				GroupBox gb = control as GroupBox;

				if (gb != null)
				{
					CallAllControl(gb.Controls, rtn);
				}
				TabControl tc = control as TabControl;

				if (tc != null)
				{
					foreach (TabPage tp in tc.TabPages)
					{
						CallAllControl(tp.Controls, rtn);
					}
				}
				SplitContainer sc = control as SplitContainer;

				if (sc != null)
				{
					CallAllControl(sc.Panel1.Controls, rtn);
					CallAllControl(sc.Panel2.Controls, rtn);
				}
			}
		}

		public static void SetEnabledDoubleBuffer(Control control)
		{
			control.GetType().InvokeMember(
				"DoubleBuffered",
				BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
				null,
				control,
				new object[] { true }
				);
		}

		private static string EasyBkDestDir = null;

		public static void EasyBackupPath(string path)
		{
			if (EasyBkDestDir == null)
				EasyBkDestDir = MakeFreeDir();

			string destPath = Path.Combine(EasyBkDestDir, Path.GetFileName(path));
			destPath = ToCreatablePath(destPath);

			if (File.Exists(path))
				File.Copy(path, destPath);
			else if (Directory.Exists(path))
				FileTools.CopyDir(path, destPath);
		}

		public static string MakeFreeDir()
		{
			for (int c = 1; ; c++)
			{
				string dir = @"C:\" + c;

				if (Accessible(dir) == false)
				{
					FileTools.CreateDir(dir);
					return dir;
				}
			}
		}

		public static string ToCreatablePath(string path)
		{
			if (Accessible(path))
			{
				String prefix = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
				String suffix = Path.GetExtension(path);

				for (int c = 2; ; c++)
				{
					path = prefix + "~" + c + suffix;

					if (Accessible(path) == false)
						break;
				}
			}
			return path;
		}

		public static bool Accessible(string path)
		{
			return File.Exists(path) || Directory.Exists(path);
		}
	}
}
