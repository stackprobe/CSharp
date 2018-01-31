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
	public partial class MainWin : Form
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

		public MainWin()
		{
			InitializeComponent();
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.Visible = false;

			using (new ResourceWin())
			{ }

			Gnd.LoadConf();

			this.TaskTrayIcon.Icon = Gnd.Icons[0];
			this.TaskTrayIcon.Visible = true;
			this.MTEnabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MTEnabled = false;
			this.TaskTrayIcon.Visible = false;
		}

		private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private bool MTEnabled;
		private bool MTBusy;
		private long MTCount;

		private int MouseStayMillis = 0;
		private int LastMouse_X;
		private int LastMouse_Y;

		private int MouseShakeIndex = -1;
		private int MouseShake_X;
		private int MouseShake_Y;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MTEnabled == false || this.MTBusy)
				return;

			this.MTBusy = true;

			try
			{
				int mouseX = Cursor.Position.X;
				int mouseY = Cursor.Position.Y;

#if false
				try // test
				{
					using (System.IO.StreamWriter writer = new System.IO.StreamWriter(@"C:\temp\AntiScreenSaver_Mouse.log", true, Encoding.ASCII))
					{
						writer.WriteLine("[" + DateTime.Now + "] " + mouseX + ", " + mouseY);
					}
				}
				catch
				{ }
#endif

				if (this.MouseShakeIndex != -1)
				{
					if (
						mouseX == this.LastMouse_X &&
						mouseY == this.LastMouse_Y
						)
					{
						if (this.MouseShakeIndex < Gnd.MouseShakeRoute.Count)
						{
							Gnd.XYPoint point = Gnd.MouseShakeRoute[this.MouseShakeIndex];

							Cursor.Position = new Point(
								this.MouseShake_X + point.X,
								this.MouseShake_Y + point.Y
								);

							this.LastMouse_X = Cursor.Position.X;
							this.LastMouse_Y = Cursor.Position.Y;

							this.MouseShakeIndex++;
							return;
						}
						Cursor.Position = new Point(this.MouseShake_X, this.MouseShake_Y);

						this.LastMouse_X = this.MouseShake_X;
						this.LastMouse_Y = this.MouseShake_Y;
					}
					this.MouseShakeIndex = -1;
					return;
				}

				if (
					mouseX == this.LastMouse_X &&
					mouseY == this.LastMouse_Y
					)
				{
					this.MouseStayMillis += this.MainTimer.Interval;
				}
				else
				{
					this.MouseStayMillis = 0;

					this.LastMouse_X = mouseX;
					this.LastMouse_Y = mouseY;
				}

				{
					Icon nextIcon;

					if (this.MouseStayMillis < Gnd.MouseStayTimeoutMillis)
					{
						nextIcon = Gnd.Icons[(10 * this.MouseStayMillis) / Gnd.MouseStayTimeoutMillis];
					}
					else
					{
						nextIcon = Gnd.Icons[10];

						this.MouseShakeIndex = 0;
						this.MouseShake_X = mouseX;
						this.MouseShake_Y = mouseY;

						this.MouseStayMillis = 0;
					}

					if (this.TaskTrayIcon.Icon != nextIcon)
						this.TaskTrayIcon.Icon = nextIcon;
				}
			}
			finally
			{
				this.MTBusy = false;
				this.MTCount++;
			}
		}
	}
}
