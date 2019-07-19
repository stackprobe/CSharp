using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte.Chocomint.Dialogs
{
	public partial class InputFolderDlg : Form
	{
		public InputFolderDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		private void InputFolderDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void InputFolderDlg_Shown(object sender, EventArgs e)
		{
			//
		}

		private void InputFolderDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void InputFolderDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}
	}
}
