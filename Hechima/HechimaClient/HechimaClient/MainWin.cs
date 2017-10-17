using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Charlotte
{
	public partial class MainWin : Form
	{
		public MainWin()
		{
			InitializeComponent();

			this.RemarksText.ForeColor = new TextBox().ForeColor;
			this.RemarksText.BackColor = new TextBox().BackColor;
			this.RemarksText.Text = "";

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

				this.RemarksText.Height -= ha;
				this.MessageText.Top -= ha;
				this.MessageText.Height += ha;
			}
		}

		private void ImportSetting(bool withoutMainWinLTWH = false)
		{
			this.RemarksText.Font = new Font(Gnd.setting.RemarksTextFontFamily, Gnd.setting.RemarksTextFontSize);
			this.RemarksText.ForeColor = Gnd.setting.RemarksTextForeColor;
			this.RemarksText.BackColor = Gnd.setting.RemarksTextBackColor;

			this.MessageText.ForeColor = Gnd.setting.MessageTextForeColor;
			this.MessageText.BackColor = Gnd.setting.MessageTextBackColor;

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

			this.ExportSetting();
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

		private string TrueRemarksText = null;

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
				Ident = Gnd.UserRealName + " @ 127.0.0.1",
				Message = message,
			};
			string provText = Common.RemarkToTextBoxText(provRemark);

			if (this.TrueRemarksText == null)
				this.TrueRemarksText = this.RemarksText.Text;

			this.RemarksText.AppendText(provText);
			this.RemarksText.SelectionStart = this.RemarksText.TextLength;
			this.RemarksText.ScrollToCaret();

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
				// zantei -- このへん適当...
				if (Gnd.conf.RemarksTextMaxLength < this.RemarksText.TextLength)
				{
					int clearLength = (this.RemarksText.TextLength * 100) / Gnd.conf.RemarksTextClearPct;

					this.RemarksText.Text = this.RemarksText.Text.Substring(clearLength);
					this.RemarksText.SelectionStart = this.RemarksText.TextLength;
					this.RemarksText.ScrollToCaret();

					return;
				}
			}

			Gnd.bgService.Perform();

			if (1 <= Gnd.bgService.RecvedRemarks.Count) // 受信データ -> RemarksText
			{
				StringBuilder buff = new StringBuilder();

				while (1 <= Gnd.bgService.RecvedRemarks.Count)
				{
					Remark remark = Gnd.bgService.RecvedRemarks.Dequeue();
					string text = Common.RemarkToTextBoxText(remark);

					buff.Append(text);

					Gnd.bgService.KnownStamp = Math.Max(Gnd.bgService.KnownStamp, remark.Stamp);

					if (
						Gnd.bgService.RecvedRemarks.Count < 30 &&
						Gnd.setting.BouyomiChanEnabled
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
						bc.Message = remark.Message;

						bc.GetSendData(); // TODO -- 送信
					}
				}
				string rrsText = buff.ToString();

				// RemarksText 更新 {

				if (this.TrueRemarksText != null)
				{
					this.RemarksText.Text = this.TrueRemarksText + rrsText;
					this.TrueRemarksText = null;
				}
				else
					this.RemarksText.AppendText(rrsText);

				this.RemarksText.SelectionStart = this.RemarksText.TextLength;
				this.RemarksText.ScrollToCaret();

				// }

				return;
			}
		}

		private void 設定SToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.MainTimer.Enabled = false;
			this.Visible = false;
			this.ExportSetting();

			Common.WaitToBgServiceEndable();

			using (SettingWin f = new SettingWin())
			{
				f.ShowDialog();

				if (f.OkBtnPressed)
				{
					Gnd.ImportSetting();
					this.ImportSetting(true);
					Gnd.setting.Save();
				}
			}
			this.Visible = true;
			this.MainTimer.Enabled = true;
		}
	}
}
