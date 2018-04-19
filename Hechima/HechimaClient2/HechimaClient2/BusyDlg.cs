using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;
using System.Security.Permissions;
using System.Threading;

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

		public delegate void Perform_d();

		private Thread _th;

		public BusyDlg(Perform_d d_operation)
		{
			_th = new Thread((ThreadStart)delegate()
			{
				d_operation();
			});
			_th.Start();

			InitializeComponent();
		}

		private void BusyDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void BusyDlg_Shown(object sender, EventArgs e)
		{
			this.MainTimer.Enabled = true;
		}

		private void BusyDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MainTimer.Enabled = false;
		}

		private long mtCount;
		private string mtJunsuke;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (5L < this.mtCount && _th.IsAlive == false)
			{
				this.MainTimer.Enabled = false;
				this.Close();
				return;
			}
			if (this.mtCount == 0L)
				this.mtJunsuke = this.Junsuke.Text;
			else
				this.Junsuke.Text = this.mtJunsuke.Insert((int)(this.mtCount % (this.mtJunsuke.Length + 1)), " ");

			this.mtCount++;
		}

		private void Junsuke_Click(object sender, EventArgs e)
		{
			// noop
		}
	}
}
