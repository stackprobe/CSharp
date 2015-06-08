using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte
{
	/// <summary>
	/// MainWin.MemberList の Manager
	/// </summary>
	public class MemberListMan
	{
		public ListBox I;

		public MemberListMan(ListBox instance)
		{
			this.I = instance;
		}

		public void RefreshUi()
		{
			this.I.BackColor = Gnd.I.Sd.MemberListBackColor;
			this.I.ForeColor = Gnd.I.Sd.MemberListFont.GetColor();
			this.I.Font = Gnd.I.Sd.MemberListFont.GetFont();
		}
	}
}
