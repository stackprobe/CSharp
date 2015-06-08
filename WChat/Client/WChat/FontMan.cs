using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte
{
	public class FontMan
	{
		public string Name;
		public int Size;
		public FontStyle Style;
		public Color Color;

		public FontMan(Color color)
		{
			this.Name = Consts.DEFAULT_FONT_NAME;
			this.Size = 10;
			this.Style = FontStyle.Regular;
			this.Color = color;
		}

		public FontMan(int size, Color color)
		{
			this.Name = Consts.DEFAULT_FONT_NAME;
			this.Size = size;
			this.Style = FontStyle.Regular;
			this.Color = color;
		}

		public FontMan(string name, int size, FontStyle style, Color color)
		{
			this.Name = name;
			this.Size = size;
			this.Style = style;
			this.Color = color;
		}

		public FontMan(string str)
		{
			this.SetString(str);
		}

		public void SetString(string str)
		{
			string[] tokens = str.Split(':');
			int c = 0;

			this.Name = tokens[c++];
			this.Size = int.Parse(tokens[c++]);
			this.Style = DataConv.GetFontStyle(tokens[c++]);
			this.Color = DataConv.GetColor(tokens[c++]);
		}

		public override string ToString()
		{
			return
				this.Name + ":" +
				this.Size + ":" +
				DataConv.GetString(this.Style) + ":" +
				DataConv.GetString(this.Color);
		}

		public Font GetFont()
		{
			return new Font(this.Name, this.Size, this.Style);
		}

		public Color GetColor()
		{
			return this.Color;
		}

		public FontMan GetClone()
		{
			return new FontMan(this.ToString());
		}
	}
}
