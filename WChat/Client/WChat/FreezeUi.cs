using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Charlotte
{
	public class FreezeUi : IDisposable
	{
		[DllImport("user32.dll", EntryPoint = "SendMessageA")]
		private static extern uint SendMessage(IntPtr hWnd, uint wMsg, uint wParam, uint lParam);

		private const Int32 WM_SETREDRAW = 0x000B;

		private static void SetWindowRedraw(IWin32Window window, bool fRedraw)
		{
			if ((window != null) && (window.Handle != IntPtr.Zero))
			{
				SendMessage(window.Handle, (uint)WM_SETREDRAW, (fRedraw) ? 1u : 0u, 0u);
			}
		}

		private Control Target;

		public FreezeUi(Control target)
		{
			SetWindowRedraw(target, false);
			this.Target = target;
		}

		public void Dispose()
		{
			if (this.Target != null)
			{
				SetWindowRedraw(this.Target, true);
				this.Target.Refresh();
				this.Target = null;
			}
		}
	}
}
