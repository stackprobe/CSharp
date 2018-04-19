using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using Charlotte.Tools;

namespace Charlotte
{
	public partial class OnlineDlg : Form
	{
		#region ALT_F4 抑止

		public bool XPressed = false;

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
			{
				this.XPressed = true;
				return;
			}
			base.WndProc(ref m);
		}

		#endregion

		public OnlineDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;

			this.OnlineText.ForeColor = Gnd.setting.OnlineForeColor;
			this.OnlineText.BackColor = Gnd.setting.OnlineBackColor;

			Common.SetTextBoxBorderStyle(this.OnlineText, Gnd.setting.Flat_OnlineText);

			if (Gnd.setting.OnlineDlg_W != -1)
			{
				this.Left = Gnd.setting.OnlineDlg_L;
				this.Top = Gnd.setting.OnlineDlg_T;
				this.Width = Gnd.setting.OnlineDlg_W;
				this.Height = Gnd.setting.OnlineDlg_H;
			}
			if (Gnd.setting.OnlineDlg_Minimized)
				this.WindowState = FormWindowState.Minimized;
		}

		private void OnlineDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void OnlineDlg_Shown(object sender, EventArgs e)
		{
			this.MainTimer.Enabled = true;
		}

		private void OnlineDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MainTimer.Enabled = false;

			if (this.WindowState == FormWindowState.Normal)
			{
				Gnd.setting.OnlineDlg_L = this.Left;
				Gnd.setting.OnlineDlg_T = this.Top;
				Gnd.setting.OnlineDlg_W = this.Width;
				Gnd.setting.OnlineDlg_H = this.Height;
			}
			Gnd.setting.OnlineDlg_Minimized = this.WindowState == FormWindowState.Minimized;
		}

		private void OnlineText_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (Gnd.bgService.RecvedOnlineLines != null)
			{
				List<string> lines = Gnd.bgService.RecvedOnlineLines;
				Gnd.bgService.RecvedOnlineLines = null;

				List<string> dest = new List<string>();

				foreach (string line in lines)
				{
					int dlmPos = line.IndexOf(' ');

					string sTime = line.Substring(0, dlmPos);
					string ident = line.Substring(dlmPos + 1);

					long time = long.Parse(sTime);
					time /= 60;

					//if (time <= 9) // 十分に古いものは表示しない。
					if (time <= Gnd.conf.MemberVisibleTimeMax)
					{
						//sTime = StringTools.zPad((int)time, 4);
						sTime = "" + time;
						dest.Add("[" + sTime + "] " + ident);
					}
				}
				ArrayTools.sort(dest, StringTools.comp);
				dest.Add("# 頭の数値は最終アクセスからの時間です。");
				dest.Add("# 1以上はへちま改を閉じている可能性大");
				dest.Add("# 更新=" + Common.DateTimeToString(DateTimeToSec.Now.getDateTime()));

				this.OnlineText.Text = string.Join("\r\n", dest.ToArray());
			}
		}

		private void 直ちに更新RToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Gnd.bgService.ReqDoRecvOnline();
		}

		private void OnlineText_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 1) // ctrl + a
			{
				this.OnlineText.SelectAll();
				e.Handled = true;
			}
		}
	}
}
