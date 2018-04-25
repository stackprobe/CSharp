using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RTBChiratsukiBoushi
{
	public partial class MainWin : Form
	{
		public MainWin()
		{
			InitializeComponent();
		}

		private RtbMan RtbMan;

		private void MainWin_Load(object sender, EventArgs e)
		{
			this.RtbMan = new RtbMan(this.MainRTB, this.button1);
			this.RtbMan.Clear();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.RtbMan.Clear();
		}

		private Random Random = new Random();

		private List<RtbMan.Token> GetRemark()
		{
			List<RtbMan.Token> dest = new List<RtbMan.Token>();

			dest.Add(new RtbMan.Token(
				new Font("メイリオ", 10f),
				Color.Black,
				"\n\n"
				));
			dest.Add(new RtbMan.Token(
				new Font("Consolas", 10f),
				Color.Black,
				"[" + DateTime.Now + "]"
				));
			dest.Add(new RtbMan.Token(
				new Font("メイリオ", 10f),
				Color.Black,
				" "
				));
			dest.Add(new RtbMan.Token(
				new Font("メイリオ", 20f),
				Color.DarkOrange,
				"ほげいん" + this.Random.Next(100) + "." + this.Random.Next(100)
				));
			dest.Add(new RtbMan.Token(
				new Font("メイリオ", 10f),
				Color.Black,
				"\n"
				));
			dest.Add(new RtbMan.Token(
				new Font("メイリオ", 10f),
				new Color[] {
					Color.DarkRed, Color.DarkGreen, Color.DarkBlue,
					Color.DarkOrange, Color.DarkGoldenrod, Color.DarkGray,
					Color.DarkCyan, Color.IndianRed, Color.Navy,
				}
				[this.Random.Next(9)],
				"ほげいん\nほげいんほげいん\nほげいんほげいんほげいん"
				));

			return dest;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.RtbMan.Add(this.GetRemark());
			this.RtbMan.ScrollToBottom();
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			this.RtbMan.Set行間を詰める(this.checkBox1.Checked);
			this.RtbMan.Add(new List<RtbMan.Token>());
		}

		private void button3_Click(object sender, EventArgs e)
		{
			List<RtbMan.Token> dest = this.GetRemark();
			dest.AddRange(this.GetRemark());
			dest.AddRange(this.GetRemark());

			this.RtbMan.Add(dest);
			this.RtbMan.ScrollToBottom();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			List<RtbMan.Token> dest = this.GetRemark();

			for (int c = 1; c < 300; c++)
				dest.AddRange(this.GetRemark());

			this.RtbMan.Add(dest);
			this.RtbMan.ScrollToBottom();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			this.RtbMan.Cut(this.MainRTB.TextLength / 2);

			// 行数が増えない場合は、上にスクロールが必要。
			// -- テキストの終端までスクロールした状態｜それよりも下にスクロールした状態で ScrollToBottom すると変な位置にスクロールしてしまう。

			this.RtbMan.ScrollToTop();
			this.RtbMan.ScrollToBottom();
		}

		private void button6_Click(object sender, EventArgs e)
		{
			this.RtbMan.ScrollToTop();
			this.RtbMan.ScrollToBottom();
		}

		private string SavedRtf = null;

		private void button7_Click(object sender, EventArgs e)
		{
			this.SavedRtf = this.MainRTB.Rtf;
		}

		private void button8_Click(object sender, EventArgs e)
		{
			if (this.SavedRtf == null)
				return;

			// Saveした時よりも増えるはずなので ScrollToTop は不要。なはず...

			List<RtbMan.Token> dest = this.GetRemark();
			dest.AddRange(this.GetRemark());
			dest.AddRange(this.GetRemark());

			this.RtbMan.Join(this.SavedRtf, dest);
			this.RtbMan.ScrollToBottom();
		}
	}
}
