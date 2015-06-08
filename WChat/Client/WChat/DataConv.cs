using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;

namespace Charlotte
{
	public class DataConv
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fi"></param>
		/// <param name="str">文字列表現</param>
		/// <returns>FieldInfo.SetValue()用オブジェクト</returns>
		public static object GetObject(FieldInfo fi, string str)
		{
			if (ReflecTools.IsSame(fi, typeof(string)))
			{
				return str;
			}
			if (ReflecTools.IsSame(fi, typeof(int)))
			{
				return int.Parse(str);
			}
			if (ReflecTools.IsSame(fi, typeof(bool)))
			{
				return str == StringTools.S_TRUE || str == "" + 1;
			}
			if (ReflecTools.IsSame(fi, typeof(Color)))
			{
				return DataConv.GetColor(str);
			}
			if (ReflecTools.IsSame(fi, typeof(FontMan)))
			{
				return new FontMan(str);
			}
			throw new Exception("そんなタイプ知りません：" + fi);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fi"></param>
		/// <param name="obj">FieldInfo.GetValue()から得たオブジェクト</param>
		/// <returns>文字列表現</returns>
		public static string GetString(FieldInfo fi, object obj)
		{
			if (ReflecTools.IsSame(fi, typeof(string)))
			{
				return (string)obj;
			}
			if (ReflecTools.IsSame(fi, typeof(int)))
			{
				return "" + obj;
			}
			if (ReflecTools.IsSame(fi, typeof(bool)))
			{
				return (bool)obj ? StringTools.S_TRUE : StringTools.S_FALSE;
			}
			if (ReflecTools.IsSame(fi, typeof(Color)))
			{
				return DataConv.GetString((Color)obj);
			}
			if (ReflecTools.IsSame(fi, typeof(FontMan)))
			{
				return "" + obj;
			}
			throw new Exception("そんなタイプ知りません：" + fi);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ctrl"></param>
		/// <returns>コントロールの値の文字列表現</returns>
		public static string GetString(Control ctrl)
		{
			if (ReflecTools.IsSame(ctrl.GetType(), typeof(TextBox)))
			{
				return ((TextBox)ctrl).Text;
			}
			if (ReflecTools.IsSame(ctrl.GetType(), typeof(CheckBox)))
			{
				return ((CheckBox)ctrl).Checked ? StringTools.S_TRUE : StringTools.S_FALSE;
			}
			if (ReflecTools.IsSame(ctrl.GetType(), typeof(ComboBox)))
			{
				return "" + ((ComboBox)ctrl).SelectedIndex;
			}
			throw new Exception("そんなコントロール知りません：" + ctrl);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ctrl"></param>
		/// <param name="str">コントロールにセットする値の文字列表現</param>
		public static void SetString(Control ctrl, string str)
		{
			if (ReflecTools.IsSame(ctrl.GetType(), typeof(TextBox)))
			{
				((TextBox)ctrl).Text = str;
				return;
			}
			if (ReflecTools.IsSame(ctrl.GetType(), typeof(CheckBox)))
			{
				((CheckBox)ctrl).Checked = str == StringTools.S_TRUE;
				return;
			}
			if (ReflecTools.IsSame(ctrl.GetType(), typeof(ComboBox)))
			{
				int index;

				if (str == StringTools.S_TRUE || str == StringTools.S_FALSE)
					index = str == StringTools.S_TRUE ? 1 : 0;
				else
					index = int.Parse(str);

				((ComboBox)ctrl).SelectedIndex = index;
				return;
			}
			throw new Exception("そんなコントロール知りません：" + ctrl);
		}

		private const string S_BOLD = "BOLD";
		private const string S_ITALIC = "ITALIC";
		private const string S_STRIKEOUT = "STRIKE";
		private const string S_UNDERLINE = "UNDER";
		private const string S_REGULAR = "REGULAR";

		public static string GetString(FontStyle fs)
		{
			List<string> tokens = new List<string>();

			if ((fs & FontStyle.Bold) != 0)
				tokens.Add(S_BOLD);

			if ((fs & FontStyle.Italic) != 0)
				tokens.Add(S_ITALIC);

			if ((fs & FontStyle.Strikeout) != 0)
				tokens.Add(S_STRIKEOUT);

			if ((fs & FontStyle.Underline) != 0)
				tokens.Add(S_UNDERLINE);

			if (tokens.Count == 0)
				tokens.Add(S_REGULAR);

			return string.Join(" ", tokens);
		}

		public static FontStyle GetFontStyle(string str)
		{
			FontStyle fs = 0;

			foreach (string token in str.Split(' '))
			{
				switch (token)
				{
					case S_BOLD: fs |= FontStyle.Bold; break;
					case S_ITALIC: fs |= FontStyle.Italic; break;
					case S_STRIKEOUT: fs |= FontStyle.Strikeout; break;
					case S_UNDERLINE: fs |= FontStyle.Underline; break;
				}
			}
			return fs;
		}

		public static string GetString(Color color)
		{
			return StringTools.ToHex(new byte[] { color.R, color.G, color.B });
		}

		public static Color GetColor(string str)
		{
			byte[] rgb = StringTools.Hex(str);
			return Color.FromArgb(rgb[0], rgb[1], rgb[2]);
		}
	}
}
