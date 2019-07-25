using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Charlotte.Tools;
using System.Security.Permissions;

namespace Charlotte.Chocomint.Dialogs
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

		public ThreadEx Th = null;
		public Action PostShown = () => { };

		// <---- prm

		public BusyDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		private void BusyDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void BusyDlg_Shown(object sender, EventArgs e)
		{
			this.EndedCount = 0;

			this.PostShown();
			ChocomintCommon.DlgCommonPostShown(this);
		}

		private void BusyDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void BusyDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.EndedCount = -1; // 2bs
		}

		private int EndedCount = -1;
		private double ProgressRate = 0.0;
		private int PRCount = 0;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.EndedCount == -1)
				return;

			if (this.EndedCount == 0)
			{
				if (this.Th == null || this.Th.IsEnded())
				{
					this.EndedCount = 1;
					this.ProgressRate = 1.0;
					this.ProgressRateChanged();
					return;
				}
				this.PRCount++;
				this.PRCount %= 10;

				if (this.PRCount == 1)
				{
					this.ProgressRate -= 0.9;
					this.ProgressRate *= 0.9;
					this.ProgressRate += 0.9;
					this.ProgressRateChanged();
				}
				return;
			}
			if (10 < ++this.EndedCount)
			{
				this.EndedCount = -1;
				this.Close();
			}
		}

		private void ProgressRateChanged()
		{
			int value = DoubleTools.ToInt(this.ProgressRate * 1000.0);

			if (this.ProgressBar.Value != value)
				this.ProgressBar.Value = value;
		}
	}
}
