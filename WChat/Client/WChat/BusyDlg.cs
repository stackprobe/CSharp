using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
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

		public static BusyDlg I;

		public static void Perform(EventCenter.Event_d d_event)
		{
			using (BusyDlg f = new BusyDlg())
			{
				I = f;
				f.D_Event = d_event;
				f.ShowDialog();
				I = null;
			}
		}

		private EventCenter.Event_d D_Event;

		public BusyDlg()
		{
			InitializeComponent();
		}

		private void BusyDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void BusyDlg_Shown(object sender, EventArgs e)
		{
			new Thread((ThreadStart)delegate
			{
				Thread th = new Thread((ThreadStart)delegate
				{
					try
					{
						this.D_Event();
					}
					catch (Exception ex)
					{
						SystemTools.WriteLog(ex);
					}
				});
				th.Start();
				Thread.Sleep(500);
				th.Join();

				this.BeginInvoke((MethodInvoker)delegate
				{
					this.Close();
				});
			})
			.Start();

			this.MT_Enabled = true;
		}

		private void BusyDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MT_Enabled = false;
		}

		// ---- setting ----

		private object Setting_SyncRoot = new object();
		private string NextMessage;

		public void SetMessage(string message)
		{
			lock (this.Setting_SyncRoot)
			{
				this.NextMessage = message;
			}
		}

		// ----

		private bool MT_Enabled;
		private bool MT_Busy;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MT_Enabled == false && this.MT_Busy)
				return;

			this.MT_Busy = true;

			try
			{
				lock (this.Setting_SyncRoot)
				{
					if (this.NextMessage != null)
					{
						this.MainMessage.Text = this.NextMessage;
						this.NextMessage = null;
					}
				}
			}
			catch (Exception ex)
			{
				SystemTools.WriteLog(ex);
			}
			finally
			{
				this.MT_Busy = false;
			}
		}
	}
}
