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

		public static PostBox<string> StatusBox = new PostBox<string>(null);

		private Thread _th;

		public BusyDlg(Thread th)
		{
			_th = th;

			InitializeComponent();
		}

		private int Status_Base_T;
		private int Status_Base_H;

		private void BusyDlg_Load(object sender, EventArgs e)
		{
			this.Status_Base_T = this.Status.Top;
			this.Status_Base_H = this.Status.Height;

			this.Elapsed.Text = "";
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
				if (Cancelled)
				{
					Common.StartKillAndBoot();
					MT_Enabled = false;
					return;
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

				//if (2 < MT_Count) // ちゃんと見えるように...
				//if (2 < MT_Count && MT_Count % 2 == 1) // ちゃんと見えるように...
				{
					string status = StatusBox.Get();

					if (status != null)
					{
						this.Status.Text = status;
						this.Status.Top = Status_Base_T - (this.Status.Height - this.Status_Base_H) / 2;
					}
				}

				if (MT_Count % 10 == 0)
				{
					long t = MT_Count / 10;
					long m = t / 60;
					long s = t % 60;

					this.Elapsed.Text = "経過時間 ... だいたい " + m + " 分 " + s + " 秒 くらい";
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
