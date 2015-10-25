using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte
{
	public partial class MainWin : Form
	{
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
			Gnd.I.TimeLine = new TimeLine(new TimeLineTextMan(new RtbMan(this.TimeLineText, this.RemarkText)));
			Gnd.I.Heartbeat = new Heartbeat(new MemberListMan(this.MemberList));
			Gnd.I.MainWin = this;

			this.MT_Enabled = true;

			if (Gnd.I.Sd.MainWin_W != 0)
			{
				this.Left = Gnd.I.Sd.MainWin_L;
				this.Top = Gnd.I.Sd.MainWin_T;
				this.Width = Gnd.I.Sd.MainWin_W;
				this.Height = Gnd.I.Sd.MainWin_H;
			}

			this.StatusMessage.Text = "";

			this.RefreshUi();

			if (Gnd.I.Sd.ServerTimeDiffを取得しない == false)
			{
				EventCenter.I.AddEvent(Consts.EVENT_REGULAR, delegate
				{
					// ChatMan 使用中の可能性があるのでイベントで実行すること。
					Gnd.I.ChatMan.ServerTimeDiffCommand();
				});
				EventCenter.I.AddEvent(Consts.EVENT_REGULAR, EventCollection.ChatMan_End);
				EventCenter.I.AddEvent(Consts.EVENT_REGULAR, delegate
				{
					string[] lines = Gnd.I.ChatMan.GetOutput();

					if (lines == null)
						return;

					string line = lines[0];
					int ret = int.Parse(line);

					Gnd.I.ServerTimeDiff = ret;

					if (Gnd.I.ServerTimeDiff != 0)
						SystemTools.WriteLog("STD: " + Gnd.I.ServerTimeDiff);
				});
			}
			if (Gnd.I.Sd.MainWinAlwaysTop)
				EventTools.MainWinToTop();
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.SaveLTWH();
			Gnd.I.Sd.Save();
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MT_Enabled = false;

			Gnd.I.TimeLine = null;
			Gnd.I.Heartbeat = null;
			Gnd.I.MainWin = null;
		}

		private bool MT_Enabled;
		private bool MT_Busy;
		private long MT_Count;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MT_Enabled == false || this.MT_Busy)
				return;

			this.MT_Busy = true;

			try
			{
				{
					int pc = EventCenter.I.GetCount(Consts.EVENT_PREFERENCE);
					int rc = EventCenter.I.GetCount(Consts.EVENT_REGULAR);
					int bc = EventCenter.I.GetCount(Consts.EVENT_BACKGROUND);
					string status = "P:" + pc + ", R:" + rc + ", B:" + bc + ", H:" + Gnd.I.Heartbeat.GetStatus();

					if (this.LeftStatusMessage.Text != status)
					{
						int mc = Math.Max(Math.Max(pc, rc), bc);
						Color color;

						if (mc < 20)
							color = Color.Gray;
						else if (mc < 40)
							color = Color.Black;
						else
							color = Color.Red;

						this.LeftStatusMessage.Text = status;

						if (this.LeftStatusMessage.ForeColor != color)
							this.LeftStatusMessage.ForeColor = color;
					}
				}

				if (
					EventCenter.I.Perform(Consts.EVENT_PREFERENCE) == false &&
					EventCenter.I.Perform(Consts.EVENT_REGULAR) == false
					)
					EventCenter.I.Perform(Consts.EVENT_BACKGROUND);

				Gnd.I.TimeLine.EachTimer();
				Gnd.I.Heartbeat.EachTimer();
			}
			catch (Exception ex)
			{
				SystemTools.WriteLog(ex);
			}
			finally
			{
				this.MT_Busy = false;
				this.MT_Count++;
			}
		}

		private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void RefreshUi()
		{
			this.ユーザーリストの幅を固定するLToolStripMenuItem.Checked = Gnd.I.Sd.MemberList_W != -1;
			this.入力エリアの高さを固定するTToolStripMenuItem.Checked = Gnd.I.Sd.RemarkText_H != -1;
			this.送信ボタンの幅を固定するBToolStripMenuItem.Checked = Gnd.I.Sd.BtnSend_W != -1;

			this.MainSplit_SplitterMoved(null, null);
			this.TimeLineSplit_SplitterMoved(null, null);
			this.RemarkSplit_SplitterMoved(null, null);

			this.ユーザーリストを表示しないUToolStripMenuItem.Checked = Gnd.I.Sd.HideMemberList;
			this.送信ボタンを表示しないHToolStripMenuItem.Checked = Gnd.I.Sd.HideBtnSend;

			this.MainSplit.Panel1Collapsed = Gnd.I.Sd.HideMemberList;
			this.RemarkSplit.Panel2Collapsed = Gnd.I.Sd.HideBtnSend;

			this.ユーザーリストの幅を固定するLToolStripMenuItem.Enabled = Gnd.I.Sd.HideMemberList == false;
			this.送信ボタンの幅を固定するBToolStripMenuItem.Enabled = Gnd.I.Sd.HideBtnSend == false;

			Gnd.I.TimeLine.SettingChanged();
			Gnd.I.Heartbeat.SettingChanged();

			this.RemarkText.BackColor = Gnd.I.Sd.RemarkTextBackColor;
			this.RemarkText.ForeColor = Gnd.I.Sd.RemarkTextFont.GetColor();
			this.RemarkText.Font = Gnd.I.Sd.RemarkTextFont.GetFont();
		}

		private void ユーザーリストの幅を固定するLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Gnd.I.Sd.MemberList_W == -1)
				Gnd.I.Sd.MemberList_W = this.MainSplit.SplitterDistance;
			else
				Gnd.I.Sd.MemberList_W = -1;

			this.RefreshUi();
		}

		private void 入力エリアの高さを固定するTToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Gnd.I.Sd.RemarkText_H == -1)
				Gnd.I.Sd.RemarkText_H = GetLeftDistance(this.TimeLineSplit);
			else
				Gnd.I.Sd.RemarkText_H = -1;

			this.RefreshUi();
		}

		private void 送信ボタンの幅を固定するBToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Gnd.I.Sd.BtnSend_W == -1)
				Gnd.I.Sd.BtnSend_W = GetLeftDistance(this.RemarkSplit);
			else
				Gnd.I.Sd.BtnSend_W = -1;

			this.RefreshUi();
		}

		private void MainWin_Resize(object sender, EventArgs e)
		{
			//this.RefreshUi(); // 廃止?
		}

		private static int GetLeftDistance(SplitContainer sc)
		{
			return GetBound(sc) - sc.SplitterDistance;
		}

		private static void SetLeftDistance(SplitContainer sc, int value)
		{
			int sd = GetBound(sc) - value;

			if (sd < 0)
				return;

			sc.SplitterDistance = sd;
		}

		private static int GetBound(SplitContainer sc)
		{
			if (sc.Orientation == Orientation.Vertical)
				return sc.Width;
			else
				return sc.Height;
		}

		private bool MS_SM_Busy;

		private void MainSplit_SplitterMoved(object sender, SplitterEventArgs e)
		{
			if (this.MS_SM_Busy)
				return;

			this.MS_SM_Busy = true;

			if (Gnd.I.Sd.MemberList_W != -1)
				this.MainSplit.SplitterDistance = Gnd.I.Sd.MemberList_W;

			this.MS_SM_Busy = false;
		}

		private bool TLS_SM_Busy;

		private void TimeLineSplit_SplitterMoved(object sender, SplitterEventArgs e)
		{
			if (this.TLS_SM_Busy)
				return;

			this.TLS_SM_Busy = true;

			if (Gnd.I.Sd.RemarkText_H != -1)
				SetLeftDistance(this.TimeLineSplit, Gnd.I.Sd.RemarkText_H);

			this.TLS_SM_Busy = false;
		}

		private bool RS_SM_Busy;

		private void RemarkSplit_SplitterMoved(object sender, SplitterEventArgs e)
		{
			if (this.RS_SM_Busy)
				return;

			this.RS_SM_Busy = true;

			if (Gnd.I.Sd.BtnSend_W != -1)
				SetLeftDistance(this.RemarkSplit, Gnd.I.Sd.BtnSend_W);

			this.RS_SM_Busy = false;
		}

		private void その他の設定SToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.MT_Enabled = false;
			this.Visible = false;

			using (SettingWin f = new SettingWin())
			{
				f.ShowDialog();
			}
			this.Visible = true;
			this.MT_Enabled = true;

			this.RefreshUi();

			this.SaveLTWH();
			Gnd.I.Sd.Save();

			EventTools.MainWinToTop();

			Gnd.I.FileSvMan.EndAndBegin_MainWin();
			Gnd.I.NamedTrackHttpMan.EndAndBegin_MainWin();
			Gnd.I.NamedTrackMan.EndAndBegin_MainWin();
			Gnd.I.RevClientMan.EndAndBegin_MainWin();
		}

		private void SaveLTWH()
		{
			if (this.WindowState != FormWindowState.Normal)
				return;

			Gnd.I.Sd.MainWin_L = this.Left;
			Gnd.I.Sd.MainWin_T = this.Top;
			Gnd.I.Sd.MainWin_W = this.Width;
			Gnd.I.Sd.MainWin_H = this.Height;
		}

		private void MemberList_SelectedIndexChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void TimeLineText_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void RemarkText_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void BtnSend_Click(object sender, EventArgs e)
		{
			this.DoSend();
		}

		private void ユーザーリストを表示しないUToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Gnd.I.Sd.HideMemberList = Gnd.I.Sd.HideMemberList == false;
			this.RefreshUi();
		}

		private void 送信ボタンを表示しないHToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Gnd.I.Sd.HideBtnSend = Gnd.I.Sd.HideBtnSend == false;
			this.RefreshUi();
		}

		public void SetStatusMessage(string message)
		{
			this.StatusMessage.Text = message;
		}

		private const int CTRL_A = 1;
		private const int KEYCHAR_ENTER = 13;
		private const int KEYCHAR_CTRL_ENTER = 10;

		private void RemarkText_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == CTRL_A)
			{
				this.RemarkText.SelectAll();
				e.Handled = true;
			}
			else if (e.KeyChar == KEYCHAR_ENTER)
			{
				if (Gnd.I.Sd.CtrlEnterで改行_Enterで送信)
				{
					this.DoSend();
					e.Handled = true;
				}
				else
				{
					// noop
				}
			}
			else if (e.KeyChar == KEYCHAR_CTRL_ENTER)
			{
				if (Gnd.I.Sd.CtrlEnterで改行_Enterで送信)
				{
					e.KeyChar = (char)KEYCHAR_ENTER;
				}
				else
				{
					this.DoSend();
					e.Handled = true;
				}
			}
		}

		private void DoSend()
		{
			try
			{
				Gnd.I.TimeLine.DoRemark(this.RemarkText.Text);
				this.RemarkText.Text = "";
				this.RemarkText.Focus();
			}
			catch (Exception e)
			{
				SystemTools.WriteLog(e);
			}
		}

		private void MemberListMenu_Opening(object sender, CancelEventArgs e)
		{
			Heartbeat.MemberData md = Gnd.I.Heartbeat.GetSelected();
			bool flag = false;

			if (md != null)
				flag = md.Message.FileSvEnabled;

			this.MemberListMenu.Items[0].Enabled = flag;
		}

		private void RemarkText_Enter(object sender, EventArgs e)
		{
			this.MemberList.SelectedIndex = -1;
		}

		private void ホームディレクトリを開くHToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Heartbeat.MemberData md = Gnd.I.Heartbeat.GetSelected();

			if (md == null)
				return;

			AppTools.Browse(
				"localhost",//Gnd.I.Sd.ServerDomain, // maybe debugged
				Gnd.I.Sd.NamedTrackHttpPort,
				AppTools.GetUrlPath(md.Message.TrackName, md.Message.FileSvHomeDir, true)
				);
		}

		private void ファイル又はフォルダの貼り付けDToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.MT_Enabled = false;
				this.Visible = false;

				if (Gnd.I.Sd.FileSvEnabled == false)
				{
					MessageBox.Show(
						"この機能を使用するには「設定→その他の設定→ファイル転送→ファイル転送を有効にする」を有効にする必要があります。",
						"この機能は使用できません",
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning
						);
					return;
				}
				this.SaveLTWH();

				using (FileFolderDropWin f = new FileFolderDropWin())
				{
					f.ShowDialog();
					this.DropPaths(f.DroppedPaths);
				}
			}
			finally
			{
				this.Visible = true;
				this.MT_Enabled = true;
			}
		}

		private void DropPaths(List<string> paths)
		{
			int pathNum = Math.Min(paths.Count, 100);

			for (int index = 0; index < pathNum; index += 10)
			{
				List<string> subPaths = paths.GetRange(index, Math.Min(pathNum - index, 10));

				Gnd.I.TimeLine.DoDropPaths(subPaths);
			}
		}

		private void TimeLineText_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			switch (Gnd.I.Sd.TimeLineTextPathClickMode)
			{
				case (int)Consts.PathClickMode_e.確認する:
					if (MessageBox.Show(
						"以下のリンクを開きます。\n" + e.LinkText,
						"リンクを開く",
						MessageBoxButtons.OKCancel,
						MessageBoxIcon.Question
						) != DialogResult.OK
						)
						return;

					AppTools.BrowseUrl(e.LinkText);
					break;

				case (int)Consts.PathClickMode_e.確認せずに開く:
					AppTools.BrowseUrl(e.LinkText);
					break;

				case (int)Consts.PathClickMode_e.何もしない:
					break;
			}
		}
	}
}
