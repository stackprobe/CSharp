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
			// -- 0001

			Ground.I = new Ground();

			this.MainPanel.AutoScroll = true;
			this.MPic_SetSize(800, 600);

			this.South.Text = "";
			this.SouthWest.Text = "";

			this.RefreshUI();

			// ----

			this.MTEnabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MTEnabled = false;

			// ----

			// -- 9999
		}

		private void BeforeDialog()
		{
			this.MTEnabled = false;
		}

		private void AfterDialog()
		{
			this.MTEnabled = true;
		}

		private void CloseWindow()
		{
			this.MTEnabled = false;

			// ----

			// -- 9000

			// ----

			this.Close();
		}

		private bool MTEnabled;
		private bool MTBusy;
		private long MTCount;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MTEnabled == false || this.MTBusy)
				return;

			this.MTBusy = true;

			try
			{
				// -- 3001
			}
			catch (Exception ex)
			{
				ProcMain.WriteLog(ex);
			}
			finally
			{
				this.MTBusy = false;
				this.MTCount++;
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

		private void MPic_SetSize(int w, int h)
		{
			this.MainPicture.Image = new Bitmap(w, h);

			using (Graphics g = Graphics.FromImage(this.MainPicture.Image))
			{
				g.FillRectangle(Brushes.White, 0, 0, w, h);
			}
			this.MainPicture.Bounds = new Rectangle(0, 0, w, h);
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
	}
}
