using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TextBoxChiratsukiBoushi
{
	public partial class MainWin : Form
	{
		public MainWin()
		{
			InitializeComponent();
		}

		private string REMARK = "\r\n[2017/10/12 20:47:35] ぽきた◆Pokita @ 127.0.0.1\r\n\r\nぽやしみｗ\r\n";

		private void button1_Click(object sender, EventArgs e)
		{
			// チラつく。

			this.textBox1.Text += REMARK;
			this.textBox1.SelectionStart = this.textBox1.TextLength;
			this.textBox1.ScrollToCaret();
		}

		[DllImport("user32.dll", EntryPoint = "SendMessageA")]
		private static extern uint SendMessage(IntPtr hWnd, uint wMsg, uint wParam, uint lParam);
		private const uint WM_SETREDRAW = 0x000B;

		private void button2_Click(object sender, EventArgs e)
		{
			// チラつく。

			SendMessage(this.textBox1.Handle, WM_SETREDRAW, 0u, 0u);
			this.textBox1.Enabled = false;

			// ----

			this.textBox1.Text += REMARK;
			this.textBox1.SelectionStart = this.textBox1.TextLength;
			this.textBox1.ScrollToCaret();

			// ----

			this.textBox1.Enabled = true;
			SendMessage(this.textBox1.Handle, WM_SETREDRAW, 1u, 0u);
			this.textBox1.Refresh();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			// ほとんどチラつかない！

			this.textBox1.AppendText(REMARK);
			this.textBox1.SelectionStart = this.textBox1.TextLength;
			this.textBox1.ScrollToCaret();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			StringBuilder buff = new StringBuilder();

			for (int c = 0; c < 1000; c++)
				buff.Append(REMARK);

			this.textBox1.AppendText(buff.ToString());
			this.textBox1.SelectionStart = this.textBox1.TextLength;
			this.textBox1.ScrollToCaret();
		}
	}
}
