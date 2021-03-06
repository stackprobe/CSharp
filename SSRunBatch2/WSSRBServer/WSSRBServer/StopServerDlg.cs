﻿using System;
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
			this.MainTimer.Enabled = true;
		}

		private void StopServerDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MainTimer.Enabled = false;
		}

		private long MT_Count;

		private Utils.PeriodicPerform StopServerPP = new Utils.PeriodicPerform(50, SSRBServerProc.StopServer); // per 5 sec
		private Utils.PeriodicPerform StopTSRServerPP = new Utils.PeriodicPerform(50, SSRBServerProc.StopTSRServer); // per 5 sec

		public static int BeforeCloseWaitCounter = 0;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				if (Gnd.I.ServerProc.HasExited == false)
				{
					this.StopServerPP.Perform();
					return;
				}
				if (Gnd.I.TSRServerProc.HasExited == false)
				{
					this.StopTSRServerPP.Perform();
					return;
				}
				if (0 < BeforeCloseWaitCounter)
				{
					BeforeCloseWaitCounter--;
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

		private void BtnAbandon_Click(object sender, EventArgs e)
		{
			SSRBServerProc.AbandonCurrentRunningBatch();
		}
	}
}
