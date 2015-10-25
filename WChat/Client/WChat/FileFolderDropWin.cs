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
	public partial class FileFolderDropWin : Form
	{
		public List<string> DroppedPaths = new List<string>(); // {} == キャンセルした。

		public FileFolderDropWin()
		{
			InitializeComponent();

			this.TopMost = Gnd.I.Sd.MainWinAlwaysTop; // XXX
		}

		private void FileFolderDropWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void FileFolderDropWin_Shown(object sender, EventArgs e)
		{
			this.Left = Gnd.I.Sd.MainWin_L;
			this.Top = Gnd.I.Sd.MainWin_T;
			this.Width = Gnd.I.Sd.MainWin_W;
			this.Height = Gnd.I.Sd.MainWin_H;
		}

		private void FileFolderDropWin_Resize(object sender, EventArgs e)
		{
			this.MainLabel.Left = (this.Width - this.MainLabel.Width) / 2;
			this.MainLabel.Top = this.Height / 2 - 50;
		}

		private void FileFolderDropWin_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void FileFolderDropWin_DragDrop(object sender, DragEventArgs e)
		{
			this.DroppedPaths.AddRange((string[])e.Data.GetData(DataFormats.FileDrop, false));
			this.Close();
		}
	}
}
