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
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.RtbMan.Clear();
		}

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
				"[2018/04/22 23:57:30]"
				));
			dest.Add(new RtbMan.Token(
				new Font("メイリオ", 10f),
				Color.Black,
				" "
				));
			dest.Add(new RtbMan.Token(
				new Font("メイリオ", 20f),
				Color.DarkOrange,
				"ほげいん"
				));
			dest.Add(new RtbMan.Token(
				new Font("メイリオ", 10f),
				Color.Black,
				"\n"
				));
			dest.Add(new RtbMan.Token(
				new Font("メイリオ", 10f),
				Color.DarkGreen,
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
			dest.AddRange(this.GetRemark());

			this.RtbMan.Add(dest);
			this.RtbMan.ScrollToBottom();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			List<RtbMan.Token> dest = this.GetRemark();

			for (int c = 0; c < 300; c++)
				dest.AddRange(this.GetRemark());

			this.RtbMan.Add(dest);
			this.RtbMan.ScrollToBottom();
		}
	}
}
