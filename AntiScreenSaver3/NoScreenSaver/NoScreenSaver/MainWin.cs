using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Diagnostics;

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

			Ground.LoadConf();

			if (Ground.MonitorKeyboard)
				this.KeysMon = new KeysMon();

			this.TaskTrayIcon.Icon = Ground.Icons[0];
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

		private void CloseWindow()
		{
			this.MTEnabled = false;
			this.Close();
		}

		private void EndProcMenuItem_Click(object sender, EventArgs e)
		{
			this.RequestEndProc = true;
		}

		private KeysMon KeysMon;

		private bool MTEnabled;
		private bool MTBusy;
		private long MTCount;

		private int MouseStayMillis = 0;
		private int LastMouse_X;
		private int LastMouse_Y;

		private bool RequestEndProc = false;

		private Process ProcTimeoutBatch = null;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MTEnabled == false || this.MTBusy)
				return;

			this.MTBusy = true;

			try
			{
				if (this.ProcTimeoutBatch != null)
				{
					if (this.ProcTimeoutBatch.HasExited)
						this.ProcTimeoutBatch = null;

					return;
				}
				if (Program.StopRunEv.WaitOne(0) || this.RequestEndProc)
				{
					this.CloseWindow();
					return;
				}
				if (Ground.MonitorKeyboard)
					this.KeysMon.DoCheck();

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

				if (Ground.MonitorKeyboard && this.KeysMon.IsTouched())
					this.MouseStayMillis = 0;

				{
					Icon nextIcon;

					if (this.MouseStayMillis < Ground.MouseStayTimeoutMillis)
					{
						nextIcon = Ground.Icons[(10 * this.MouseStayMillis) / Ground.MouseStayTimeoutMillis];
					}
					else
					{
						nextIcon = Ground.Icons[10];

						try
						{
							ProcessStartInfo psi = new ProcessStartInfo();

							psi.FileName = "cmd";
							psi.Arguments = "/c " + Ground.TimeoutBatchFile;
							psi.CreateNoWindow = true;
							psi.UseShellExecute = false;

							this.ProcTimeoutBatch = Process.Start(psi);
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex + "", Program.APP_TITLE + " / Start Timeout-Batch Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

							this.ProcTimeoutBatch = null;
						}

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
