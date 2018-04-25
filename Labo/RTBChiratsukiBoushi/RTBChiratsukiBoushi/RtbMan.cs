using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace RTBChiratsukiBoushi
{
	public class RtbMan
	{
		public RichTextBox I;
		private Control OtherCtrl;

		public RtbMan(RichTextBox rtb, Control otherCtrl)
		{
			this.I = rtb;
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

				this.I.Rtf = rtb.SelectedRtf;
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
			this.Join(this.I.Rtf, tokens);
		}

		private const int JOIN_NUM_MAX = 100;

		public void Join(string rtf, List<Token> tokens)
		{
			if (JOIN_NUM_MAX < tokens.Count)
			{
				for (int index = 0; index < tokens.Count; index += JOIN_NUM_MAX)
				{
					int count = Math.Min(JOIN_NUM_MAX, tokens.Count - index);
					List<Token> range = tokens.GetRange(index, count);
					this.Join_Main(index == 0 ? rtf : this.I.Rtf, range);
				}
			}
			else
				this.Join_Main(rtf, tokens);
		}

		public void Join_Main(string rtf, List<Token> tokens)
		{
			try
			{
				this.OtherCtrl.Focus();

				RichTextBox rtb = new RichTextBox();

				rtb.Rtf = rtf;
				//rtb.Rtf = this.I.Rtf; // old

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

				this.I.Rtf = rtb.SelectedRtf;

				this.OtherCtrl.Focus();
			}
			catch (Exception e)
			{
				MessageBox.Show("" + e);
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
				this.I.SelectionStart = 0;
				this.I.SelectionLength = 0;
				this.I.ScrollToCaret();
			}
			catch (Exception e)
			{
				MessageBox.Show("" + e);
			}
		}

		/// <summary>
		/// うまく下まで行かないことがある。
		/// </summary>
		public void ScrollToBottom()
		{
			try
			{
				this.I.SelectionStart = this.I.Text.Length;
				this.I.SelectionLength = 0;
				this.I.ScrollToCaret();
			}
			catch (Exception e)
			{
				MessageBox.Show("" + e);
			}
		}

		public void Set行間を詰める(bool flag)
		{
			if (flag)
				this.I.LanguageOption = RichTextBoxLanguageOptions.UIFonts; // 行間を詰める。
			else
				this.I.LanguageOption = RichTextBoxLanguageOptions.AutoFont | RichTextBoxLanguageOptions.DualFont; // デフォルト
		}

		public void Cut(int cutLen)
		{
			RichTextBox rtb = this.I;

			rtb.SelectionStart = 0;
			rtb.SelectionLength = cutLen;
			rtb.SelectionProtected = false;
			rtb.SelectedText = "";
		}
	}
}
