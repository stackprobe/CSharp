using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Charlotte
{
	public partial class SettingWin : Form
	{
		public SettingWin()
		{
			InitializeComponent();

			_epm = new ErrProvMan(this.ErrProv);
		}

		private void SettingWin_Load(object sender, EventArgs e)
		{
			// 特殊な色 -- 環境によっては見えないんじゃないかと危惧..
			{
				this.Caution.ForeColor = Color.Red;
				this.Caution.BackColor = Color.LightYellow;

				Color memoColor = Color.DarkCyan;

				this.Memo1.ForeColor = memoColor;
				this.Memo2.ForeColor = memoColor;
				this.Memo3.ForeColor = memoColor;
				this.FileSvRecvPort_Memo.ForeColor = memoColor;
				this.NamedTrackPort_Memo.ForeColor = memoColor;
			}
		}

		private bool ShownFlag = false;

		private void SettingWin_Shown(object sender, EventArgs e)
		{
#if false
			{
				Rectangle rect = Screen.PrimaryScreen.WorkingArea;
				const int MARGIN = 50;

				int l = rect.Left + MARGIN;
				int t = rect.Top + MARGIN;
				int w = rect.Width - MARGIN * 2;
				int h = rect.Height - MARGIN * 2;

				this.Left = l;
				this.Top = t;
				this.Width = w;
				this.Height = h;
			}
#endif
			this.MinimumSize = this.Size;

			this.BtnCancel.Focus();
			this.DoLoad();
			this.RefreshUi();

			this.ShownFlag = true;
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			this.Correct();

			if (this.IsError())
				return;

			this.DoSave();
			this.Close();
		}

		private ErrProvMan _epm;

		private bool IsError()
		{
			_epm.Clear();

			_epm.Check(this.ServerDomain, StringTools.DIGIT + StringTools.ALPHA + StringTools.alpha + "-.", 1);
			_epm.Check(this.ServerPort, 1, 65535);
			_epm.Check(this.FileSvPort, 1, 65535);
			_epm.Check(this.NamedTrackHttpPort, 1, 65535);

			_epm.Check(this.FileSvRecvPort, 1, 65535);
			_epm.Check(this.NamedTrackPort, 1, 65535);

			_epm.Check(this.TimeLineTextLenMax, 1000, 999999999);
			_epm.Check(this.TimeLineTextShortenPct, 10, 99);

			_epm.Check(this.UserName, true, false, false, true, 1, 30);
			if (this.UserName.Text.Contains(Consts.TRIP_PREFIX)) _epm.SetError(this.UserName, "「" + Consts.TRIP_PREFIX + "」は使用できません。");
			_epm.Check(this.TripKey, true, false, false, true, 0, 100);

			_epm.Check(this.BouyomichanDomain, StringTools.DIGIT + StringTools.ALPHA + StringTools.alpha + "-.", 1);
			_epm.Check(this.BouyomichanPort, 1, 65535);

			return _epm.HasError();
		}

		private void Correct()
		{
			this.FileSvHomeDir.Text = FileTools.Correct(this.FileSvHomeDir.Text);
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void DoSave()
		{
			this.DoSaveLoad(true);
		}

		private void DoLoad()
		{
			this.DoSaveLoad(false);
		}

		private void DoSaveLoad(bool saveFlag)
		{
			foreach (FieldInfo fi in ReflecTools.GetFields(typeof(SaveData)))
			{
				FieldInfo ctrlFi = ReflecTools.GetField(this, fi.Name);

				if (ctrlFi != null)
				{
					object ctrlObj = ReflecTools.GetValue(ctrlFi, this);
					Control ctrl = (Control)ctrlObj;

					if (saveFlag)
					{
						string str = DataConv.GetString(ctrl);
						object obj = DataConv.GetObject(fi, str);
						ReflecTools.SetValue(fi, Gnd.I.Sd, obj);
					}
					else
					{
						object obj = ReflecTools.GetValue(fi, Gnd.I.Sd);
						string str = DataConv.GetString(fi, obj);
						DataConv.SetString(ctrl, str);
					}
				}
			}
		}

		private void RefreshUi()
		{
			{
				bool flag = this.FileSvEnabled.Checked;

				this.FileSvRecvPortLabel.Enabled = flag;
				this.FileSvRecvPort.Enabled = flag;
				this.FileSvRecvPort_Memo.Enabled = flag;
				this.NamedTrackPortLabel.Enabled = flag;
				this.NamedTrackPort.Enabled = flag;
				this.NamedTrackPort_Memo.Enabled = flag;
				this.BtnFileSvHomeDir.Enabled = flag;
				this.FileSvHomeDir.Enabled = flag;
				this.Btn_B_LinkColor.Enabled = flag;
				this.Btn_B_BackColor.Enabled = flag;
				this.Btn_B_TextColor.Enabled = flag;
				this.B_LinkColor.Enabled = flag;
				this.B_BackColor.Enabled = flag;
				this.B_TextColor.Enabled = flag;
				this.FileSvVisibleHomeDirOnly.Enabled = flag;
			}

			{
				bool flag = this.BouyomichanEnabled.Checked;

				this.BouyomichanDomainLabel.Enabled = flag;
				this.BouyomichanDomain.Enabled = flag;
				this.BouyomichanPortLabel.Enabled = flag;
				this.BouyomichanPort.Enabled = flag;
			}
		}

		private void FileSvEnabled_CheckedChanged(object sender, EventArgs e)
		{
			if (this.FileSvEnabled.Checked && this.ShownFlag)
			{
				DialogResult ret = MessageBox.Show(
					"この設定を有効にすると、このコンピュータの論理ドライブ及びこのコンピュータから" +
					"アクセスできるネットワークフォルダを他のコンピュータから閲覧できるようになります。\n" +
					"この設定を有効にして宜しいですか？",
					"確認",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Warning
					);

				if (ret != DialogResult.Yes)
				{
					this.FileSvEnabled.Checked = false;
					return;
				}
			}
			this.RefreshUi();
		}

		private void BtnLListBackColor_Click(object sender, EventArgs e)
		{
			ColorWin.Edit(this.MemberListBackColor);
		}

		private void BtnLListFont_Click(object sender, EventArgs e)
		{
			FontWin.Edit(this.MemberListFont);
		}

		private void BtnFileSvHomeDir_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog f = new FolderBrowserDialog())
			{
				f.Description = "ホームディレクトリを指定してください。";
				//f.RootFolder = Environment.SpecialFolder.Desktop;
				f.SelectedPath = this.FileSvHomeDir.Text;
				//f.ShowNewFolderButton = true;

				if (f.ShowDialog(this) == DialogResult.OK)
				{
					string dir = f.SelectedPath;
					this.FileSvHomeDir.Text = dir;
				}
			}
		}

		private void BtnTimeLineTextBackColor_Click(object sender, EventArgs e)
		{
			ColorWin.Edit(this.TimeLineTextBackColor);
		}

		private void BtnTimeLineTextStampFont_Click(object sender, EventArgs e)
		{
			FontWin.Edit(this.TimeLineTextStampFont);
		}

		private void BtnTimeLineTextUserNameFont_Click(object sender, EventArgs e)
		{
			FontWin.Edit(this.TimeLineTextUserNameFont);
		}

		private void BtnTimeLineTextMessageFont_Click(object sender, EventArgs e)
		{
			FontWin.Edit(this.TimeLineTextMessageFont);
		}

		private void BtnRemarkTextBackColor_Click(object sender, EventArgs e)
		{
			ColorWin.Edit(this.RemarkTextBackColor);
		}

		private void BtnRemarkTextFont_Click(object sender, EventArgs e)
		{
			FontWin.Edit(this.RemarkTextFont);
		}

		private void BouyomichanEnabled_CheckedChanged(object sender, EventArgs e)
		{
			this.RefreshUi();
		}

		private void Btn_B_LinkColor_Click(object sender, EventArgs e)
		{
			ColorWin.Edit(this.B_LinkColor);
		}

		private void Btn_B_BackColor_Click(object sender, EventArgs e)
		{
			ColorWin.Edit(this.B_BackColor);
		}

		private void Btn_B_TextColor_Click(object sender, EventArgs e)
		{
			ColorWin.Edit(this.B_TextColor);
		}
	}
}
