using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;

namespace WinAfterMainWin
{
	public partial class PseudoMainWin : Form
	{
		#region ALT_F4 抑止

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
				return;

			base.WndProc(ref m);
		}

		#endregion

		private Action MainProc;

		public PseudoMainWin(Action mainProc)
		{
			this.MainProc = mainProc;

			InitializeComponent();
		}

		private void PseudoMainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void PseudoMainWin_Shown(object sender, EventArgs e)
		{
			this.Visible = false;

			this.BeginInvoke((MethodInvoker)delegate
			{
				this.MainProc();
				this.Close();
			});
		}
	}
}
