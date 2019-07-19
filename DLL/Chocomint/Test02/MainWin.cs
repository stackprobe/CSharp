using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Chocomint.Dialogs;

namespace Test02
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

		private void BtnGo_Click(object sender, EventArgs e)
		{
			new Program2().Main2();
		}

		private void BtnInputStringDlg_Click(object sender, EventArgs e)
		{
			using (InputStringDlg f = new InputStringDlg())
			{
				f.ShowDialog();
			}
		}
	}
}
