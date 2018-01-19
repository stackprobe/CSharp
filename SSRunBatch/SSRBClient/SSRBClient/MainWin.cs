using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Charlotte
{
	public partial class MainWin : Form
	{
		public MainWin()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;

			this.Batch.Text = "DIR";
			this.Response.Text = "";

			this.EastStatus.Text = "";
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.UILoad();
			this.UIRefresh();
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void UILoad()
		{
			this.ServerDomain.Text = Gnd.I.ServerDomain;
			this.ServerPortNo.Text = "" + Gnd.I.ServerPortNo;
		}

		private void UIRefresh()
		{
			this.SetItems(this.SendFile_削除DToolStripMenuItem, Gnd.I.SendFiles, " <<< ");
			this.SetItems(this.RecvFile_削除DToolStripMenuItem, Gnd.I.RecvFiles, " >>> ");

			this.Status.Text = Gnd.I.Status;

			if (Gnd.I.StatusErrorFlag)
			{
				this.Status.ForeColor = Color.White;
				this.Status.BackColor = Color.DarkRed;
			}
			else
			{
				this.Status.ForeColor = new ToolStripStatusLabel().ForeColor;
				this.Status.BackColor = new ToolStripStatusLabel().BackColor;
			}
		}

		private void SetItems(ToolStripMenuItem menuItem, List<string> files, string s_indicator)
		{
			menuItem.DropDownItems.Clear();

			foreach (string file in files)
			{
				menuItem.DropDownItems.Add(Path.GetFileName(file) + s_indicator + file);
			}
			for (int index = 0; index < menuItem.DropDownItems.Count; index++)
			{
				this.PutDeleteEvent(menuItem.DropDownItems[index], files, files[index]);
			}
			menuItem.Enabled = 1 <= files.Count;
		}

		private void PutDeleteEvent(ToolStripItem item, List<string> files, string file)
		{
			item.Click += new EventHandler(delegate
			{
				files.Remove(file);

				this.UIRefresh();
			});
		}

		private void Batch_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void Batch_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)1) // ctrl_a
			{
				this.Batch.SelectAll();
				e.Handled = true;
			}
			else if (e.KeyChar == (char)10) // ctrl_enter
			{
				this.BtnRun_Click(null, null);
				e.Handled = true;
			}
		}

		private void Response_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void Response_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)1) // ctrl_a
			{
				this.Response.SelectAll();
				e.Handled = true;
			}
		}

		private string LastSendFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "send.dat");

		private void SendFile_追加AToolStripMenuItem_Click(object sender, EventArgs e)
		{
			for (; ; )
			{
				//OpenFileDialogクラスのインスタンスを作成
				using (OpenFileDialog ofd = new OpenFileDialog())
				{
					//はじめのファイル名を指定する
					//はじめに「ファイル名」で表示される文字列を指定する
					ofd.FileName = Path.GetFileName(this.LastSendFile);
					//はじめに表示されるフォルダを指定する
					//指定しない（空の文字列）の時は、現在のディレクトリが表示される
					ofd.InitialDirectory = Path.GetDirectoryName(this.LastSendFile);
					//[ファイルの種類]に表示される選択肢を指定する
					//指定しないとすべてのファイルが表示される
					ofd.Filter = "すべてのファイル(*.*)|*.*";
					//[ファイルの種類]ではじめに選択されるものを指定する
					//2番目の「すべてのファイル」が選択されているようにする
					ofd.FilterIndex = 1;
					//タイトルを設定する
					ofd.Title = "送信ファイルを選択してください";
					//ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
					ofd.RestoreDirectory = true;
					//存在しないファイルの名前が指定されたとき警告を表示する
					//デフォルトでTrueなので指定する必要はない
					ofd.CheckFileExists = true;
					//存在しないパスが指定されたとき警告を表示する
					//デフォルトでTrueなので指定する必要はない
					ofd.CheckPathExists = true;

					//ダイアログを表示する
					if (ofd.ShowDialog() == DialogResult.OK)
					{
						//OKボタンがクリックされたとき、選択されたファイル名を表示する
						//Console.WriteLine(ofd.FileName);
						this.LastSendFile = ofd.FileName;

						try
						{
							this.CheckSendFile(this.LastSendFile);
							Gnd.I.SendFiles.Add(this.LastSendFile);
						}
						catch (Exception ex)
						{
							MessageBox.Show(this, "" + ex, "送信ファイルを追加出来ません", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							continue;
						}
					}
				}
				break;
			}
			this.UIRefresh();
		}

		private void CheckSendFile(string file)
		{
			string lFile = Path.GetFileName(file);

			if (lFile != lFile.Trim())
				throw new Exception("ローカル名の最初または終端に空白があります。");

			if (lFile != Consts.ENCODING_SJIS.GetString(Consts.ENCODING_SJIS.GetBytes(lFile)))
				throw new Exception("ローカル名に Shift_JIS に変換出来ない文字が含まれています。");

			if (Utils.ContainsLPath(Gnd.I.SendFiles, lFile))
				throw new Exception("ローカル名が重複しています。");
		}

		private string LastRecvFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "recv.dat");

		private void RecvFile_追加AToolStripMenuItem_Click(object sender, EventArgs e)
		{
			for (; ; )
			{
				//SaveFileDialogクラスのインスタンスを作成
				using (SaveFileDialog sfd = new SaveFileDialog())
				{
					//はじめのファイル名を指定する
					//はじめに「ファイル名」で表示される文字列を指定する
					sfd.FileName = Path.GetFileName(this.LastRecvFile);
					//はじめに表示されるフォルダを指定する
					sfd.InitialDirectory = Path.GetDirectoryName(this.LastRecvFile);
					//[ファイルの種類]に表示される選択肢を指定する
					//指定しない（空の文字列）の時は、現在のディレクトリが表示される
					sfd.Filter = "すべてのファイル(*.*)|*.*";
					//[ファイルの種類]ではじめに選択されるものを指定する
					//2番目の「すべてのファイル」が選択されているようにする
					sfd.FilterIndex = 1;
					//タイトルを設定する
					sfd.Title = "受信ファイル(保存先パス)を入力してください";
					//ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
					sfd.RestoreDirectory = true;
					//既に存在するファイル名を指定したとき警告する
					//デフォルトでTrueなので指定する必要はない
					sfd.OverwritePrompt = true;
					//存在しないパスが指定されたとき警告を表示する
					//デフォルトでTrueなので指定する必要はない
					sfd.CheckPathExists = true;

					//ダイアログを表示する
					if (sfd.ShowDialog() == DialogResult.OK)
					{
						//OKボタンがクリックされたとき、選択されたファイル名を表示する
						//Console.WriteLine(sfd.FileName);
						this.LastRecvFile = sfd.FileName;

						try
						{
							this.CheckRecvFile(this.LastRecvFile);
							Gnd.I.RecvFiles.Add(this.LastRecvFile);
						}
						catch (Exception ex)
						{
							MessageBox.Show(this, "" + ex, "受信ファイルを追加出来ません", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							continue;
						}
					}
				}
				break;
			}
			this.UIRefresh();
		}

		private void CheckRecvFile(string file)
		{
			string lFile = Path.GetFileName(file);

			if (lFile != lFile.Trim())
				throw new Exception("ローカル名の最初または終端に空白があります。");

			if (lFile != Consts.ENCODING_SJIS.GetString(Consts.ENCODING_SJIS.GetBytes(lFile)))
				throw new Exception("ローカル名に Shift_JIS に変換出来ない文字が含まれています。");
		}

		private void BtnRun_Click(object sender, EventArgs e)
		{
			DateTime startedTime = DateTime.Now;

			try
			{
				// UISave
				Gnd.I.ServerDomain = this.ServerDomain.Text;
				Gnd.I.ServerPortNo = Utils.ToInt(this.ServerPortNo.Text, 1, 65535, "接続先ポート番号に問題があります。");

				string[] commands;

				{
					string batch = this.Batch.Text;

					if (batch != Consts.ENCODING_SJIS.GetString(Consts.ENCODING_SJIS.GetBytes(batch)))
						throw new Exception("Batch に Shift_JIS に変換出来ない文字が含まれています。");

					{
						string workFile = Path.Combine(Environment.GetEnvironmentVariable("TMP"), "{3f77c059-f824-44c2-9d39-269a392d61b2}");

						File.WriteAllText(workFile, batch, Consts.ENCODING_SJIS);
						commands = File.ReadAllLines(workFile, Consts.ENCODING_SJIS);
						File.Delete(workFile);
					}
				}

				{
					BatchClient client = new BatchClient()
					{
						Domain = Gnd.I.ServerDomain,
						PortNo = Gnd.I.ServerPortNo,
						SendFiles = Gnd.I.SendFiles.ToArray(),
						RecvFiles = Gnd.I.RecvFiles.ToArray(),
						Commands = commands,
					};

					BusyDlg.Perform(this, delegate
					{
						client.Perform();
					});

					this.Response.Text = string.Join("\r\n", client.OutLines);
					this.Response.SelectionStart = this.Response.Text.Length;
					this.Response.ScrollToCaret();
				}

				Gnd.I.Status = "成功 @ " + Utils.ToString_Span(startedTime, DateTime.Now);
				Gnd.I.StatusErrorFlag = false;
			}
			catch (Exception ex)
			{
				Gnd.I.Status = "失敗 @ " + Utils.ToString_Span(startedTime, DateTime.Now);
				Gnd.I.StatusErrorFlag = true;

				this.Response.Clear();

				while (ex is RelayException)
					ex = ex.InnerException;

				MessageBox.Show(this, "" + ex, "失敗しました", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			this.UIRefresh();
		}
	}
}
