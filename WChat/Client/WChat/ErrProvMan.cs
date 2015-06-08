using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte
{
	public class ErrProvMan
	{
		private ErrorProvider ErrProv;
		private bool ErrorFlag;

		public ErrProvMan(ErrorProvider errProv)
		{
			this.ErrProv = errProv;
		}

		public void Clear()
		{
			this.ErrProv.Clear();
			this.ErrorFlag = false;
		}

		public void SetError(Control ctrl, string message)
		{
			if (this.ErrorFlag == false)
			{
				this.ErrorFlag = true;

				TabPage tp = (TabPage)ctrl.Parent;
				TabControl tc = (TabControl)tp.Parent;

				tc.SelectedTab = tp;
			}
			this.ErrProv.SetError(ctrl, message);
		}

		public bool HasError()
		{
			return this.ErrorFlag;
		}

		public void Check(TextBox tb, int minval = 0, int maxval = int.MaxValue)
		{
			try
			{
				if (IntTools.IsRange(int.Parse(tb.Text), minval, maxval) == false)
					throw new SystemTools.Discontinued();
			}
			catch
			{
				this.SetError(tb, minval + "～" + maxval + " の整数を指定して下さい。");
			}
		}

		public void Check(TextBox tb, string validChrs, int minlen = 0, int maxlen = int.MaxValue)
		{
			tb.Text = tb.Text.Trim(); // XXX

			if (StringTools.ContainsOnly(tb.Text, validChrs) == false)
			{
				this.SetError(tb, "使用できない文字が含まれています。\n使用可能な文字は " + validChrs);
			}
			this.CheckLen(tb, minlen, maxlen);
		}

		public void Check(TextBox tb, bool okJpn, bool okRet, bool okTab, bool okSpc, int minlen = 0, int maxlen = int.MaxValue)
		{
			tb.Text = tb.Text.Trim(); // XXX

			if (JString.IsJString(tb.Text, okJpn, okRet, okTab, okSpc) == false)
			{
				List<string> valids = new List<string>();

				valids.Add("半角英数記号");
				if (okJpn) valids.Add("CP932に含まれる全角文字");
				if (okRet) valids.Add("改行");
				if (okTab) valids.Add("タブ");
				if (okSpc) valids.Add("空白");

				this.SetError(tb, "使用できない文字が含まれています。\n使用可能な文字は " + string.Join(", ", valids));
			}
			this.CheckLen(tb, minlen, maxlen);
		}

		public void CheckLen(TextBox tb, int minlen, int maxlen = int.MaxValue)
		{
			if (tb.Text.Length < minlen)
			{
				this.SetError(tb, minlen + "文字より短くすることはできません。");
			}
			if (maxlen < tb.Text.Length)
			{
				this.SetError(tb, maxlen + "文字より長くすることはできません。");
			}
		}
	}
}
