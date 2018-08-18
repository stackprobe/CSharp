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

			Gnd.LoadConf();

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
		private Queue<Action> MTActions = new Queue<Action>();
		private int ElapsedMillis = 0;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MTEnabled == false)
				return;

			if (1 <= this.MTActions.Count)
			{
				this.MTActions.Dequeue()();
				return;
			}
			this.ElapsedMillis += this.MainTimer.Interval;

			if (Gnd.WakeupPeriodMillis <= this.ElapsedMillis)
			{
				this.ElapsedMillis = 0;

				this.MTActions.Enqueue(() => Win32.SetThreadExecutionState(Win32.ExecutionState.ES_SYSTEM_REQUIRED));
				this.MTActions.Enqueue(() => Win32.SetThreadExecutionState(Win32.ExecutionState.ES_DISPLAY_REQUIRED));
			}
		}
	}
}
