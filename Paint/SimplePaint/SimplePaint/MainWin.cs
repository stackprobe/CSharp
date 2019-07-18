using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using System.Security.Permissions;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	public partial class MainWin : Form
	{
		#region ALT_F4 抑止

		private bool XPressed = false;

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
			{
				this.XPressed = true;
				return;
			}
			base.WndProc(ref m);
		}

		#endregion

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
			// -- 0001

			Ground.I = new Ground();

			this.MainPanel.AutoScroll = true;
			this.MPic_SetSize(800, 600);

			this.South.Text = "";
			this.SouthWest.Text = "";

			this.RefreshUI();

			// ----

			this.MTBusy.Leave();
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MTBusy.Enter();

			// ----

			// -- 9999
		}

		private void CloseWindow()
		{
			using (MTBusy.Section())
			{
				// -- 9000

				DialogResult ret = MessageBox.Show(
					"画像ファイルに保存しますか？",
					"確認",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question
					);

				if (ret == System.Windows.Forms.DialogResult.Cancel)
					return;

				if (ret == System.Windows.Forms.DialogResult.Yes)
					if (this.SaveFileUI() == false)
						return;

				// ----

				this.MTBusy.Enter();

				// ----

				// -- 9900

				// ----

				this.Close();
			}
		}

		private VisitorCounter MTBusy = new VisitorCounter(1);
		private long MTCount;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MTBusy.HasVisitor())
				return;

			this.MTBusy.Enter();
			try
			{
				if (this.XPressed)
				{
					this.XPressed = false;
					this.CloseWindow();
					return;
				}
				this.RefreshSouthWest();
			}
			catch (Exception ex)
			{
				ProcMain.WriteLog(ex);
			}
			finally
			{
				this.MTBusy.Leave();
				this.MTCount++;
			}
		}

		private void RefreshSouthWest()
		{
			{
				string text = "(" + Ground.I.LastNibX + ", " + Ground.I.LastNibY + ")";

				if (this.SouthWest.Text != text)
					this.SouthWest.Text = text;
			}
		}

		private void RefreshUI()
		{
			{
				string text = this.MainPicture.Image.Width + " x " + this.MainPicture.Image.Height;

				if (this.South.Text != text)
					this.South.Text = text;
			}
		}

		private void MainPicture_Click(object sender, EventArgs e)
		{
			// noop
		}

		// ---- MPic_ ----

		private void MPic_SetSize(Size size)
		{
			this.MPic_SetSize(size.Width, size.Height);
		}

		private void MPic_SetSize(int w, int h)
		{
			w = IntTools.Range(w, Consts.MPIC_W_MIN, Consts.MPIC_W_MAX);
			h = IntTools.Range(h, Consts.MPIC_H_MIN, Consts.MPIC_H_MAX);

			Image img = new Bitmap(w, h);

			using (Graphics g = Graphics.FromImage(img))
			{
				g.FillRectangle(Brushes.White, 0, 0, w, h);
			}
			this.MPic_SetImage(img);
		}

		private void MPic_SetImage(Image img)
		{
			Image oldImg = this.MainPicture.Image;

			this.MainPicture.Image = img;
			this.MainPicture.Bounds = new Rectangle(0, 0, img.Width, img.Height);

			if (oldImg != null)
				oldImg.Dispose();
		}

		private void MPic_Draw(Action<Graphics> routine)
		{
			using (Graphics g = Graphics.FromImage(this.MainPicture.Image))
			{
				routine(g);
			}
			this.MainPicture.Invalidate();
		}

		// ----

		private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.CloseWindow();
		}

		private void クリアToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.MPic_Draw(g => g.FillRectangle(Brushes.White, 0, 0, this.MainPicture.Image.Width, this.MainPicture.Image.Height));
		}

		private void MainPanel_Paint(object sender, PaintEventArgs e)
		{
			// noop
		}

		private void MainPicture_MouseDown(object sender, MouseEventArgs e)
		{
			Ground.I.NibDown = true;
		}

		private void MainPicture_MouseUp(object sender, MouseEventArgs e)
		{
			Ground.I.NibDown = false;
		}

		private void MainPicture_MouseMove(object sender, MouseEventArgs e)
		{
			int nibX = e.X;
			int nibY = e.Y;

			if (Ground.I.NibDown)
			{
				this.MPic_Draw(g =>
				{
					switch (Ground.I.Nib)
					{
						case Ground.Nib_e.SIMPLE:
							g.DrawLine(new Pen(Ground.I.NibColor), nibX, nibY, Ground.I.LastNibX, Ground.I.LastNibY);
							break;

						default:
							throw null; // never
					}
				});
			}

			Ground.I.LastNibX = nibX;
			Ground.I.LastNibY = nibY;
		}

		private void サイズ変更ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (this.MTBusy.Section())
			using (InputSizeDlg f = new InputSizeDlg())
			{
				f.RefSize = this.MainPicture.Image.Size;
				f.ShowDialog();

				if (f.OkPressed)
				{
					this.MPic_SetSize(f.RefSize);
				}
			}
		}

		private void ファイル読み込みToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (this.MTBusy.Section())
			{
				try
				{
					string file = SaveLoadDialogs.LoadFile(
						"画像ファイルを選択して下さい",
						"",
						Ground.I.ActiveImageFile == null ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : Path.GetDirectoryName(Ground.I.ActiveImageFile),
						Ground.I.ActiveImageFile == null ? "Input.png" : Path.GetFileName(Ground.I.ActiveImageFile),
						dlg => dlg.Filter = "画像ファイル(*.bmp;*.gif;*.jpg;*.jpeg;*.png)|*.bmp;*.gif;*.jpg;*.jpeg;*.png|すべてのファイル(*.*)|*.*"
						);

					if (file != null)
					{
						using (Image img = Image.FromFile(file))
						{
							if (img.Width != IntTools.Range(img.Width, Consts.MPIC_W_MIN, Consts.MPIC_W_MAX))
								throw new Exception("画像の幅に問題があります。");

							if (img.Height != IntTools.Range(img.Height, Consts.MPIC_H_MIN, Consts.MPIC_H_MAX))
								throw new Exception("画像の高さに問題があります。");

							Image img2 = new Bitmap(img.Width, img.Height);

							// ここで例外投げると img2 が宙に浮く -> GC

							using (Graphics g = Graphics.FromImage(img2))
							{
								g.FillRectangle(Brushes.White, 0, 0, img2.Width, img2.Height);
								g.DrawImage(img, 0, 0);
							}
							this.MPic_SetImage(img2);
						}
						Ground.I.ActiveImageFile = file;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("" + ex, "ファイル読み込み失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				GC.Collect();
			}
		}

		private void ファイル書き出しToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.SaveFileUI();
		}

		private bool SaveFileUI()
		{
			using (this.MTBusy.Section())
			{
				try
				{
					string file = SaveLoadDialogs.SaveFile(
						"保存するファイルを入力して下さい",
						"bmp.gif.jpg.jpeg.png",
						Ground.I.ActiveImageFile == null ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : Path.GetDirectoryName(Ground.I.ActiveImageFile),
						Ground.I.ActiveImageFile == null ? "Output.png" : Path.GetFileName(Ground.I.ActiveImageFile),
						dlg => dlg.FilterIndex = 5
						);

					if (file != null)
					{
						this.MainPicture.Image.Save(file, CommonUtils.ExtToImageFormat(Path.GetExtension(file)));

						Ground.I.ActiveImageFile = file;

						MessageBox.Show("保存しました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
						return true;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("" + ex, "ファイル書き出し失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				return false;
			}
		}
	}
}
