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

		public static BusyDlg I;

		public static void Perform(Perform_d dPerform)
		{
			using (BusyDlg f = new BusyDlg(dPerform))
			{
				I = f;
				f.D_Perform = dPerform;
				f.ShowDialog();
				I = null;
			}
		}

		public delegate void Perform_d();
		private Perform_d D_Perform;

		public BusyDlg(Perform_d dPerform)
		{
			this.D_Perform = dPerform;

			InitializeComponent();
		}

		private void BusyDlg_Load(object sender, EventArgs e)
		{
			this.BackColor = Color.Black;
			//this.AdjustToImage(); // move to _Shown
		}

		private Label MessageLabel;

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
			this.Left -= w;
			this.Top -= h;
#endif

#if true
			this.Width += 400;
			this.Left -= 200;
			//this.Width += 300;
			//this.Left -= 150;

			{
				Label l = new Label();

				l.ForeColor = Color.White;
				l.Left = 230;
				l.Top = (this.Height - l.Height) / 2;
				l.Text = "メッセージを準備しています...";
				l.Width = 350;
				//l.Width = 200;
				l.TextAlign = ContentAlignment.MiddleLeft;

				//l.BackColor = Color.Blue; // test

				this.Controls.Add(l);

				this.MessageLabel = l;
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
		private bool MT_Busy;
		private long MT_Count;

		private Thread PerformTh;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MT_Enabled == false || this.MT_Busy)
				return;

			this.MT_Busy = true;

			try
			{
				{
					Image img = this.MainPic.Image;
					img.RotateFlip(RotateFlipType.Rotate90FlipNone);
					this.MainPic.Image = img;
				}

				{
					string message = this.NextMessage();

					if (message != null)
					{
						this.MessageLabel.Text = message;
						return;
					}
				}

				if (this.PerformTh == null)
				{
					this.PerformTh = new Thread((ThreadStart)delegate
					{
						Thread th = new Thread((ThreadStart)delegate
						{
							try
							{
								this.D_Perform();
							}
							catch (Exception ex)
							{
								SystemTools.WriteLog(ex);
							}
						});
						th.Start();
						Thread.Sleep(500);
						th.Join();
					});
					this.PerformTh.Start();
				}
				if (this.PerformTh.IsAlive == false)
				{
					this.MT_Enabled = false;
					this.Close();
					return;
				}
			}
			finally
			{
				this.MT_Busy = false;
				this.MT_Count++;
			}
		}

		private object Messages_SYNCROOT = new object();
		private QueueData<string> Messages = new QueueData<string>();

		public void SetMessage(string message)
		{
			lock (Messages_SYNCROOT)
			{
				this.Messages.Add(message);
			}
		}

		private string NextMessage()
		{
			lock (Messages_SYNCROOT)
			{
				return this.Messages.Poll(null);
			}
		}
	}
}
