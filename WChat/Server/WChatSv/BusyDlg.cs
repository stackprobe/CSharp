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
			this.BackColor = Color.FromArgb(16, 0, 0);
			//this.BackColor = Color.Black;
			//this.AdjustToImage(); // move to _Shown
		}

		private void AdjustToImage()
		{
			int w = this.MainPic.Width - this.MainPic.Image.Width;
			int h = this.MainPic.Height - this.MainPic.Image.Height;

			this.MainPic.Anchor = AnchorStyles.Left | AnchorStyles.Top;

			this.MainPic.Width -= w;
			this.MainPic.Height -= h;

			this.Width -= w;
			this.Height -= h;

#if true
			w = 10;
			h = 10;

			this.MainPic.Left += w;
			this.MainPic.Top += h;
			this.Width += w * 2;
			this.Height += h * 2;
#endif

#if true
			this.Width += 300;
			this.Left -= 150;

			{
				Label l = new Label();

				l.ForeColor = Color.White;
				l.Left = 230;
				l.Top = (this.Height - l.Height) / 2;
				l.Text = "処理中です。お待ち下さい...";
				l.Width = 200;

				this.Controls.Add(l);
			}
#endif

#if false
			this.Width = Screen.PrimaryScreen.Bounds.Width;
			this.MainPic.Left = (this.Width - this.MainPic.Width) / 2;
			this.Left = 0;

			this.Height += 40;
			this.MainPic.Top = (this.Height - this.MainPic.Height) / 2;
			this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
#endif
		}

		private void BusyDlg_Shown(object sender, EventArgs e)
		{
			this.AdjustToImage();
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

			if (this.MT_Count < 10)
				return;

			if (this.D_Timer(this.MT_Count))
			{
				this.MT_Enabled = false;
				this.Close();
			}
		}
	}
}
