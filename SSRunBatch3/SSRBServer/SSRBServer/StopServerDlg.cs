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

		public StopServerDlg()
		{
			InitializeComponent();
		}

		private void StopServerDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void StopServerDlg_Shown(object sender, EventArgs e)
		{
			this.MTEnabled = true;
		}

		private void StopServerDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void StopServerDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MTEnabled = false;
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
				Gnd.I.BatchServer.SockServer.Stop();

				if (5 < this.MTCount && Gnd.I.BatchServer.SockServer.IsRunning() == false) // 0.5 sec <
				{
					Gnd.I.BatchServer = null;

					this.MTEnabled = false;
					this.Close();
					return;
				}
			}
			finally
			{
				this.MTBusy = false;
				this.MTCount++;
			}
		}

		private void BtnAbandon_Click(object sender, EventArgs e)
		{
			Gnd.I.AbandonCurrentRunningBatchFlag = true;
		}
	}
}
