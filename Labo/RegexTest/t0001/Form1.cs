using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace t0001
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.DoRegex();
		}

		private void Str_TextChanged(object sender, EventArgs e)
		{
			this.DoRegex();
		}

		private void Exp_TextChanged(object sender, EventArgs e)
		{
			this.DoRegex();
		}

		private void IgnCase_CheckedChanged(object sender, EventArgs e)
		{
			this.DoRegex();
		}

		private void DoRegex()
		{
			string sRes;

			try
			{
				bool res;

				if (this.IgnCase.Checked)
				{
					res = Regex.IsMatch(this.Str.Text, this.Exp.Text, RegexOptions.IgnoreCase);
				}
				else
				{
					res = Regex.IsMatch(this.Str.Text, this.Exp.Text);
				}
				sRes = res.ToString();
			}
			catch (Exception e)
			{
				sRes = e.Message;
			}
			this.Res.Text = DateTime.Now + " " + sRes;
		}
	}
}
