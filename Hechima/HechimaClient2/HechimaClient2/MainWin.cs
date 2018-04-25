﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Charlotte.Tools;

namespace Charlotte
{
	public partial class MainWin : Form
	{
		private RemarksRTBManager RemarksRTBMgr;

		public MainWin()
		{
			InitializeComponent();

			this.RemarksRTBMgr = new RemarksRTBManager(new RTBManager(this.RemarksRTB, this.MessageText));

			this.MessageText.Text = "";
			//this.MessageText.Focus(); // ここじゃ効かない。

			this.MinimumSize = new Size(300, 300);

			this.ImportConf();
			this.ImportSetting();
		}

		private void ImportConf()
		{
			if (Gnd.conf.MessageTextFontFamily != Consts.S_DUMMY && Gnd.conf.MessageTextFontSize != 0)
			{
				this.MessageText.Font = new Font(Gnd.conf.MessageTextFontFamily, Gnd.conf.MessageTextFontSize);
			}
			if (Gnd.conf.MessageText_H != 0)
			{
				int ha = Gnd.conf.MessageText_H - this.MessageText.Height;

				this.RemarksRTB.Height -= ha;
				this.MessageText.Top -= ha;
				this.MessageText.Height += ha;
			}
		}

		private void ImportSetting(bool withoutMainWinLTWH = false)
		{
			//this.RemarksRTB.Font = new Font(Gnd.setting.RemarksTextFontFamily, Gnd.setting.RemarksTextFontSize);
			//this.RemarksRTB.ForeColor = Gnd.setting.RemarksTextForeColor;
			this.RemarksRTB.BackColor = Gnd.setting.RemarksTextBackColor;

			this.MessageText.ForeColor = Gnd.setting.MessageTextForeColor;
			this.MessageText.BackColor = Gnd.setting.MessageTextBackColor;

			this.RemarksRTBMgr.RTBMgr.SetBorderStyle(Gnd.setting.Flat_RemarksText);
			Common.SetTextBoxBorderStyle(this.MessageText, Gnd.setting.Flat_MessageText);

			if (withoutMainWinLTWH == false && Gnd.setting.MainWin_W != -1)
			{
				this.Left = Gnd.setting.MainWin_L;
				this.Top = Gnd.setting.MainWin_T;
				this.Width = Gnd.setting.MainWin_W;
				this.Height = Gnd.setting.MainWin_H;
			}
		}

		private void ExportSetting()
		{
			if (this.WindowState == FormWindowState.Normal)
			{
				Gnd.setting.MainWin_L = this.Left;
				Gnd.setting.MainWin_T = this.Top;
				Gnd.setting.MainWin_W = this.Width;
				Gnd.setting.MainWin_H = this.Height;
			}
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			Gnd.OpenOnlineDlg();

			this.MessageText.Focus();

			this.MainTimer.Enabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MainTimer.Enabled = false;
			this.Visible = false;

			this.ExportSetting();

			Gnd.CloseOnlineDlg();

			Common.WaitToBgServiceEnded(false);
		}

