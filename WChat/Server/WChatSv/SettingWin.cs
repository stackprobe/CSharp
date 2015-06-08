using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte
{
	public partial class SettingWin : Form
	{
		public SettingWin()
		{
			InitializeComponent();

			this.DoLoad();
		}

		private void SettingWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			try
			{
				this.DoSave();
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					"" + ex,
					"設定を保存できません",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
					);
			}
		}

		private void DoLoad()
		{
			this.ChatSvPort.Text = "" + Gnd.I.ChatSvPort;
			this.RevServerPort.Text = "" + Gnd.I.RevServerPort;
			this.ProcMode.SelectedIndex = (int)Gnd.I.ProcMode;
		}

		private void DoSave()
		{
			int chatSvPort = int.Parse(this.ChatSvPort.Text);
			int revServerPort = int.Parse(this.RevServerPort.Text);
			ProcessMan.Mode_e procMode = (ProcessMan.Mode_e)this.ProcMode.SelectedIndex;

			if (
				chatSvPort < 1 || 65535 < chatSvPort ||
				revServerPort < 1 || 65535 < revServerPort
				)
				throw new Exception("ポート番号は 1～65535 の範囲で指定して下さい。");

			if (chatSvPort == revServerPort)
				throw new Exception("ポート番号が重複しています。");

			Gnd.I.ChatSvPort = chatSvPort;
			Gnd.I.RevServerPort = revServerPort;
			Gnd.I.ProcMode = procMode;
		}
	}
}
