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
	public partial class StopServerDlg : Form
	{
		#region ALT_F4 抑止

		private bool XPressed = false;

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

		public StopServerDlg()
		{
			InitializeComponent();

			this.XLabel.Visible = false;
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

		private long HideXPressedMessage_MTCount;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MTEnabled == false)
				return;

			if (5 < this.MTCount && Gnd.TSRServerProc.HasExited && Gnd.ServerProc.HasExited) // 0.5 sec <
			{
				this.MTEnabled = false;
				this.Close();
				return;
			}

			{
				bool flag = 300 < this.MTCount; // 30 sec <

				if (this.BtnForceStop.Enabled != flag)
					this.BtnForceStop.Enabled = flag;
			}

			if (this.XPressed)
			{
				this.XPressed = false;
				this.HideXPressedMessage_MTCount = this.MTCount + 20;
			}

			{
				bool flag = this.MTCount < this.HideXPressedMessage_MTCount;

				if (this.XLabel.Visible != flag)
					this.XLabel.Visible = flag;
			}

			this.MTCount++;
		}

		private void BtnForceStop_Click(object sender, EventArgs e)
		{
			SSRBServerProc.AbandonCurrentRunningBatch();
		}
	}
}
