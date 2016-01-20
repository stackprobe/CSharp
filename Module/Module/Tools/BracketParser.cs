using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class BracketParser
	{
		public static string Perform(string text)
		{
			Stack<BracketInfo> bis = new Stack<BracketInfo>();

			for (int index = 0; index < text.Length; )
			{
				if (1 <= bis.Count && bis.Peek().R == text[index])
				{
					BracketInfo bi = bis.Pop();
					int iti = bi.L + 1;
					string innerText = text.Substring(iti, index - iti);

					innerText = Filter(innerText, text[bi.L]);

					string t1 = text.Substring(0, bi.L) + innerText;
					string t2 = text.Substring(index + 1);

					text = t1 + t2;
					index = t1.Length;
				}
				else
				{
					if (text[index] == '$')
					{
						text = StringTools.Remove(text, index, 1);

						if (text[index] != '$')
						{
							bis.Push(GetBI(text, index));
						}
					}
					index++;
				}
			}
			return text;
		}

		private class BracketInfo
		{
			public int L;
			public char R;
		}

		private static BracketInfo GetBI(string text, int index)
		{
			BracketInfo bi = new BracketInfo();

			bi.L = index;

			switch (text[index])
			{
				case '{': bi.R = '}'; break;
				case '(': bi.R = ')'; break;
				case '[': bi.R = ']'; break;

				default:
					throw new Exception("不明な左括弧 [" + text[index] + "]");
			}
			return bi;
		}

		private static string Filter(string text, char lb)
		{
			switch (lb)
			{
				case '{':
					return text;

				case '(':
					return text;

				case '[':
					return text;
			}
			throw new Exception("不明な左括弧 [" + lb + "]");
		}
	}
}
