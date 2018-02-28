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

			this.MinimumSize = this.Size;
		}

		private void StopServerDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void StopServerDlg_Shown(object sender, EventArgs e)
		{
			this.MainTimer.Enabled = true;
		}

		private void StopServerDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MainTimer.Enabled = false;
		}

		private long MT_Count;
		private long HideXPressedMessageMTCount;

		private Utils.PeriodicPerform StopServerPP = new Utils.PeriodicPerform(150, SSRBServerProc.StopServer); // per 15 sec
		private Utils.PeriodicPerform StopTSRServerPP = new Utils.PeriodicPerform(150, SSRBServerProc.StopTSRServer); // per 15 sec

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				if (this.XPressed)
				{
					this.XPressed = false;
					this.HideXPressedMessageMTCount = this.MT_Count + 20;
				}

				{
					bool flag = this.MT_Count < this.HideXPressedMessageMTCount;

					if (this.XLabel.Visible != flag)
						this.XLabel.Visible = flag;
				}

				if (Gnd.I.ServerProc.HasExited == false)
				{
					this.StopServerPP.Kick();
					this.SetBtnAbandonEnabled(300 < this.MT_Count); // 30 sec <
					return;
				}
				this.SetBtnAbandonEnabled(false);

				if (Gnd.I.TSRServerProc.HasExited == false)
				{
					this.StopTSRServerPP.Kick();
					return;
				}
				if (5 < this.MT_Count) // 0.5 sec <
				{
					this.MainTimer.Enabled = false;
					this.Close();
					return;
				}
			}
			finally
			{
				this.MT_Count++;
			}
		}

		private void SetBtnAbandonEnabled(bool flag)
		{
			if (this.BtnAbandon.Enabled != flag)
				this.BtnAbandon.Enabled = flag;
		}

		private void BtnAbandon_Click(object sender, EventArgs e)
		{
			SSRBServerProc.AbandonCurrentRunningBatch();
		}
	}
}
