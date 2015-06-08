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
	public partial class BusyDlg : Form
	{
		// ---- ALT_F4 抑止 ----

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
				return;

			base.WndProc(ref m);
		}

		// ----

		public delegate bool Timer_d(long count);
		private Timer_d D_Timer;
		
		public BusyDlg(Timer_d d_timer)
		{
			this.D_Timer = d_timer;

			InitializeComponent();
		}

		private void BusyDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void BusyDlg_Shown(object sender, EventArgs e)
		{
			this.MT_Enabled = true;			
		}

		private bool MT_Enabled;
		private long MT_Count;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MT_Enabled == false)
				return;

			this.MT_Count++;

			{
				Image img = this.MainPic.Image;
				img.RotateFlip(RotateFlipType.Rotate90FlipNone);
				this.MainPic.Image = img;
			}

			if (this.MT_Count < 5)
				return;

			if (this.D_Timer(this.MT_Count))
			{
				this.MT_Enabled = false;
				this.Close();
			}
		}
	}
}
