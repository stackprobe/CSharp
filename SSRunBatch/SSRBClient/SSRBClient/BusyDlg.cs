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

		public delegate void Operation_d();

		public static void Perform(Form parent, Operation_d operation)
		{
			using (BusyDlg f = new BusyDlg(operation))
			{
				//parent.Visible = false;

				f.ShowDialog();

				//parent.Visible = true;

				if (f.Ex != null)
				{
					throw new RelayException(f.Ex);
				}
			}
		}

		public Exception Ex = null;

		private Thread Th;

		public BusyDlg(Operation_d operation)
		{
			this.Th = new Thread((ThreadStart)delegate
			{
				try
				{
					operation();
				}
				catch (Exception e)
				{
					this.Ex = e;
				}
			});

			this.Th.Start();

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

		private long MTCount;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (5 < this.MTCount && this.Th.Join(0)) // 0.5 sec <
			{
				this.MainTimer.Enabled = false;
				this.Close();
				return;
			}
			this.MTCount++;
		}
	}
}
