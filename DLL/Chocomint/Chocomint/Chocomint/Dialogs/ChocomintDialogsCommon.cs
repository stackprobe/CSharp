﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte.Chocomint.Dialogs
{
	public static class ChocomintDialogsCommon
	{
		/// <summary>
		/// Charlotte.Chocomint.Dialogs 直下のフォームは _Shown メソッドの最後に、このメソッドを呼び出さなければならない。
		/// </summary>
		/// <param name="f">呼び出し側のフォーム</param>
		public static void DlgCommonPostShown(Form f)
		{
			PostShown(f);
			AntiMessageOutOfWindow(f);
			AntiWindowOutOfScreen(f);
			ChocomintDialogsGeneral.OptionalPostShown(f);
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

		public static void AntiMessageOutOfWindow(Form f)
		{
			int w = -1;

			PostShown_GetAllControl(f, c =>
			{
				if (c is Label) // 今のところ Label のみ
				{
					w = Math.Max(w, c.Right);
				}
			});

			w += 30; // margin

			if (f.Width < w)
			{
				f.Left -= (w - f.Width) / 2;
				f.Width = w;
			}
		}

		public static void AntiWindowOutOfScreen(Form f)
		{
			Screen screen = GetScreen_Inside(f);

			if (screen == null)
				return;

			I4Rect winRect = new I4Rect(f.Left, f.Top, f.Width, f.Height);
			I4Rect scrRect = new I4Rect(
				screen.Bounds.Left,
				screen.Bounds.Top,
				screen.Bounds.Width,
				screen.Bounds.Height
				);

			winRect.L = Math.Min(winRect.L, scrRect.R - winRect.W);
			winRect.T = Math.Min(winRect.T, scrRect.B - winRect.H);

			winRect.L = Math.Max(winRect.L, scrRect.L);
			winRect.T = Math.Max(winRect.T, scrRect.T);

			if (f.Left != winRect.L)
				f.Left = winRect.L;

			if (f.Top != winRect.T)
				f.Top = winRect.T;
		}

		private static Screen GetScreen_Inside(Form f)
		{
			I2Point winCenter = new I2Point((f.Left + f.Right) / 2, (f.Top + f.Bottom) / 2);

			foreach (Screen screen in Screen.AllScreens)
			{
				I4Rect scrRect = new I4Rect(
					screen.Bounds.Left,
					screen.Bounds.Top,
					screen.Bounds.Width,
					screen.Bounds.Height
					);

				if (
					scrRect.L <= winCenter.X && winCenter.X < scrRect.R &&
					scrRect.T <= winCenter.Y && winCenter.Y < scrRect.B
					)
					return screen;
			}
			return null;
		}
	}
}