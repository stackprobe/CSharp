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
	public partial class StartServerDlg : Form
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

		public StartServerDlg()
		{
			InitializeComponent();
		}

		private void StartServerDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void StartServerDlg_Shown(object sender, EventArgs e)
		{
			this.MainTimer.Enabled = true;
		}

		private void StartServerDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MainTimer.Enabled = false;
		}

		private long MT_Count;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				if (20 <= this.MT_Count) // 2 sec <=
				{
					Gnd.I.StartServer();

					this.MainTimer.Enabled = false;
					this.Close();
					return;
				}
				if (this.MT_Count % 5 == 0) // per 0.5 sec
				{
					SSRBServerProc.StopTSRServer();
					SSRBServerProc.StopServer();
				}
			}
			finally
			{
				this.MT_Count++;
			}
		}
	}
}
