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
			this.RefreshUi();
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			Gnd.I.DoSave();
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void 設定SToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Gnd.I.ConsoleProcEnd();

			using (SettingWin f = new SettingWin())
			{
				f.ShowDialog();
			}
			Gnd.I.DoSave();
			this.RefreshUi();
		}

		private void 開始SToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Gnd.I.ServerStartFlag = true;
			this.RefreshUi();
		}

		private void 停止TToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Gnd.I.ServerStartFlag = false;
			this.RefreshUi();
		}

		private void ファイル転送サーバーFToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Gnd.I.RevServerEnabled = Gnd.I.RevServerEnabled == false;
			Gnd.I.DoSave();
			this.RefreshUi();
		}

		private void RefreshUi()
		{
			this.開始SToolStripMenuItem.Checked = Gnd.I.ServerStartFlag;
			this.停止TToolStripMenuItem.Checked = Gnd.I.ServerStartFlag == false;
			this.ファイル転送サーバーFToolStripMenuItem.Checked = Gnd.I.RevServerEnabled;

			{
				List<string> l = new List<string>();

				if (Gnd.I.ServerStartFlag)
				{
					l.Add("サーバーは開始されました。");

					if (Gnd.I.RevServerEnabled)
						l.Add("ファイル転送サーバーは有効です。");
					else
						l.Add("ファイル転送サーバーは無効です。");
				}
				else
					l.Add("サーバーは停止しています。");

				this.MainText.Text = string.Join("\r\n", l);
				this.MainText.SelectionStart = this.MainText.Text.Length;
			}

			Gnd.I.ConsoleProcBegin();
		}
	}
}
