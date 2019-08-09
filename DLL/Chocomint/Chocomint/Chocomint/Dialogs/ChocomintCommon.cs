using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte.Chocomint.Dialogs
{
	public static class ChocomintCommon
	{
		/// <summary>
		/// Charlotte.Chocomint.Dialogs 直下のフォームは _Shown メソッドの最後に、このメソッドを呼び出さなければならない。
		/// </summary>
		/// <param name="f">呼び出し側のフォーム</param>
		public static void DlgCommonPostShown(Form f)
		{
			PostShown(f);
			見切れ解消(f);
			ChocomintGeneral.OptionalPostShown(f);
		}

		// sync > @ PostShown

		public static void PostShown_GetAllControl(Form f, Action<Control> reaction)
		{
			List<Control.ControlCollection> controlTable = new List<Control.ControlCollection>();

			controlTable.Add(f.Controls);

			for (int index = 0; index < controlTable.Count; index++)
			{
				foreach (Control control in controlTable[index])
				{
					GroupBox gb = control as GroupBox;

					if (gb != null)
					{
						controlTable.Add(gb.Controls);
					}
					TabControl tc = control as TabControl;

					if (tc != null)
					{
						foreach (TabPage tp in tc.TabPages)
						{
							controlTable.Add(tp.Controls);
						}
					}
					SplitContainer sc = control as SplitContainer;

					if (sc != null)
					{
						controlTable.Add(sc.Panel1.Controls);
						controlTable.Add(sc.Panel2.Controls);
					}
					Panel p = control as Panel;

					if (p != null)
					{
						controlTable.Add(p.Controls);
					}
					reaction(control);
				}
			}
		}

		public static void PostShown(Form f)
		{
			PostShown_GetAllControl(f, control =>
			{
				Control c = new Control[]
				{
					control as TextBox,
					control as NumericUpDown,
				}
				.FirstOrDefault(v => v != null);

				if (c != null)
				{
					if (c.ContextMenuStrip == null)
					{
						ToolStripMenuItem item = new ToolStripMenuItem();

						item.Text = "項目なし";
						item.Enabled = false;

						ContextMenuStrip menu = new ContextMenuStrip();

						menu.Items.Add(item);

						c.ContextMenuStrip = menu;
					}
				}
			});
		}

		// < sync

		public static void 見切れ解消(Form f)
		{
			int w = -1;

			PostShown_GetAllControl(f, c =>
			{
				w = Math.Max(w, c.Right);
			});

			w += 10; // margin

			if (f.Width < w)
			{
				f.Left -= (w - f.Width) / 2;
				f.Width = w;
			}
		}
	}
}
