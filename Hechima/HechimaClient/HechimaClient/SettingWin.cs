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
		}

		private void LoadSetting()
		{
			// ---- 設定画面 Items ----

			this.ServerDomain.Text = Gnd.setting.ServerDomain;
			this.ServerPort.Text = "" + Gnd.setting.ServerPort;
			this.crypTunnelPort.Text = "" + Gnd.setting.crypTunnelPort;
			this.Password.Text = Gnd.setting.Password;

			this.BouyomiChanEnabled.Checked = Gnd.setting.BouyomiChanEnabled;
			this.BouyomiChanDomain.Text = Gnd.setting.BouyomiChanDomain;
			this.BouyomiChanPort.Text = "" + Gnd.setting.BouyomiChanPort;

			this.MessageTextEnterMode.SelectedIndex = (int)Gnd.setting.MessageTextEnterMode;

			this.RemarkFormat.Text = Gnd.setting.RemarkFormat;

			this.RemarksTextFontFamily.Text = Gnd.setting.RemarksTextFontFamily;
			this.RemarksTextFontSize.Text = "" + Gnd.setting.RemarksTextFontSize;
			SetColor(this.RemarksTextForeColorBtn, Gnd.setting.RemarksTextForeColor);
			SetColor(this.RemarksTextBackColorBtn, Gnd.setting.RemarksTextBackColor);
			SetColor(this.MessageTextForeColorBtn, Gnd.setting.MessageTextForeColor);
			SetColor(this.MessageTextBackColorBtn, Gnd.setting.MessageTextBackColor);
			this.TripEnabled.Checked = Gnd.setting.TripEnabled;
			this.ShowRemarkStampDate.Checked = Gnd.setting.ShowRemarkStampDate;

			this.UserName.Text = Gnd.setting.UserName;
			this.UserTrip.Text = Gnd.setting.UserTrip;

			// ----
		}

		private void SaveSetting()
		{
			// ---- 設定画面 Items ----

			Gnd.setting.ServerDomain = this.ServerDomain.Text;
			Gnd.setting.ServerPort = int.Parse(this.ServerPort.Text);
			Gnd.setting.crypTunnelPort = int.Parse(this.crypTunnelPort.Text);
			Gnd.setting.Password = this.Password.Text;

			Gnd.setting.BouyomiChanEnabled = this.BouyomiChanEnabled.Checked;
			Gnd.setting.BouyomiChanDomain = this.BouyomiChanDomain.Text;
			Gnd.setting.BouyomiChanPort = int.Parse(this.BouyomiChanPort.Text);

			Gnd.setting.MessageTextEnterMode = (Setting.MessageTextEnterMode_e)this.MessageTextEnterMode.SelectedIndex;

			Gnd.setting.RemarkFormat = this.RemarkFormat.Text;

			Gnd.setting.RemarksTextFontFamily = this.RemarksTextFontFamily.Text;
			Gnd.setting.RemarksTextFontSize = int.Parse(this.RemarksTextFontSize.Text);
			Gnd.setting.RemarksTextForeColor = GetColor(this.RemarksTextForeColorBtn);
			Gnd.setting.RemarksTextBackColor = GetColor(this.RemarksTextBackColorBtn);
			Gnd.setting.MessageTextForeColor = GetColor(this.MessageTextForeColorBtn);
			Gnd.setting.MessageTextBackColor = GetColor(this.MessageTextBackColorBtn);
			Gnd.setting.TripEnabled = this.TripEnabled.Checked;
			Gnd.setting.ShowRemarkStampDate = this.ShowRemarkStampDate.Checked;

			Gnd.setting.UserName = this.UserName.Text;
			Gnd.setting.UserTrip = this.UserTrip.Text;

			// ----
		}

		private void SetColor(Button btn, Color color)
		{
			btn.Text = btn.Text.Substring(0, btn.Text.Length - 6) + ToHexString(color);
		}

		private Color GetColor(Button btn)
		{
			return ToColorHex(btn.Text.Substring(btn.Text.Length - 6));
		}

		private string ToHexString(Color color)
		{
			return StringTools.toHex(new byte[]
			{
				color.R,
				color.G,
				color.B,
			});
		}

		private Color ToColorHex(string src)
		{
			byte[] bSrc = StringTools.hex(src);

			return Color.FromArgb(
				(int)bSrc[0],
				(int)bSrc[1],
				(int)bSrc[2]
				);
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
			this.Password.Text = CorrectItem(this.Password.Text, 1, 1000, "aa9999[x22]",
				StringTools.DIGIT +
				StringTools.ALPHA +
				StringTools.alpha +
				"-@[]"
				);

			//this.BouyomiChanEnabled.Checked
			this.BouyomiChanDomain.Text = CorrectItem(this.BouyomiChanDomain.Text, 1, 300, "localhost");
			this.BouyomiChanPort.Text = CorrectItemInt(this.BouyomiChanPort.Text, 1, 65535, 50001);

			//this.MessageTextEnterMode.SelectedIndex

			this.RemarkFormat.Text = CorrectItem(this.RemarkFormat.Text.ToUpper(), 1, 100, "RSBIRZMR", "RSBZIM");

			this.RemarksTextFontFamily.Text = CorrectItem(this.RemarksTextFontFamily.Text, 1, 300, "メイリオ");
			this.RemarksTextFontSize.Text = CorrectItemInt(this.RemarksTextFontSize.Text, 1, 99, 10);

			try
			{
				new Font(this.RemarksTextFontFamily.Text, int.Parse(this.RemarksTextFontSize.Text));
			}
			catch
			{
				this.RemarksTextFontFamily.Text = "ゴシゴシゴシック";//"メイリオ";
				this.RemarksTextFontSize.Text = "" + 10;
			}

			//this.RemarksTextForeColorBtn
			//this.RemarksTextBackColorBtn
			//this.MessageTextForeColorBtn
			//this.MessageTextBackColorBtn
			//this.TripEnabled.Checked
			//this.ShowRemarkStampDate.Checked

			this.UserName.Text = CorrectItem(this.UserName.Text, 1, 20, "名無しさん" + SecurityTools.getCRandUInt());
			this.UserName.Text = this.UserName.Text.Replace(Consts.DELIM_NAME_TRIP, Consts.S_DUMMY);
			this.UserTrip.Text = CorrectItem(this.UserTrip.Text, 1, 100, StringTools.toHex(SecurityTools.getCRand(16)));

			// ----
		}

		private string CorrectItem(string value, int minlen, int maxlen, string defval, string availableChrs = null)
		{
			if (availableChrs != null)
			{
				List<char> buff = new List<char>();

				foreach (char chr in value)
					if (availableChrs.Contains(chr))
						buff.Add(chr);

				value = new string(buff.ToArray());
			}
			if (value.Length < minlen)
				return defval;

			if (maxlen < value.Length)
				value = value.Substring(0, maxlen);

			return JString.toJString(value, true, false, false, false);
		}

		private string CorrectItemInt(string value, int minval, int maxval, int defval)
		{
			return "" + IntTools.toInt(value, minval, maxval, defval);
		}

		private void RemarksTextForeColorBtn_Click(object sender, EventArgs e)
		{
			EditColor(this.RemarksTextForeColorBtn);
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
				cd.CustomColors = MakeCustomColors();

				//ダイアログを表示する
				if (cd.ShowDialog() == DialogResult.OK)
				{
					//選択された色の取得
					color = cd.Color;
					SetColor(btn, color);
				}
			}
		}

		private int[] MakeCustomColors()
		{
			List<int> buff = new List<int>();

			for (int c = 0; c < 16; c++)
				buff.Add((int)(SecurityTools.getCRandUInt() & 0xffffff));

			return buff.ToArray();
		}

		private void UpdateUserTripBtn_Click(object sender, EventArgs e)
		{
			this.UserTrip.Text = StringTools.toHex(SecurityTools.getCRand(16));
		}
	}
}
