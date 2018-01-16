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

			this.TaskTrayIcon.Icon = Gnd.IGreen;
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

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MTEnabled == false || this.MTBusy)
				return;

			this.MTBusy = true;

			try
			{
				int mouseX = Cursor.Position.X;
				int mouseY = Cursor.Position.Y;

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

					if (this.MouseStayMillis < 20000)
					{
						nextIcon = Gnd.IGreen;
					}
					else if (this.MouseStayMillis < 30000)
					{
						nextIcon = Gnd.IYellow;
					}
					else if (this.MouseStayMillis < 33000)
					{
						nextIcon = Gnd.IRed;
					}
					else
					{
						nextIcon = Gnd.IRed;

						// TODO

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
