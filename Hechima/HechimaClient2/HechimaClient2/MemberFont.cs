using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class MemberFont
	{
		public string IdentMidPtn = "名無しさん"; // この文字列を含む Ident のメンバーの発言に、このインスタンスの設定を反映する。
		public FontInfo Stamp = new FontInfo()
		{
			Size = 8,
		};
		public FontInfo Ident = new FontInfo();
		public FontInfo Message = new FontInfo();

		public string GetString()
		{
			return AttSt.untokenize(new string[]
			{
				this.IdentMidPtn,
				this.Stamp.GetString(),
				this.Ident.GetString(),
				this.Message.GetString(),
			});
		}

		public void SetString(string str)
		{
			try
			{
				string[] tokens = AttSt.tokenize(str);
				int c = 0;

				this.IdentMidPtn = tokens[c++];
				this.Stamp.SetString(tokens[c++]);
				this.Ident.SetString(tokens[c++]);
				this.Message.SetString(tokens[c++]);
			}
			catch (Exception e)
			{
				Gnd.Logger.writeLine(e);
			}
		}

		private static AttachString AttSt = new AttachString(';', '$', 'S');
	}
}
