using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;

namespace Charlotte
{
	public partial class MainWin : Form
	{
		#region ALT_F4 抑止

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
				return;

			base.WndProc(ref m);
		}

		#endregion

		public MainWin()
		{
			InitializeComponent();
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.Visible = false;
			this.TaskTrayIcon.Visible = true;
			this.UIRefresh();
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.TaskTrayIcon.Visible = false;
		}

		private void UIRefresh()
		{
			this.TaskTrayIcon.Text = "SSRBServer2 / ポート番号 : " + Gnd.PortNo;
		}

		private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ポート番号PToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.TaskTrayIcon.Visible = false;

			Gnd.StopServer();

			using (SettingDlg f = new SettingDlg())
			{
				f.ShowDialog();
			}
			Gnd.Save(Gnd.SettingFile);

			Gnd.StartServer();

			this.UIRefresh();
			this.TaskTrayIcon.Visible = true;
		}

		private void AbandonCurrentRunningBatchParentMenuItem_Click(object sender, EventArgs e)
		{
			// noop
		}

		private void AbandonCurrentRunningBatchMenuItem_Click(object sender, EventArgs e)
		{
			SSRBServerProc.AbandonCurrentRunningBatch();
		}
	}
}