		private void RemarksText_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13) // enter
			{
				this.MessageText.Focus();
				e.Handled = true;
			}
		}

		private void MessageText_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 1) // ctrl + a
			{
				this.MessageText.SelectAll();
				e.Handled = true;
			}
			else if (e.KeyChar == 10) // ctrl + enter
			{
				if (Gnd.setting.MessageTextEnterMode == Setting.MessageTextEnterMode_e.CtrlEnterで送信_Enterで改行)
				{
					this.DoRemark();
					e.Handled = true;
				}
			}
			else if (e.KeyChar == 13) // enter
			{
				if (Gnd.setting.MessageTextEnterMode == Setting.MessageTextEnterMode_e.Enterで送信_CtrlEnterで改行)
				{
					this.DoRemark();
					e.Handled = true;
				}
			}
		}

		private List<Remark> DoRemark_OneRemarkBuff = new List<Remark>(new Remark[] { null });

		private void DoRemark()
		{
			string message = this.MessageText.Text;

			this.MessageText.Text = "";

			if (30 < Gnd.bgService.SendingMessages.Count) // ? 未送信発言貯まり過ぎ
				return;

			message = Common.ToFairMessage(message);

			if (message == "") // ? 空の発言
				return;

			Remark provRemark = new Remark()
			{
				Stamp = Common.GetStamp(),
				Ident = Gnd.UserRealName + " @ 127.0.0.2",
				Message = message,
			};

			if (this.RemarksRTBMgr.IsSaved() == false)
				this.RemarksRTBMgr.Save();

			this.DoRemark_OneRemarkBuff[0] = provRemark;
			this.RemarksRTBMgr.Add(this.DoRemark_OneRemarkBuff);
			this.RemarksRTBMgr.RTBMgr.ScrollToBottom();

			Gnd.bgService.SendingMessages.Enqueue(message);
		}

		private void MessageText_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void RemarksText_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private long MT_Count;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			MT_Count++;

			if (MT_Count % 1000 == 30)
			{
				GC.Collect();
				return;
			}
			if (MT_Count % 100 == 30)
			{
				if (Gnd.conf.RemarksTextMaxLength < this.RemarksRTB.TextLength)
				{
					int cutLen = (this.RemarksRTB.TextLength * 100) / Gnd.conf.RemarksTextClearPct;

					this.RemarksRTBMgr.RTBMgr.CutTop(cutLen);
					this.RemarksRTBMgr.RTBMgr.ScrollToTop();
					this.RemarksRTBMgr.RTBMgr.ScrollToBottom();

					return;
				}

				{
					string title;

					if (Gnd.NetErrorLevel < 4)
						title = "へちま改";
					else
						title = "へちま改 @ 通信不良 " + Gnd.NetErrorLevel;

					if (this.Text != title)
						this.Text = title;
				}

				return;
			}
			if (Gnd.onlineDlg != null && Gnd.onlineDlg.XPressed)
			{
				Gnd.onlineDlg.XPressed = false;
				this.Close();
				return;
			}

			Gnd.bgService.Perform();

			if (1 <= Gnd.bgService.RecvedRemarks.Count) // 受信データ -> RemarksText
			{
				List<Remark> rrsBuff = new List<Remark>();
				bool foundOtherRemark = false;

				while (1 <= Gnd.bgService.RecvedRemarks.Count)
				{
					Remark remark = Gnd.bgService.RecvedRemarks.Dequeue();

					rrsBuff.Add(remark);

					Gnd.bgService.KnownStamp = Math.Max(Gnd.bgService.KnownStamp, remark.Stamp);

					bool myRemark = remark.Ident.StartsWith(Gnd.UserRealName); // ? 自分の発言である。

					if (myRemark == false)
						foundOtherRemark = true;

					if (
						Gnd.setting.BouyomiChanEnabled &&
						Gnd.bgService.RecvedRemarks.Count < 30 &&
						(
							Gnd.setting.BouyomiChanIgnoreSelfRemark == false ||
							myRemark == false
						)
						)
					{
						BouyomiChan bc = new BouyomiChan();

						bc.ServerDomain = Gnd.setting.BouyomiChanDomain;
						bc.ServerPort = Gnd.setting.BouyomiChanPort;
						bc.Speed =
							Gnd.setting.BouyomiChanSpeedUseDef ?
							BouyomiChan.SPEED_DEF :
							Gnd.setting.BouyomiChanSpeed;
						bc.Tone =
							Gnd.setting.BouyomiChanToneUseDef ?
							BouyomiChan.TONE_DEF :
							Gnd.setting.BouyomiChanTone;
						bc.Volume =
							Gnd.setting.BouyomiChanVolumeUseDef ?
							BouyomiChan.VOLUME_DEF :
							Gnd.setting.BouyomiChanVolume;
						bc.Voice = Gnd.setting.BouyomiChanVoice;

						string msg = remark.Message;

						if (Gnd.setting.BouyomiChanSnipLen < msg.Length)
							msg = msg.Substring(0, Gnd.setting.BouyomiChanSnipLen) + Gnd.setting.BouyomiChanSnippedTrailer;

						bc.Message = msg;

						Gnd.bgService.BouyomiChanSendDataBuff.Enqueue(bc.GetSendData());
					}
				}

				// RemarksText 更新 {

				this.RemarksRTBMgr.Add(rrsBuff, true);

				// 他の人の発言が有る -> RemarksRTB のテキストが今より長くなる -> スクロールOK -> スクロールする。
				// 他の人の発言が無い -> RemarksRTB のテキストが今と同じ長さになる -> スクロールに問題 -> スクロールしない。
				if (foundOtherRemark)
					this.RemarksRTBMgr.RTBMgr.ScrollToBottom();

				// }

				//foundOtherRemark = true; // test

				if (Gnd.setting.TaskBarFlashEnabled && foundOtherRemark)
				{
					Common.TaskBarFlash(this);
				}
				return;
			}
		}

		private void 設定SToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.MainTimer.Enabled = false;
			this.Visible = false;
			this.ExportSetting();

			Gnd.CloseOnlineDlg();
			Common.WaitToBgServiceEnded();

			using (SettingWin f = new SettingWin())
			{
				f.ShowDialog();

				if (f.OkBtnPressed)
				{
					this.Visible = true; // 非表示のままだと、コントロールの更新が反映されないことがある。-- tb.BorderStyle

					Gnd.ImportSetting();
					this.ImportSetting(true);
					Gnd.setting.Save();
				}
			}
			Gnd.OpenOnlineDlg();

			this.Visible = true;
			this.MainTimer.Enabled = true;
		}

		private void 見るだけMToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.MainTimer.Enabled = false;
			this.Visible = false;
			this.ExportSetting();

			Gnd.CloseOnlineDlg();
			Common.WaitToBgServiceEnded();

			using (ViewWin f = new ViewWin(this.RemarksRTB.Text))
			{
				f.ShowDialog();
			}
			Gnd.OpenOnlineDlg();

			this.Visible = true;
			this.MainTimer.Enabled = true;
		}

		private void たかざわダブルじゅんすけDToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.MessageText.Text = "_(:3 」∠ )_(:3 」∠ )_";
			this.MessageText.Focus();
			this.MessageText.SelectAll();
		}
	}
}
