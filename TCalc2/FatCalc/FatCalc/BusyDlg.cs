using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Threading;
using Charlotte.Tools;

namespace Charlotte
{
	public partial class BusyDlg : Form
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

		private Thread _th;

		public BusyDlg(Thread th)
		{
			_th = th;

			InitializeComponent();
		}

		private void BusyDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void BusyDlg_Shown(object sender, EventArgs e)
		{
			MT_Enabled = true;
		}

		private void BusyDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void BusyDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			MT_Enabled = false;
		}

		private bool MT_Enabled;
		private bool MT_Busy;
		private long MT_Count;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (MT_Enabled == false || MT_Busy)
				return;

			MT_Busy = true;

			try
			{
				if (MT_Count % 10 == 0)
				{
					long t = MT_Count / 10;
					long m = t / 60;
					long s = t % 60;

					this.Message.Text = "経過時間 ... だいたい " + m + " 分 " + s + " 秒 くらい";
				}
				if (Cancelled)
				{
					MT_Enabled = false;
					Common.StartKillAndBoot();
				}
				if (5 < MT_Count)
				{
					if (_th.IsAlive == false)
					{
						MT_Enabled = false;
						this.Close();
						return;
					}
				}
			}
			finally
			{
				MT_Busy = false;
				MT_Count++;
			}
		}

		private bool Cancelled = false;

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			Cancelled = true;
		}

		private void ChkBoxCancel_CheckedChanged(object sender, EventArgs e)
		{
			this.BtnCancel.Enabled = this.ChkBoxCancel.Checked;
		}
	}
}
