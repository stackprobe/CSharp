using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	public partial class SettingWin : Form
	{
		public SettingWin()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		private void SettingWin_Load(object sender, EventArgs e)
		{
			this.LoadSetting();
			this.UIRefresh();
		}

		private void LoadSetting()
		{
			// ---- 設定画面 Items ----

			this.ServerDomain.Text = Gnd.setting.ServerDomain;
			this.ServerPort.Text = "" + Gnd.setting.ServerPort;
			this.crypTunnelPort.Text = "" + Gnd.setting.crypTunnelPort;
			this.Password.Text = Gnd.setting.Password;

			this.BouyomiChanDomain.Text = Gnd.setting.BouyomiChanDomain;
			this.BouyomiChanPort.Text = "" + Gnd.setting.BouyomiChanPort;

			this.MessageTextEnterMode.SelectedIndex = (int)Gnd.setting.MessageTextEnterMode;

			this.RemarkFormat.Text = Gnd.setting.RemarkFormat;

			this.RemarksTextDefaultFontFamily.Text = Gnd.setting.RemarksTextDefaultFontFamily;
			this.RemarksTextDefaultFontSize.Text = "" + Gnd.setting.RemarksTextDefaultFontSize;
			SetColor(this.RemarksTextDefaultFontColorBtn, Gnd.setting.RemarksTextDefaultFontColor);
			SetColor(this.RemarksTextBackColorBtn, Gnd.setting.RemarksTextBackColor);
			SetColor(this.MessageTextForeColorBtn, Gnd.setting.MessageTextForeColor);
			SetColor(this.MessageTextBackColorBtn, Gnd.setting.MessageTextBackColor);
			this.TripEnabled.Checked = Gnd.setting.TripEnabled;
			this.IPDisabledWhenTripDisabled.Checked = Gnd.setting.IPDisabledWhenTripDisabled;
			this.ShowRemarkStampDate.Checked = Gnd.setting.ShowRemarkStampDate;

			this.OnlineDlgEnabled.Checked = Gnd.setting.OnlineDlgEnabled;
			SetColor(this.OnlineForeColorBtn, Gnd.setting.OnlineForeColor);
			SetColor(this.OnlineBackColorBtn, Gnd.setting.OnlineBackColor);
			this.Flat_RemarksText.Checked = Gnd.setting.Flat_RemarksText;
			this.Flat_MessageText.Checked = Gnd.setting.Flat_MessageText;
			this.Flat_OnlineText.Checked = Gnd.setting.Flat_OnlineText;
			this.TaskBarFlashEnabled.Checked = Gnd.setting.TaskBarFlashEnabled;

			this.UserName.Text = Gnd.setting.UserName;
			this.UserTrip.Text = Gnd.setting.UserTrip;

			this.BouyomiChanEnabled.Checked = Gnd.setting.BouyomiChanEnabled;
			this.BouyomiChanSpeed.Text = "" + Gnd.setting.BouyomiChanSpeed;
			this.BouyomiChanSpeedUseDef.Checked = Gnd.setting.BouyomiChanSpeedUseDef;
			this.BouyomiChanTone.Text = "" + Gnd.setting.BouyomiChanTone;
			this.BouyomiChanToneUseDef.Checked = Gnd.setting.BouyomiChanToneUseDef;
			this.BouyomiChanVolume.Text = "" + Gnd.setting.BouyomiChanVolume;
			this.BouyomiChanVolumeUseDef.Checked = Gnd.setting.BouyomiChanVolumeUseDef;
			this.BouyomiChanVoice.Text = "" + Gnd.setting.BouyomiChanVoice;

			this.BouyomiChanSnipLen.Text = "" + Gnd.setting.BouyomiChanSnipLen;
			this.BouyomiChanSnippedTrailer.Text = Gnd.setting.BouyomiChanSnippedTrailer;
			this.BouyomiChanIgnoreSelfRemark.Checked = Gnd.setting.BouyomiChanIgnoreSelfRemark;

			// MemberFontList
			{
				this.MemberFontList.Items.Clear();

				foreach (MemberFont mf in Gnd.setting.MemberFonts)
				{
					this.MemberFontList.Items.Add(mf.GetString());
				}
			}

			// ----
		}

		private void SaveSetting()
		{
			// ---- 設定画面 Items ----

			Gnd.setting.ServerDomain = this.ServerDomain.Text;
			Gnd.setting.ServerPort = int.Parse(this.ServerPort.Text);
			Gnd.setting.crypTunnelPort = int.Parse(this.crypTunnelPort.Text);
			Gnd.setting.Password = this.Password.Text;

			Gnd.setting.BouyomiChanDomain = this.BouyomiChanDomain.Text;
			Gnd.setting.BouyomiChanPort = int.Parse(this.BouyomiChanPort.Text);

			Gnd.setting.MessageTextEnterMode = (Setting.MessageTextEnterMode_e)this.MessageTextEnterMode.SelectedIndex;

			Gnd.setting.RemarkFormat = this.RemarkFormat.Text;

			Gnd.setting.RemarksTextDefaultFontFamily = this.RemarksTextDefaultFontFamily.Text;
			Gnd.setting.RemarksTextDefaultFontSize = int.Parse(this.RemarksTextDefaultFontSize.Text);
			Gnd.setting.RemarksTextDefaultFontColor = GetColor(this.RemarksTextDefaultFontColorBtn);
			Gnd.setting.RemarksTextBackColor = GetColor(this.RemarksTextBackColorBtn);
			Gnd.setting.MessageTextForeColor = GetColor(this.MessageTextForeColorBtn);
			Gnd.setting.MessageTextBackColor = GetColor(this.MessageTextBackColorBtn);
			Gnd.setting.TripEnabled = this.TripEnabled.Checked;
			Gnd.setting.IPDisabledWhenTripDisabled = this.IPDisabledWhenTripDisabled.Checked;
			Gnd.setting.ShowRemarkStampDate = this.ShowRemarkStampDate.Checked;

			Gnd.setting.OnlineDlgEnabled = this.OnlineDlgEnabled.Checked;
			Gnd.setting.OnlineForeColor = GetColor(this.OnlineForeColorBtn);
			Gnd.setting.OnlineBackColor = GetColor(this.OnlineBackColorBtn);
			Gnd.setting.Flat_RemarksText = this.Flat_RemarksText.Checked;
			Gnd.setting.Flat_MessageText = this.Flat_MessageText.Checked;
			Gnd.setting.Flat_OnlineText = this.Flat_OnlineText.Checked;
			Gnd.setting.TaskBarFlashEnabled = this.TaskBarFlashEnabled.Checked;

			Gnd.setting.UserName = this.UserName.Text;
			Gnd.setting.UserTrip = this.UserTrip.Text;

			Gnd.setting.BouyomiChanEnabled = this.BouyomiChanEnabled.Checked;
			Gnd.setting.BouyomiChanSpeed = int.Parse(this.BouyomiChanSpeed.Text);
			Gnd.setting.BouyomiChanSpeedUseDef = this.BouyomiChanSpeedUseDef.Checked;
			Gnd.setting.BouyomiChanTone = int.Parse(this.BouyomiChanTone.Text);
			Gnd.setting.BouyomiChanToneUseDef = this.BouyomiChanToneUseDef.Checked;
			Gnd.setting.BouyomiChanVolume = int.Parse(this.BouyomiChanVolume.Text);
			Gnd.setting.BouyomiChanVolumeUseDef = this.BouyomiChanVolumeUseDef.Checked;
			Gnd.setting.BouyomiChanVoice = int.Parse(this.BouyomiChanVoice.Text);

			Gnd.setting.BouyomiChanSnipLen = int.Parse(this.BouyomiChanSnipLen.Text);
			Gnd.setting.BouyomiChanSnippedTrailer = this.BouyomiChanSnippedTrailer.Text;
			Gnd.setting.BouyomiChanIgnoreSelfRemark = this.BouyomiChanIgnoreSelfRemark.Checked;

			// MemberFontList
			{
				Gnd.setting.MemberFonts.Clear();

				for (int index = 0; index < this.MemberFontList.Items.Count; index++)
				{
					MemberFont mf = new MemberFont();
					string src = (string)this.MemberFontList.Items[index];
					mf.SetString(src);

					Gnd.setting.MemberFonts.Add(mf);
				}
			}

			// ----
		}

		private void SetColor(Button btn, Color color)
		{
			btn.Text = btn.Text.Substring(0, btn.Text.Length - 6) + Common.ToHexString(color);
		}

		private Color GetColor(Button btn)
		{
			return Common.ToColorHex(btn.Text.Substring(btn.Text.Length - 6));
		}

		private void CancelBtn_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		public bool OkBtnPressed = false;

		private void OkBtn_Click(object sender, EventArgs e)
		{
			this.CorrectItems();
			this.SaveSetting();

			this.OkBtnPressed = true;

			this.Close();
		}

		private void CorrectBtn_Click(object sender, EventArgs e)
		{
			this.CorrectItems();
		}

		private void CorrectItems()
		{
			// ---- 設定画面 Items ----

			this.ServerDomain.Text = CorrectItem(this.ServerDomain.Text, 1, 300, "localhost",
				StringTools.DIGIT +
				StringTools.ALPHA +
				StringTools.alpha +
				"-."
				);
			this.ServerPort.Text = CorrectItemInt(this.ServerPort.Text, 1, 65535, 52255);
			this.crypTunnelPort.Text = CorrectItemInt(this.crypTunnelPort.Text, 1, 65535, 52525);
			this.Password.Text = CorrectItem(this.Password.Text, 1, 1000, "aa9999x22x",
				StringTools.DIGIT +
				StringTools.ALPHA +
				StringTools.alpha +
				"-@[]"
				);

			this.BouyomiChanDomain.Text = CorrectItem(this.BouyomiChanDomain.Text, 1, 300, "localhost");
			this.BouyomiChanPort.Text = CorrectItemInt(this.BouyomiChanPort.Text, 1, 65535, 50001);

			// zantei -- BCPort問題
			{
				int p1 = int.Parse(this.ServerPort.Text);
				int p2 = int.Parse(this.crypTunnelPort.Text);
				int p = int.Parse(this.BouyomiChanPort.Text);

				bool mod = false;

				while (
					p == p1 ||
					p == p2
					)
				{
					if (p < 65535)
						p++;
					else
						p = 50001;

					mod = true;
				}
				this.BouyomiChanPort.Text = "" + p;

				if (mod)
					MessageBox.Show(
						"止むに止まれぬ事情で棒読みちゃんのポート番号を変更しました。",
						"情報",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information
						);
			}

			//this.MessageTextEnterMode.SelectedIndex

			this.RemarkFormat.Text = CorrectItem(this.RemarkFormat.Text.ToUpper(), 1, 100, "RRSBIRZM", "RSBZIM", value => value.EndsWith("R") == false);

			this.RemarksTextDefaultFontFamily.Text = CorrectItem(this.RemarksTextDefaultFontFamily.Text, 1, 300, "メイリオ");
			this.RemarksTextDefaultFontSize.Text = CorrectItemInt(this.RemarksTextDefaultFontSize.Text, 1, 99, 10);

			try
			{
				new Font(this.RemarksTextDefaultFontFamily.Text, int.Parse(this.RemarksTextDefaultFontSize.Text));
			}
			catch
			{
				this.RemarksTextDefaultFontFamily.Text = "ゴシゴシゴシック";//"メイリオ";
				this.RemarksTextDefaultFontSize.Text = "" + 10;
			}

			//this.RemarksTextForeColorBtn
			//this.RemarksTextBackColorBtn
			//this.MessageTextForeColorBtn
			//this.MessageTextBackColorBtn
			//this.TripEnabled.Checked
			//this.IPDisabledWhenTripDisabled.Checked
			//this.ShowRemarkStampDate.Checked

			//this.OnlineDlgEnabled.Checked
			//this.OnlineForeColorBtn
			//this.OnlineBackColorBtn
			//this.Flat_RemarksText.Checked
			//this.Flat_MessageText.Checked
			//this.Flat_OnlineText.Checked
			//this.TaskBarFlashEnabled.Checked

			this.UserName.Text = CorrectItem(this.UserName.Text, 1, 20, "名無しさん" + SecurityTools.getCRandUInt());
			this.UserName.Text = this.UserName.Text.Replace(Consts.DELIM_NAME_TRIP, Consts.S_DUMMY);
			this.UserTrip.Text = CorrectItem(this.UserTrip.Text, 1, 100, StringTools.toHex(SecurityTools.getCRand(16)));

			//this.BouyomiChanEnabled.Checked
			this.BouyomiChanSpeed.Text = CorrectItemInt(this.BouyomiChanSpeed.Text, BouyomiChan.SPEED_MIN, BouyomiChan.SPEED_MAX, (BouyomiChan.SPEED_MIN + BouyomiChan.SPEED_MAX) / 2);
			//this.BouyomiChanSpeedUseDef.Checked
			this.BouyomiChanTone.Text = CorrectItemInt(this.BouyomiChanTone.Text, BouyomiChan.TONE_MIN, BouyomiChan.TONE_MAX, (BouyomiChan.TONE_MIN + BouyomiChan.TONE_MAX) / 2);
			//this.BouyomiChanToneUseDef.Checked
			this.BouyomiChanVolume.Text = CorrectItemInt(this.BouyomiChanVolume.Text, BouyomiChan.VOLUME_MIN, BouyomiChan.VOLUME_MAX, (BouyomiChan.VOLUME_MIN + BouyomiChan.VOLUME_MAX) / 2);
			//this.BouyomiChanVolumeUseDef.Checked
			this.BouyomiChanVoice.Text = CorrectItemInt(this.BouyomiChanVoice.Text, BouyomiChan.VOICE_MIN, BouyomiChan.VOICE_MAX, BouyomiChan.VOICE_MIN);

			this.BouyomiChanSnipLen.Text = CorrectItemInt(this.BouyomiChanSnipLen.Text, 1, 999, 100);
			this.BouyomiChanSnippedTrailer.Text = CorrectItem(this.BouyomiChanSnippedTrailer.Text, 1, 1000, "以下略");
			//this.BouyomiChanIgnoreSelfRemark.Checked

			//this.MemberFontList

			// ----
		}

		private string CorrectItem(string value, int minlen, int maxlen, string defval, string availableChrs = null, Func<string, bool> extraCheck = null)
		{
			if (availableChrs != null)
			{
				List<char> buff = new List<char>();

				foreach (char chr in value)
					if (availableChrs.Contains(chr))
						buff.Add(chr);

				value = new string(buff.ToArray());
			}
			value = JString.toJString(value, true, false, false, false);

			if (value.Length < minlen)
				return defval;

			if (maxlen < value.Length)
				value = value.Substring(0, maxlen);

			if (extraCheck != null && extraCheck(value) == false)
				return defval;

			return value;
		}

		private string CorrectItemInt(string value, int minval, int maxval, int defval)
		{
			return "" + IntTools.toInt(value, minval, maxval, defval);
		}

		private string CorrectItemColors(string value, int minlen, int maxlen, Color[] defval)
		{
			List<string> dest = new List<string>();

			foreach (string fToken in value.Split(':'))
			{
				string token = fToken;

				token = token.Trim();

				if (token != "")
				{
					token = token.ToLower();
					token = CorrectItem(token, 6, 6, "99aaff", StringTools.hexadecimal);

					dest.Add(token);
				}
			}
			if (dest.Count < minlen)
				return Common.ToString(defval);

			if (maxlen < dest.Count)
				dest.RemoveRange(maxlen, dest.Count - maxlen);

			return string.Join(":", dest);
		}

		private void RemarksTextForeColorBtn_Click(object sender, EventArgs e)
		{
			EditColor(this.RemarksTextDefaultFontColorBtn);
		}

		private void RemarksTextBackColorBtn_Click(object sender, EventArgs e)
		{
			EditColor(this.RemarksTextBackColorBtn);
		}

		private void MessageTextForeColorBtn_Click(object sender, EventArgs e)
		{
			EditColor(this.MessageTextForeColorBtn);
		}

		private void MessageTextBackColorBtn_Click(object sender, EventArgs e)
		{
			EditColor(this.MessageTextBackColorBtn);
		}

		private void EditColor(Button btn)
		{
			Color color = GetColor(btn);

			//ColorDialogクラスのインスタンスを作成
			using (ColorDialog cd = new ColorDialog())
			{
				//はじめに選択されている色を設定
				cd.Color = color;
				//色の作成部分を表示可能にする
				//デフォルトがTrueのため必要はない
				cd.AllowFullOpen = true;
				//純色だけに制限しない
				//デフォルトがFalseのため必要はない
				cd.SolidColorOnly = false;
				//[作成した色]に指定した色（RGB値）を表示する
				cd.CustomColors = Common.MakeCustomColors();

				//ダイアログを表示する
				if (cd.ShowDialog() == DialogResult.OK)
				{
					//選択された色の取得
					color = cd.Color;
					SetColor(btn, color);
				}
			}
		}

		private void UpdateUserTripBtn_Click(object sender, EventArgs e)
		{
			this.UserTrip.Text = StringTools.toHex(SecurityTools.getCRand(16));
		}

		private void TripEditable_CheckedChanged(object sender, EventArgs e)
		{
			this.UIRefresh();
		}

		private void UIRefresh()
		{
			this.IPDisabledWhenTripDisabled.Enabled = this.TripEnabled.Checked == false;

			{
				bool flag = this.TripEditable.Checked;

				this.UserTrip.Enabled = flag;
				this.UpdateUserTripBtn.Enabled = flag;
			}

			this.UIR_SetColor(this.BouyomiChanSpeed, this.BouyomiChanSpeedUseDef.Checked);
			this.UIR_SetColor(this.BouyomiChanTone, this.BouyomiChanToneUseDef.Checked);
			this.UIR_SetColor(this.BouyomiChanVolume, this.BouyomiChanVolumeUseDef.Checked);
		}

		private void UIR_SetColor(TextBox tb, bool disabled)
		{
			tb.ForeColor = disabled ? Color.Gray : Color.Black;
		}

		private void BouyomiChanSpeedUseDef_CheckedChanged(object sender, EventArgs e)
		{
			this.UIRefresh();
		}

		private void BouyomiChanToneUseDef_CheckedChanged(object sender, EventArgs e)
		{
			this.UIRefresh();
		}

		private void BouyomiChanVolumeUseDef_CheckedChanged(object sender, EventArgs e)
		{
			this.UIRefresh();
		}

		private void OnlineForeColorBtn_Click(object sender, EventArgs e)
		{
			EditColor(this.OnlineForeColorBtn);
		}

		private void OnlineBackColorBtn_Click(object sender, EventArgs e)
		{
			EditColor(this.OnlineBackColorBtn);
		}

		private void TripEnabled_CheckedChanged(object sender, EventArgs e)
		{
			this.UIRefresh();
		}

		private void MemberFontAddBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (Gnd.conf.MemberFontMax <= this.MemberFontList.Items.Count)
				{
					MessageBox.Show(
						"大杉",
						"情報",
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning
						);

					return;
				}
				this.MemberFontList.Items.Add(new MemberFont().GetString());
				this.MemberFontList.SelectedIndex = this.MemberFontList.Items.Count - 1;
			}
			catch (Exception ex)
			{
				Gnd.Logger.writeLine(ex);
			}
		}

		private void MemberFontEditBtn_Click(object sender, EventArgs e)
		{
			try
			{
				int index = this.MemberFontList.SelectedIndex;

				if (index == -1)
					return;

				this.MemberFontList.Items[index] = this.EditMemberFontStr((string)this.MemberFontList.Items[index]);
			}
			catch (Exception ex)
			{
				Gnd.Logger.writeLine(ex);
			}
		}

		private string EditMemberFontStr(string str)
		{
			MemberFont mf = new MemberFont();
			mf.SetString(str);

			this.Visible = false;

			using (MemberFontDlg f = new MemberFontDlg() { MemberFont = mf })
			{
				f.ShowDialog();
			}
			this.Visible = true;

			return mf.GetString();
		}

		private void MemberFontDeleteBtn_Click(object sender, EventArgs e)
		{
			try
			{
				int index = this.MemberFontList.SelectedIndex;

				if (index == -1)
					return;

				this.MemberFontList.Items.RemoveAt(index);
				index = Math.Min(index, this.MemberFontList.Items.Count - 1);
				this.MemberFontList.SelectedIndex = index;
			}
			catch (Exception ex)
			{
				Gnd.Logger.writeLine(ex);
			}
		}

		private void MemberFontUpBtn_Click(object sender, EventArgs e)
		{
			try
			{
				int index = this.MemberFontList.SelectedIndex;

				if (index < 1)
					return;

				string tmp = (string)this.MemberFontList.Items[index];
				this.MemberFontList.Items[index] = this.MemberFontList.Items[index - 1];
				this.MemberFontList.Items[index - 1] = tmp;
				this.MemberFontList.SelectedIndex = index - 1;
			}
			catch (Exception ex)
			{
				Gnd.Logger.writeLine(ex);
			}
		}

		private void MemberFontDownBtn_Click(object sender, EventArgs e)
		{
			try
			{
				int index = this.MemberFontList.SelectedIndex;

				if (index == -1)
					return;

				if (this.MemberFontList.Items.Count - 1 <= index)
					return;

				string tmp = (string)this.MemberFontList.Items[index];
				this.MemberFontList.Items[index] = this.MemberFontList.Items[index + 1];
				this.MemberFontList.Items[index + 1] = tmp;
				this.MemberFontList.SelectedIndex = index + 1;
			}
			catch (Exception ex)
			{
				Gnd.Logger.writeLine(ex);
			}
		}

		private void 選択解除ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.MemberFontList.ClearSelected();
		}
	}
}
