using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Charlotte.Tools;

namespace Charlotte
{
	public class FontInfo
	{
		public string Family = "メイリオ";
		public int Size = 8;
		public FontStyle Style = FontStyle.Regular;
		public Color Color = Color.Black;

		public string GetString()
		{
			return AttSt.untokenize(new string[]
			{
				this.Family,
				"" + this.Size,
				"" + (int)this.Style,
				Common.ToHexString(this.Color),
			});
		}

		public void SetString(string str)
		{
			try
			{
				string[] tokens = AttSt.tokenize(str);
				int c = 0;

				this.Family = tokens[c++];
				this.Size = int.Parse(tokens[c++]);
				this.Style = (FontStyle)int.Parse(tokens[c++]);
				this.Color = Common.ToColorHex(tokens[c++]);
			}
			catch (Exception e)
			{
				Gnd.Logger.writeLine(e);
			}
		}

		private static AttachString AttSt = new AttachString(':', '$', 'C');
	}
}
