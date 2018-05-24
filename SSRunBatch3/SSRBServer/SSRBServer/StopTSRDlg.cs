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

			if (Gnd.I.BatchServer == null)
				this.LMessage.Text += " (S停止!)";
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
				Gnd.I.TSRInfos.Rotate(info => info.IsEnded() == false);

				if (1 <= Gnd.I.TSRInfos.Count)
				{
					this.TSRZeroCount = 0;
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

				// memo @ 2018.5.23
				// TSR を non-TSR によって停止する運用を想定する。
				//   --> TSR=0 を待つには BatchServer を動かしておく必要がある。--> BatchServer 停止前に StopTSR を呼ばなければならない。*1
				// このdlgを閉じてから BatchServer を停止するまでの間に、新しいTSRが開始されないようにしたい。これはその対策
				// RejectNewProcess == true になってから 0.5 秒間 TSR=0 であればほぼ確実だろう。
				//   -- new ProcessStartInfo() から TSRInfos.Enqueue まで 0.5 秒も掛からないだろうということ。
				// 時間による担保は確実ではない。--> 同期を取って担保を取るのが望ましい。fixme
				// 確実ではないので BatchServer 停止後にも StopTSR を呼ぶ。*2 --> このときは TSR を強制終了するしかない。
				//   *1,*2 -- StopServer で StopTSR > StopServer > StopTSR している理由
				if (Gnd.I.BatchServer != null)
					Gnd.I.BatchServer.RejectNewProcess = 1 <= this.TSRZeroCount;

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
			Gnd.I.TSRInfos.Rotate(
				info =>
				{
					info.Stop();
					return true;
				},
				int.MaxValue
				);
		}
	}
}
