﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using Charlotte.Tools;

namespace Charlotte.Chocomint.Dialogs
{
	public partial class WaitDlg : Form
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

		public ThreadEx Th = null;
		public Func<double> Interlude = () => 0.5;
		public Action Interlude_Cancelled = () => { };
		public Action PostShown = () => { };
		public string Message_Cancelled = "キャンセルしています...";

		// <---- prm

		public static bool LastCancelled = false;

		// <---- ret

		public static bool Cancellable = true;
		public static SyncValue<string> MessagePost = new SyncValue<string>();
		public static SyncValue<string[]> DetailMessagePost = new SyncValue<string[]>();

		public WaitDlg()
		{
			// reset
			{
				LastCancelled = false;
				Cancellable = true;
				MessagePost.Post(null);
				DetailMessagePost.Post(null);
			}

			InitializeComponent();

			this.MinimumSize = this.Size;
			this.DetailMessage.Text = "";
			this.DetailMessage.Enabled = false;
		}

		private void WaitDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void WaitDlg_Shown(object sender, EventArgs e)
		{
			this.EndedCount = 0;

			this.PostShown();
			ChocomintDialogsCommon.DlgCommonPostShown(this);
		}

		private void WaitDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void WaitDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.EndedCount = -1; // 2bs
		}

		private int EndedCount = -1;
		private int CancelledCount = 0;
		private bool DetailMessagePosted = false;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.EndedCount == -1)
				return;

			if (this.CancelledCount != 0)
			{
				this.Interlude_Cancelled();

				if (10 < ++this.CancelledCount)
				{
					if (this.Th == null || this.Th.IsEnded())
					{
						this.EndedCount = -1;
						this.Close();
					}
				}
				return;
			}
			if (this.EndedCount == 0)
			{
				if (this.Th == null || this.Th.IsEnded())
				{
					this.EndedCount = 1;
					this.SetProgressRate(1.0);
					this.BtnCancel.Enabled = false;
					return;
				}

				{
					double progressRate = this.Interlude();

					progressRate = DoubleTools.ToRange(progressRate, 0.0, 1.0);
					progressRate *= 0.9;
					progressRate += 0.05;

					this.SetProgressRate(progressRate);
				}

				{
					string message = MessagePost.GetPost(null);

					if (message != null)
					{
						if (this.Message.Text != message)
							this.Message.Text = message;
					}
				}

				{
					string[] detailMessage = DetailMessagePost.GetPost(null);

					if (detailMessage != null)
					{
						string text = string.Join("\r\n", detailMessage).Trim();

						if (this.DetailMessage.Text != text)
						{
							if (!this.DetailMessagePosted)
							{
								this.DetailMessagePosted = true;

								this.DetailMessage.Enabled = true;
								this.DetailMessage.Focus();
							}
							this.DetailMessage.Text = text;
							this.DetailMessage.SelectionStart = text.Length;
							this.DetailMessage.ScrollToCaret();
						}
					}
				}

				{
					// プログラム的に BtnCancel.Enabled = false にした後は EndedCount, CancelledCount の条件により、ここへは到達しない。

					bool flag = Cancellable;

					if (this.BtnCancel.Enabled != flag)
						this.BtnCancel.Enabled = flag;
				}

				return;
			}
			if (10 < ++this.EndedCount)
			{
				this.EndedCount = -1;
				this.Close();
				return;
			}
		}

		private void SetProgressRate(double progressRate)
		{
			progressRate = DoubleTools.ToRange(progressRate, 0.0, 1.0);

			int value = DoubleTools.ToInt(progressRate * 1000.0);

			if (this.ProgressBar.Value != value)
				this.ProgressBar.Value = value;
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.CancelledCount = 1;
			this.Message.Text = this.Message_Cancelled;
			this.ProgressBar.Style = ProgressBarStyle.Marquee;
			this.BtnCancel.Enabled = false;

			LastCancelled = true;

			this.Interlude_Cancelled();
		}
	}
}
