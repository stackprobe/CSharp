using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Permissions;
using System.Windows.Forms;
using Charlotte.Tools;
using Charlotte.Chocomint.Dialogs;
using Charlotte.Command2;

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

			this.MinimumSize = this.Size;
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

			Ground.I.History.Dispose();
			Ground.I.History = null;

			// -- 9999
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

		private void CloseWindow()
		{
			using (MTBusy.Section())
			{
				// -- 9000

				int ret = InputOptionDlgTools.Show(
					"確認",
					"画像ファイルに保存しますか？",
					"はい:いいえ:キャンセル".Split(':'),
					true
					);

				if (ret == 0)
				{
					if (this.SaveFileUI() == false)
						return;
				}
				else if (ret != 1)
				{
					return;
				}

				// ----

				this.MTBusy.Enter();

				// ----

				// -- 9900

				// ----

				this.Close();
			}
		}

		private void RefreshSouthWest()
		{
			{
				double workingSetMB = Environment.WorkingSet / 1000000.0;

				List<string> tokens = new List<string>();

				tokens.Add(string.Format("{0:F1} MB", workingSetMB));
				tokens.Add("(" + Ground.I.LastNibX + ", " + Ground.I.LastNibY + ")");

				string text = string.Join(", ", tokens);

				if (this.SouthWest.Text != text)
					this.SouthWest.Text = text;
			}
		}

		private void RefreshUI()
		{
			{
				List<string> tokens = new List<string>();

				tokens.Add(this.MainPicture.Image.Width + " x " + this.MainPicture.Image.Height);
				tokens.Add(string.Format("色={0:x8}", Ground.I.NibColor.ToArgb()));
				tokens.Add(string.Format("色2={0:x8}", Ground.I.NibColor2.ToArgb()));
				tokens.Add("ペン先=" + Ground.I.Nib);
				tokens.Add("ペン先2=" + Ground.I.Nib2);
				tokens.Add("Mode=" + (Ground.I.NibRoutine == null ? 0 : 1));
				tokens.Add("Hist=" + Ground.I.History.GetCount() + "_" + Ground.I.History.GetCountForRedo());

				string text = string.Join(", ", tokens);

				if (this.South.Text != text)
					this.South.Text = text;

				Color backColor = Consts.DefaultBackColor;
				Color foreColor = Consts.DefaultForeColor;

				if (Ground.I.NibRoutine != null)
				{
					backColor = Color.DarkCyan;
					foreColor = Color.White;
				}

				if (this.South.BackColor != backColor)
					this.South.BackColor = backColor;

				if (this.South.ForeColor != foreColor)
					this.South.ForeColor = foreColor;
			}

			this.アンチエイリアスToolStripMenuItem.Checked = Ground.I.AntiAliasing;
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

			if (oldImg == img)
				return;

			bool resized = oldImg == null || oldImg.Size != img.Size;

			this.MainPicture.Image = img;
			this.MainPicture.Bounds = new Rectangle(0, 0, 10, 10); // MainPictureの位置がおかしくなる問題の対策
			this.MainPicture.Bounds = new Rectangle(0, 0, img.Width, img.Height);

			if (oldImg != null)
				oldImg.Dispose();

			if (resized)
				Ground.I.History.Clear(); // リサイズしたら履歴は読み込めない。
		}

		private void MPic_Draw(Action<Graphics> routine)
		{
			using (Graphics g = Graphics.FromImage(this.MainPicture.Image))
			{
				if (Ground.I.AntiAliasing)
				{
					g.TextRenderingHint = TextRenderingHint.AntiAlias;
					g.SmoothingMode = SmoothingMode.AntiAlias;
				}
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
			using (this.MTBusy.Section())
			{
				if (Ground.I.NibRoutine != null)
				{
					if (e.Button == System.Windows.Forms.MouseButtons.Left)
					{
						Ground.I.History.Save(this.MainPicture.Image);

						try
						{
							if (Ground.I.NibRoutine(e.X, e.Y))
								Ground.I.NibRoutine = null;
						}
						catch (Exception ex)
						{
							MessageDlgTools.Warning("NibRoutine Error", ex, true);
							Ground.I.NibRoutine = null;
						}
					}
					else
					{
						Ground.I.NibRoutine = null; // モード解除
					}
					this.RefreshUI();
					return;
				}
				Ground.I.History.Save(this.MainPicture.Image);
				Ground.I.NibDown = true;
				//this.RefreshUI(); // moved -> _MouseUp()
			}
		}

		private void MainPicture_MouseUp(object sender, MouseEventArgs e)
		{
			Ground.I.NibDown = false;
			this.RefreshUI();
		}

		private void MainPicture_MouseMove(object sender, MouseEventArgs e)
		{
			int nibX = e.X;
			int nibY = e.Y;

			if (Ground.I.NibDown)
			{
				Ground.Nib_e nib = e.Button == System.Windows.Forms.MouseButtons.Left ? Ground.I.Nib : Ground.I.Nib2;
				Color nibColor = e.Button == System.Windows.Forms.MouseButtons.Left ? Ground.I.NibColor : Ground.I.NibColor2;

				this.MPic_Draw(g =>
				{
					switch (nib)
					{
						case Ground.Nib_e.SIMPLE:
							g.DrawLine(new Pen(nibColor), nibX, nibY, Ground.I.LastNibX, Ground.I.LastNibY);
							break;

						case Ground.Nib_e.THICK:
							g.DrawLine(new Pen(nibColor, 5), nibX, nibY, Ground.I.LastNibX, Ground.I.LastNibY);
							break;

						case Ground.Nib_e.THICK_x2:
							g.DrawLine(new Pen(nibColor, 10), nibX, nibY, Ground.I.LastNibX, Ground.I.LastNibY);
							break;

						case Ground.Nib_e.THICK_x3:
							g.DrawLine(new Pen(nibColor, 15), nibX, nibY, Ground.I.LastNibX, Ground.I.LastNibY);
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
			this.RefreshUI();
		}

		private void MainWin_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
		}

		private void MainWin_DragDrop(object sender, DragEventArgs e)
		{
			using (this.MTBusy.Section())
			{
				if (InputOptionDlgTools.Show("質問", "ドラッグアンドドロップしたファイルをロードしますか？", "はい:いいえ".Split(':'), true) == 0)
				{
					try
					{
						string file = ((string[])e.Data.GetData(DataFormats.FileDrop)).FirstOrDefault(v => File.Exists(v));

						if (file == null)
							throw new Exception("ファイル以外の何か！");

						this.LoadFile(file);
					}
					catch (Exception ex)
					{
						MessageDlgTools.Warning("ファイル読み込み失敗", ex, true);
					}
					GC.Collect();
				}
			}
		}

		private void LoadFile(string file)
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
					g.DrawImage(img, new Rectangle(0, 0, img2.Width, img2.Height));
				}
				this.MPic_SetImage(img2);
				this.RefreshUI();
			}
		}

		private void MPicBoundsBugMenuItem_Click(object sender, EventArgs e)
		{
			Image img = this.MainPicture.Image;

			this.MainPicture.Bounds = new Rectangle(0, 0, img.Width, img.Height);
		}

		private void ファイル読み込みToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (this.MTBusy.Section())
			{
				try
				{
					string file = SaveLoadDialogs.LoadFile(
						"画像ファイルを選択して下さい",
						"画像:bmp.gif.jpg.jpeg.png",
						Ground.I.ActiveImageFile == null ?
							Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Input.png") :
							Ground.I.ActiveImageFile
						);

					if (file != null)
					{
						this.LoadFile(file);
						Ground.I.ActiveImageFile = file;
					}
				}
				catch (Exception ex)
				{
					MessageDlgTools.Warning("ファイル読み込み失敗", ex, true);
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
						Ground.I.ActiveImageFile == null ?
							Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Output.png") :
							Ground.I.ActiveImageFile
						);

					if (file != null)
					{
						this.MainPicture.Image.Save(file, CommonUtils.ExtToImageFormat(Path.GetExtension(file)));

						Ground.I.ActiveImageFile = file;

						MessageDlgTools.Information("成功", "保存しました。", true);
						return true;
					}
				}
				catch (Exception ex)
				{
					MessageDlgTools.Warning("ファイル書き出し失敗", ex, true);
				}
				return false;
			}
		}

		private void アンチエイリアスToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Ground.I.AntiAliasing = Ground.I.AntiAliasing == false;
			this.RefreshUI();
		}

		private void 色ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (MTBusy.Section())
			{
				SaveLoadDialogs.SelectColor(ref Ground.I.NibColor);
			}
			this.RefreshUI();
		}

		private void 色2MenuItem_Click(object sender, EventArgs e)
		{
			using (MTBusy.Section())
			{
				SaveLoadDialogs.SelectColor(ref Ground.I.NibColor2);
			}
			this.RefreshUI();
		}

		private void 透明度ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Ground.I.NibColor = this.Edit透明度(Ground.I.NibColor, "左");
			this.RefreshUI();
		}

		private void 透明度2MenuItem_Click(object sender, EventArgs e)
		{
			Ground.I.NibColor2 = this.Edit透明度(Ground.I.NibColor2, "右");
			this.RefreshUI();
		}

		private Color Edit透明度(Color target, string titleTrailer)
		{
			using (MTBusy.Section())
			{
				int ret = InputTrackBarDlgTools.Show(
					"アルファ値：" + titleTrailer,
					"アルファ値を入力して下さい。(0 ～ 255 = 透明 ～ 不透明)",
					true,
					target.A,
					0,
					255,
					-1,
					null
					);

				if (ret != -1)
				{
					target = Color.FromArgb(
						ret,
						target.R,
						target.G,
						target.B
						);
				}
			}
			return target;
		}

		private void 形ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Ground.I.Nib = this.Edit形(Ground.I.Nib, "左");
			this.RefreshUI();
		}

		private void 形2MenuItem_Click(object sender, EventArgs e)
		{
			Ground.I.Nib2 = this.Edit形(Ground.I.Nib2, "右");
			this.RefreshUI();
		}

		private Ground.Nib_e Edit形(Ground.Nib_e target, string titleTrailer)
		{
			using (MTBusy.Section())
			using (InputComboDlg f = new InputComboDlg())
			{
				f.Value = target;
				f.StartPosition = FormStartPosition.CenterParent;

				f.PostShown = () =>
				{
					f.Text = "ペン先の形状：" + titleTrailer;
					f.Prompt.Text = "ペン先の形状を選択して下さい。";
				};

				foreach (Ground.Nib_e nib in Enum.GetValues(typeof(Ground.Nib_e)))
					f.AddItem(nib, Enum.GetName(typeof(Ground.Nib_e), nib));

				f.ShowDialog();

				if (f.OkPressed)
				{
					target = (Ground.Nib_e)f.Value;
				}
			}
			return target;
		}

		private void MainWin_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)26) // ctrl_z
			{
				this.CtrlZMenuItem_Click(null, null);
				e.Handled = true;
			}
			if (e.KeyChar == (char)25) // ctrl_y
			{
				this.CtrlYMenuItem_Click(null, null);
				e.Handled = true;
			}
			if (e.KeyChar == (char)19) // ctrl_s
			{
				this.SaveFileUI();
				e.Handled = true;
			}
		}

		private void CtrlZMenuItem_Click(object sender, EventArgs e)
		{
			Image image = Ground.I.History.Undo(this.MainPicture.Image);

			if (image != null)
				this.MPic_SetImage(image);

			this.RefreshUI();
		}

		private void CtrlYMenuItem_Click(object sender, EventArgs e)
		{
			Image image = Ground.I.History.Redo(this.MainPicture.Image);

			if (image != null)
				this.MPic_SetImage(image);

			this.RefreshUI();
		}

		private void GCMenuItem_Click(object sender, EventArgs e)
		{
			GC.Collect();
		}

		private void South_Click(object sender, EventArgs e)
		{
			Ground.I.NibRoutine = null; // モード解除
			this.RefreshUI();
		}

		private void 塗りつぶしToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Ground.I.NibRoutine = (targetX, targetY) =>
			{
				Canvas canvas = new Canvas(this.MainPicture.Image);

				canvas.FillSameColor(targetX, targetY, Ground.I.NibColor);

				this.MPic_SetImage(canvas.GetImage());
				return true;
			};

			this.RefreshUI();
		}

		private string ActiveTextureImageFile = "";

		private void テクスチャToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Ground.I.NibRoutine = (targetX, targetY) =>
			{
				string textureImageFile = InputFileDlgTools.Load("テクスチャ画像ファイル入力", "テクスチャ画像ファイルを入力して下さい。", true, this.ActiveTextureImageFile, null, "bmp.gif.jpg.jpeg.png");

				if (textureImageFile != null)
				{
					this.ActiveTextureImageFile = textureImageFile;

					Canvas canvas = new Canvas(this.MainPicture.Image);

					BusyDlgTools.Show("テクスチャ", "テクスチャ処理中...", () =>
					{
						Canvas texture = new Canvas(textureImageFile);

						canvas.SpreadSameColor(targetX, targetY, pt =>
						{
							int x = pt.X;
							int y = pt.Y;

							canvas.Set(x, y, texture.Get(x % texture.GetWidth(), y % texture.GetHeight()));
						});
					},
					true
					);

					this.MPic_SetImage(canvas.GetImage());
				}
				return true;
			};

			this.RefreshUI();
		}

		private string テクスチャ矩形タイルLastInputLine = "";

		private void テクスチャ矩形タイルToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Ground.I.NibRoutine = (targetX, targetY) =>
			{
				string line = InputStringDlgTools.Show("タイルサイズ", "タイルサイズを入力して下さい。(書式 = 幅:高さ:アルファ値下限(0～1), 例 = 30:20:0.5)", true, this.テクスチャ矩形タイルLastInputLine);

				if (line != null)
				{
					this.テクスチャ矩形タイルLastInputLine = line;

					string[] tokens = line.Split(':');
					int tileW = int.Parse(tokens[0]);
					int tileH = int.Parse(tokens[1]);
					double alphaMin = double.Parse(tokens[2]);

					if (
						tileW < 1 || IntTools.IMAX < tileW ||
						tileH < 1 || IntTools.IMAX < tileH ||
						alphaMin < 0.0 || 1.0 < alphaMin
						)
						throw new Exception("不正なタイルサイズ");

					Canvas canvas = new Canvas(this.MainPicture.Image);

					BusyDlgTools.Show("テクスチャ_矩形タイル", "テクスチャ_矩形タイル 処理中...", () =>
					{
						Color targetColor = canvas.Get(targetX, targetY);

						CacheMap<string, Color> tileColors = DictionaryTools.CreateCache<Color>(k =>
						{
							double a = SecurityTools.CRandom.GetReal();

							a = alphaMin + (1.0 - alphaMin) * a;

							Color color = targetColor;

							return Color.FromArgb(
								color.A,
								DoubleTools.ToInt(color.R * a),
								DoubleTools.ToInt(color.G * a),
								DoubleTools.ToInt(color.B * a)
								);
						});

						canvas.SpreadSameColor(targetX, targetY, pt =>
						{
							int x = pt.X;
							int y = pt.Y;

							int tileX = x / tileW;
							int tileY = y / tileH;

							canvas.Set(x, y, tileColors[tileX + ":" + tileY]);
						});
					},
					true
					);

					this.MPic_SetImage(canvas.GetImage());
				}
				return true;
			};

			this.RefreshUI();
		}

		private string テクスチャ矩形タイル2LastInputLine = "";

		private void テクスチャ矩形タイル2ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Ground.I.NibRoutine = (targetX, targetY) =>
			{
				string line = InputStringDlgTools.Show("タイルサイズ(色⇒色2)", "タイルサイズを入力して下さい。(書式 = 幅:高さ, 例 = 30:20)", true, this.テクスチャ矩形タイル2LastInputLine);

				if (line != null)
				{
					this.テクスチャ矩形タイル2LastInputLine = line;

					string[] tokens = line.Split(':');
					int tileW = int.Parse(tokens[0]);
					int tileH = int.Parse(tokens[1]);

					if (
						tileW < 1 || IntTools.IMAX < tileW ||
						tileH < 1 || IntTools.IMAX < tileH
						)
						throw new Exception("不正なタイルサイズ");

					Canvas canvas = new Canvas(this.MainPicture.Image);

					BusyDlgTools.Show("テクスチャ_矩形タイル", "テクスチャ_矩形タイル 処理中...", () =>
					{
						Color targetColor = canvas.Get(targetX, targetY);

						CacheMap<string, Color> tileColors = DictionaryTools.CreateCache<Color>(k =>
						{
							double rate = SecurityTools.CRandom.GetReal();

							return Color.FromArgb(
								DoubleTools.ToInt(Ground.I.NibColor.A + (Ground.I.NibColor2.A - Ground.I.NibColor.A) * rate),
								DoubleTools.ToInt(Ground.I.NibColor.R + (Ground.I.NibColor2.R - Ground.I.NibColor.R) * rate),
								DoubleTools.ToInt(Ground.I.NibColor.G + (Ground.I.NibColor2.G - Ground.I.NibColor.G) * rate),
								DoubleTools.ToInt(Ground.I.NibColor.B + (Ground.I.NibColor2.B - Ground.I.NibColor.B) * rate)
								);
						});

						canvas.SpreadSameColor(targetX, targetY, pt =>
						{
							int x = pt.X;
							int y = pt.Y;

							int tileX = x / tileW;
							int tileY = y / tileH;

							canvas.Set(x, y, tileColors[tileX + ":" + tileY]);
						});
					},
					true
					);

					this.MPic_SetImage(canvas.GetImage());
				}
				return true;
			};

			this.RefreshUI();
		}

		private void PuzzleMenuItem_Click(object sender, EventArgs e)
		{
			using (this.MTBusy.Section())
			{
				try
				{
					Puzzle p = new Puzzle(this.MainPicture.Image);

					Ground.I.NibRoutine = (x, y) =>
					{
						this.MPic_SetImage(p.Routine(this.MainPicture.Image, x, y));

						return false;
					};
				}
				catch (Exception ex)
				{
					MessageDlgTools.Warning("Puzzle 失敗", ex, true);
				}
			}
			this.RefreshUI();
		}

		private void グラデーションToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (this.MTBusy.Section())
			{
				try
				{
					Ground.I.NibRoutine = (targetX, targetY) =>
					{
						var options = new[]
						{
							// { mode, 表示名 }
							new[] { "1", "左から右" },
							new[] { "2", "上から下" },
							new[] { "3", "中心から" },
						};

						int mode = InputOptionDlgTools.Show("グラデーション", "モード？", options.Select(v => v[1]).ToArray(), true);

						if (mode != -1)
						{
							mode = int.Parse(options[mode][0]);

							Canvas canvas = new Canvas(this.MainPicture.Image);

							BusyDlgTools.Show("グラデーション", "グラデーション処理中...", () =>
							{
								Rectangle rect = canvas.GetRectSameColor(targetX, targetY);

								double rrMax = 1.0;
								canvas.SpreadSameColor(targetX, targetY, pt =>
								{
									int xx = pt.X - targetX;
									int yy = pt.Y - targetY;

									rrMax = Math.Max(rrMax, xx * xx + yy * yy);
								});
								double rMax = Math.Sqrt(rrMax);

								canvas.SpreadSameColor(targetX, targetY, pt =>
								{
									int xx = pt.X;
									int yy = pt.Y;

									int x = xx - rect.X;
									int y = yy - rect.Y;

									Color color;

									switch (mode)
									{
										case 1:
											color = Color.FromArgb(
												DoubleTools.ToInt(Ground.I.NibColor.A + (Ground.I.NibColor2.A - Ground.I.NibColor.A) * (double)x / (rect.Width - 1)),
												DoubleTools.ToInt(Ground.I.NibColor.R + (Ground.I.NibColor2.R - Ground.I.NibColor.R) * (double)x / (rect.Width - 1)),
												DoubleTools.ToInt(Ground.I.NibColor.G + (Ground.I.NibColor2.G - Ground.I.NibColor.G) * (double)x / (rect.Width - 1)),
												DoubleTools.ToInt(Ground.I.NibColor.B + (Ground.I.NibColor2.B - Ground.I.NibColor.B) * (double)x / (rect.Width - 1))
												);
											break;

										case 2:
											color = Color.FromArgb(
												DoubleTools.ToInt(Ground.I.NibColor.A + (Ground.I.NibColor2.A - Ground.I.NibColor.A) * (double)y / (rect.Height - 1)),
												DoubleTools.ToInt(Ground.I.NibColor.R + (Ground.I.NibColor2.R - Ground.I.NibColor.R) * (double)y / (rect.Height - 1)),
												DoubleTools.ToInt(Ground.I.NibColor.G + (Ground.I.NibColor2.G - Ground.I.NibColor.G) * (double)y / (rect.Height - 1)),
												DoubleTools.ToInt(Ground.I.NibColor.B + (Ground.I.NibColor2.B - Ground.I.NibColor.B) * (double)y / (rect.Height - 1))
												);
											break;

										case 3:
											int xx2 = xx - targetX;
											int yy2 = yy - targetY;

											double r = Math.Sqrt(xx2 * xx2 + yy2 * yy2);

											r /= rMax;

											color = Color.FromArgb(
												DoubleTools.ToInt(Ground.I.NibColor.A + (Ground.I.NibColor2.A - Ground.I.NibColor.A) * r),
												DoubleTools.ToInt(Ground.I.NibColor.R + (Ground.I.NibColor2.R - Ground.I.NibColor.R) * r),
												DoubleTools.ToInt(Ground.I.NibColor.G + (Ground.I.NibColor2.G - Ground.I.NibColor.G) * r),
												DoubleTools.ToInt(Ground.I.NibColor.B + (Ground.I.NibColor2.B - Ground.I.NibColor.B) * r)
												);
											break;

										default:
											throw null; // never
									}
									canvas.Set(xx, yy, color);
								});
							},
							true
							);

							this.MPic_SetImage(canvas.GetImage());
						}
						return true;
					};
				}
				catch (Exception ex)
				{
					MessageDlgTools.Warning("グラデーション失敗", ex, true);
				}
			}
			this.RefreshUI();
		}
	}
}
