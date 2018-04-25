using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Charlotte.Tools
{
	public class RTBManager
	{
		public RichTextBox RTB;
		private Control OtherCtrl;

		public RTBManager(RichTextBox rtb, Control otherCtrl)
		{
			this.RTB = rtb;
			this.OtherCtrl = otherCtrl;
		}

		public void Clear()
		{
			try
			{
				const string DUMMY_TEXT = "\n"; // これで "" になる。

				RichTextBox rtb = new RichTextBox();

				rtb.Text = DUMMY_TEXT;
				rtb.SelectionStart = 0;
				rtb.SelectionLength = DUMMY_TEXT.Length;
				rtb.SelectionProtected = true;

				this.RTB.Rtf = rtb.SelectedRtf;
			}
			catch (Exception e)
			{
				MessageBox.Show("" + e);
			}
		}

		public class Token
		{
			public Font Font;
			public Color Color;
			public string Text;

			public Token(Font font, Color color, string text)
			{
				this.Font = font;
				this.Color = color;
				this.Text = text;
			}

			public Token GetClone()
			{
				return new Token(this.Font, this.Color, this.Text);
			}
		}

		public void Add(List<Token> tokens)
		{
			this.Join(this.RTB.Rtf, tokens);
		}

		private const int JOIN_NUM_MAX = 100;

		/// <summary>
		/// rtf に tokens を連結して this.RTF にセットする。
		/// tokens の最後のトークンの token.Text の最後の文字は改行であってはならない！！！
		/// </summary>
		/// <param name="rtf">rtf</param>
		/// <param name="tokens">tokens</param>
		public void Join(string rtf, List<Token> tokens)
		{
			if (JOIN_NUM_MAX < tokens.Count)
			{
				for (int index = 0; index < tokens.Count; index += JOIN_NUM_MAX)
				{
					int count = Math.Min(JOIN_NUM_MAX, tokens.Count - index);
					List<Token> range = tokens.GetRange(index, count);
					this.Join_Main(index == 0 ? rtf : this.RTB.Rtf, range);
				}
			}
			else
			{
				this.Join_Main(rtf, tokens);
			}
		}

		public void Join_Main(string rtf, List<Token> tokens)
		{
			try
			{
				this.OtherCtrl.Focus();

				RichTextBox rtb = new RichTextBox();

				rtb.Rtf = rtf;

				int startPos = rtb.Text.Length;

				rtb.SelectionStart = startPos;
				rtb.SelectionLength = 0;
				rtb.SelectionProtected = false;

				StringBuilder buff = new StringBuilder();

				foreach (Token token in tokens)
					buff.Append(token.Text);

				rtb.SelectedText = buff.ToString();

				foreach (Token token in tokens)
				{
					SetStyle(
						rtb,
						startPos,
						token.Text.Length,
						token.Font,
						token.Color
						);

					startPos += token.Text.Length;
				}
				rtb.SelectionStart = 0;
				rtb.SelectionLength = rtb.Text.Length;
				rtb.SelectionProtected = true;

				this.RTB.Rtf = rtb.SelectedRtf;

				this.OtherCtrl.Focus();
			}
			catch (Exception e)
			{
				Gnd.Logger.writeLine(e);
			}
		}

		private static void SetStyle(RichTextBox rtb, int startPos, int length, Font font, Color color)
		{
			rtb.SelectionStart = startPos;
			rtb.SelectionLength = length;
			rtb.SelectionFont = font;
			rtb.SelectionColor = color;
		}

		public void ScrollToTop()
		{
			try
			{
				this.RTB.SelectionStart = 0;
				this.RTB.SelectionLength = 0;
				this.RTB.ScrollToCaret();
			}
			catch (Exception e)
			{
				Gnd.Logger.writeLine(e);
			}
		}

		/// <summary>
		/// 既に下までスクロールしているか、それ以上下にスクロールしている場合、うまくスクロールしないことがある。
		/// その場合 ScrollToTop() してから ScrollToBottom() すると画面がチラつくけどうまくいく。
		/// </summary>
		public void ScrollToBottom()
		{
			try
			{
				this.RTB.SelectionStart = this.RTB.Text.Length;
				this.RTB.SelectionLength = 0;
				this.RTB.ScrollToCaret();
			}
			catch (Exception e)
			{
				Gnd.Logger.writeLine(e);
			}
		}

		public void CutTop(int cutLen)
		{
			RichTextBox rtb = this.RTB;

			rtb.SelectionStart = 0;
			rtb.SelectionLength = cutLen;
			rtb.SelectionProtected = false;
			rtb.SelectedText = "";
		}

		public void Set行間を詰める(bool flag)
		{
			if (flag)
				this.RTB.LanguageOption = RichTextBoxLanguageOptions.UIFonts; // 行間を詰める。
			else
				this.RTB.LanguageOption = RichTextBoxLanguageOptions.AutoFont | RichTextBoxLanguageOptions.DualFont; // デフォルト
		}

		public void SetBorderStyle(bool flag)
		{
			this.RTB.BorderStyle = flag ? BorderStyle.None : BorderStyle.Fixed3D;
		}
	}
}
