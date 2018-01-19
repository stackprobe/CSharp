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
	public partial class SockServerWaitToStopDlg : Form
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

		public SockServerWaitToStopDlg()
		{
			InitializeComponent();
		}

		private void SockServerWaitToStopDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void SockServerWaitToStopDlg_Shown(object sender, EventArgs e)
		{
			this.MTEnabled = true;
		}

		private bool MTEnabled;
		private long MTCount;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MTEnabled == false)
				return;

			if (5 < this.MTCount && Gnd.BatchServer.SockServer.IsRunning() == false) // 0.5 sec <
			{
				this.MTEnabled = false;
				this.Close();
				return;
			}
			if (300 < this.MTCount) // 30 sec <
			{
				Gnd.AbandonCurrentRunningBatchFlag = true;
			}
			this.MTCount++;
		}
	}
}
