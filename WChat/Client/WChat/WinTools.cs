using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Charlotte
{
	public class WinTools
	{
		public static void SetVisible(Control ctrl, bool flag)
		{
			if (ctrl.Visible != flag)
				ctrl.Visible = flag;
		}

		public static void SetEnabled(Control ctrl, bool flag)
		{
			if (ctrl.Enabled != flag)
				ctrl.Enabled = flag;
		}

		public static void SetText(Control ctrl, string text)
		{
			if (ctrl.Text != text)
				ctrl.Text = text;
		}

		public static T GetControl<T>(List<Control> ctrls) where T : class
		{
			return ArrayTools.GetOne<T, Control>(ctrls);
		}

		public static List<T> GetControls<T>(List<Control> ctrls) where T : class
		{
			return ArrayTools.GetPlur<T, Control>(ctrls);
		}

		public static TabPage GetTabPage(List<Control> ctrls, string text)
		{
			foreach (Control ctrl in ctrls)
			{
				TabPage ret = ctrl as TabPage;

				if (ret == null)
					continue;

				if (ret.Text != text)
					continue;

				return ret;
			}
			return null; // not found
		}

		public static List<Control> GetAllControl(Control target)
		{
			List<Control> dest = new List<Control>();
			GetAllControl(target, dest);
			return dest;
		}

		private static void GetAllControl(Control target, List<Control> dest)
		{
			if (target == null)
				return;

			if (dest.Contains(target))
				return;

			dest.Add(target);

			// ---- nest ----

			GetAllControl(target.Parent, dest);

			foreach (Control child in target.Controls)
				GetAllControl(child, dest);

			// ----
		}

		public static List<Control> GetAllParent(Control target)
		{
			List<Control> dest = new List<Control>();

			target = target.Parent;

			while (target == null)
			{
				dest.Add(target);
				target = target.Parent;
			}
			return dest;
		}

		public static List<Control> GetAllChild(Control target)
		{
			List<Control> dest = new List<Control>();
			GetAllChild(target, dest);
			return dest;
		}

		private static void GetAllChild(Control target, List<Control> dest)
		{
			foreach (Control child in target.Controls)
			{
				dest.Add(child);
				GetAllChild(child, dest);
			}
		}
	}
}
