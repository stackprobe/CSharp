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

			if (Gnd.MonitorKeyboard)
				this.KeysMon = new KeysMon();

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

		private void CloseWindow()
		{
			this.MTEnabled = false;
			this.Close();
		}

		private void EndProcMenuItem_Click(object sender, EventArgs e)
		{
			this.CloseWindow();
		}

		private KeysMon KeysMon;

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
				if (Program.StopRunEv.WaitOne(0))
				{
					this.CloseWindow();
					return;
				}
				if (Gnd.MonitorKeyboard)
					this.KeysMon.DoCheck();

				int mouseX = Cursor.Position.X;
				int mouseY = Cursor.Position.Y;

				if (this.MouseShakeIndex != -1)
				{
#if !true
					switch (this.MouseShakeIndex)
					{
						case 0:
							Win32.SetThreadExecutionState(Win32.ExecutionState.ES_SYSTEM_REQUIRED);
							break;

						case 1:
							Win32.SetThreadExecutionState(Win32.ExecutionState.ES_DISPLAY_REQUIRED);
							break;

						case 2:
							this.MouseShakeIndex = -1;
							return;

						default:
							throw null; // never
					}
					this.MouseShakeIndex++;
					return;
#else
					if (
						mouseX == this.LastMouse_X &&
						mouseY == this.LastMouse_Y
						)
					{
						if (this.MouseShakeIndex < Gnd.MouseShakeRoute.Count)
						{
							Gnd.XYPoint point = Gnd.MouseShakeRoute[this.MouseShakeIndex];

#if true // CUI -> Win32
							CTools.Perform(string.Format("/P {0} {1}",
								this.MouseShake_X + point.X,
								this.MouseShake_Y + point.Y
								));
#elif true // Win32
							Win32.ClipCursor(null);

							Win32.SetCursorPos(
								this.MouseShake_X + point.X,
								this.MouseShake_Y + point.Y
								);
#else // .NET
							Cursor.Position = new Point(
								this.MouseShake_X + point.X,
								this.MouseShake_Y + point.Y
								);
#endif

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
#endif
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

				if (Gnd.MonitorKeyboard && this.KeysMon.IsTouched())
					this.MouseStayMillis = 0;

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
