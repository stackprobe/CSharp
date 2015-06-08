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

			new Satellite.Satellizer("", ""); // test
		}

		private void button1_Click(object sender, EventArgs e)
		{
			MessageBox.Show(Satellite.StringTools.Format("AA$BB$CC", "ab", "bc"));
		}
	}
}
