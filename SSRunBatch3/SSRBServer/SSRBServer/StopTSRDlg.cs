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
	public partial class StopTSRDlg : Form
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

		public StopTSRDlg()
		{
			InitializeComponent();

			this.Status.Text = "";
		}

		private void StopTSRDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void StopTSRDlg_Shown(object sender, EventArgs e)
		{
			this.MTEnabled = true;
		}

		private void StopTSRDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void StopTSRDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MTEnabled = false;
		}

		private bool MTEnabled;
		private bool MTBusy;
		private long MTCount;

		private int TSRZeroCount;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MTEnabled == false || this.MTBusy)
				return;

			this.MTBusy = true;

			try
			{
				if (1 <= Gnd.I.TSRInfos.Count)
				{
					if (Gnd.I.TSRInfos[Gnd.I.TSRInfos.Count - 1].IsEnded())
						Gnd.I.TSRInfos.RemoveAt(Gnd.I.TSRInfos.Count - 1);
				}
				else
				{
					if (5 < this.TSRZeroCount) // 0.5 sec <
					{
						this.MTEnabled = false;
						this.Close();
						return;
					}
					this.TSRZeroCount++;
				}

				{
					string text = "TSR = " + Gnd.I.TSRInfos.Count;

					if (this.Status.Text != text)
						this.Status.Text = text;
				}
			}
			finally
			{
				this.MTBusy = false;
				this.MTCount++;
			}
		}

		private void BtnAbandon_Click(object sender, EventArgs e)
		{
			foreach (Gnd.TSRInfo info in Gnd.I.TSRInfos)
			{
				info.Stop();
			}
			//Gnd.I.TSRInfos.Clear();
		}
	}
}
