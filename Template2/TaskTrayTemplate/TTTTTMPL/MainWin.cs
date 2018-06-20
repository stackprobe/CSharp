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
			// -- 0001

			// ----

			this.Visible = false;
			this.TaskTrayIcon.Visible = true;
			this.MTEnabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MTEnabled = false;
			this.TaskTrayIcon.Visible = false;

			// ----

			// -- 9999
		}

		private void BeforeDialog()
		{
			this.MTEnabled = false;
			this.TaskTrayIcon.Visible = false;
		}

		private void AfterDialog()
		{
			this.TaskTrayIcon.Visible = true;
			this.MTEnabled = true;
		}

		private void CloseWindow()
		{
			this.MTEnabled = false;
			this.TaskTrayIcon.Visible = false;

			// ----

			// -- 9000

			// ----

			this.Close();
		}

		private bool MTEnabled;
		private bool MTBusy;
		private long MTCount;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MTEnabled == false || this.MTBusy)
				return;

			this.MTBusy = true;

			try
			{
				if (this.MTCount == 30) // 3 sec
				{
					this.CloseWindow();
					return;
				}
			}
			catch (Exception ex)
			{
				ProcMain.WriteLog(ex);
			}
			finally
			{
				this.MTBusy = false;
				this.MTCount++;
			}
		}

		private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.CloseWindow();
		}
	}
}
