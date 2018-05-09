using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class JsonTools
	{
		public static string Encode(object src, bool noBlank = false)
		{
			Encoder e = new Encoder(
				noBlank ? "" : " ",
				noBlank ? "" : "\t",
				noBlank ? "" : "\r\n"
				);
			e.Add(src, 0);
			return e.GetString();
		}

		private class Encoder
		{
			private string Blank;
			private string Indent;
			private string NewLine;

			public Encoder(string blank, string indent, string newLine)
			{
				this.Blank = blank;
				this.Indent = indent;
				this.NewLine = newLine;
			}

			private StringBuilder Buff = new StringBuilder();

			public void Add(object src, int indent)
			{
				if (src is ObjectMap)
				{
					ObjectMap om = (ObjectMap)src;
					bool secondOrLater = false;

					this.Buff.Append("{");
					this.Buff.Append(this.NewLine);

					List<string> keys = ArrayTools.ToList(om.GetKeys());
					keys.Sort(StringTools.Comp);
					foreach (string key in keys)
					{
						object value = om[key];

						if (secondOrLater)
						{
							this.Buff.Append(",");
							this.Buff.Append(this.NewLine);
						}
						this.AddIndent(indent + 1);
						this.Buff.Append("\"");
						this.Buff.Append(key);
						this.Buff.Append("\"");
						this.Buff.Append(this.Blank);
						this.Buff.Append(":");
						this.Buff.Append(this.Blank);
						this.Add(value, indent + 1);

						secondOrLater = true;
					}
					this.Buff.Append(this.NewLine);
					this.AddIndent(indent);
					this.Buff.Append("}");
				}
				else if (src is ObjectList)
				{
					ObjectList ol = (ObjectList)src;
					bool secondOrLater = false;

					this.Buff.Append("[");
					this.Buff.Append(this.NewLine);

					foreach (object value in ol.Direct())
					{
						if (secondOrLater)
						{
							this.Buff.Append(",");
							this.Buff.Append(this.NewLine);
						}
						this.AddIndent(indent + 1);
						this.Add(value, indent + 1);

						secondOrLater = true;
					}
					this.Buff.Append(this.NewLine);
					this.AddIndent(indent);
					this.Buff.Append("]");
				}
				else if (src is Word)
				{
					this.Buff.Append(src);
				}
				// 想定外の型 >
				else if (src == null)
				{
					this.Buff.Append("null");
				}
				else if (src is bool)
				{
					this.Buff.Append((bool)src ? "true" : "false");
				}
				else if (
					src is Int16 ||
					src is Int32 ||
					src is Int64 ||
					src is UInt16 ||
					src is UInt32 ||
					src is UInt64 ||
					src is float ||
					src is double
					)
				{
					this.Buff.Append("" + src);
				}
				// < 想定外の型
				else // src is string
				{
					string str = "" + src;
					//string str = (string)src;

					this.Buff.Append("\"");

					foreach (char chr in str)
					{
						if (chr == '"')
						{
							this.Buff.Append("\\\"");
						}
						else if (chr == '\\')
						{
							this.Buff.Append("\\\\");
						}
						else if (chr == '\b')
						{
							this.Buff.Append("\\b");
						}
						else if (chr == '\f')
						{
							this.Buff.Append("\\f");
						}
						else if (chr == '\n')
						{
							this.Buff.Append("\\n");
						}
						else if (chr == '\r')
						{
							this.Buff.Append("\\r");
						}
						else if (chr == '\t')
						{
							this.Buff.Append("\\t");
						}
						else
						{
							this.Buff.Append(chr);
						}
					}
					this.Buff.Append("\"");
				}
			}

			public void AddIndent(int count)
			{
				while (0 < count)
				{
					this.Buff.Append(this.Indent);
					count--;
				}
			}

			public string GetString()
			{
				return this.Buff.ToString();
			}
		}

		public static object Decode(byte[] src)
		{
			return Decode(GetEncoding(src).GetString(src));
		}

		private static Encoding GetEncoding(byte[] src)
		{
			if (4 <= src.Length)
			{
				string x4 = BinTools.Hex.ToString(BinTools.GetSubBytes(src, 0, 4));

				if ("0000feff" == x4 || "fffe0000" == x4)
				{
					return Encoding.UTF32;
				}
			}
			if (2 <= src.Length)
			{
				string x2 = BinTools.Hex.ToString(BinTools.GetSubBytes(src, 0, 2));

				if ("feff" == x2 || "fffe" == x2)
				{
					return Encoding.Unicode;
				}
			}
			return Encoding.UTF8;
		}

		public static object Decode(string src)
		{
			return new Decoder(src).GetObject();
		}

		private class Decoder
		{
			private string Src;
			private int RPos;

			public Decoder(string src)
			{
				this.Src = src;
			}

			private char Next()
			{
				return this.Src[this.RPos++];
			}

			private char NextNS()
			{
				char chr;

				do
				{
					chr = this.Next();
				}
				while (chr <= ' ');

				return chr;
			}

			public object GetObject()
			{
				char chr = this.NextNS();

				if (chr == '{')
				{
					ObjectMap om = ObjectMap.CreateIgnoreCase();

					if (this.NextNS() != '}')
					{
						this.RPos--;

						do
						{
							object key = this.GetObject();
							this.NextNS(); // ':'
							object value = this.GetObject();
							om.Add(key, value);
						}
						while (this.NextNS() != '}');
					}
					return om;
				}
				if (chr == '[')
				{
					ObjectList ol = new ObjectList();

					if (this.NextNS() != ']')
					{
						this.RPos--;

						do
						{
							ol.Add(this.GetObject());
						}
						while (this.NextNS() != ']');
					}
					return ol;
				}
				if (chr == '"')
				{
					StringBuilder buff = new StringBuilder();

					for (; ; )
					{
						chr = this.Next();

						if (chr == '"')
						{
							break;
						}
						if (chr == '\\')
						{
							chr = this.Next();

							if (chr == 'b')
							{
								chr = '\b';
							}
							else if (chr == 'f')
							{
								chr = '\f';
							}
							else if (chr == 'n')
							{
								chr = '\n';
							}
							else if (chr == 'r')
							{
								chr = '\r';
							}
							else if (chr == 't')
							{
								chr = '\t';
							}
							else if (chr == 'u')
							{
								char c1 = this.Next();
								char c2 = this.Next();
								char c3 = this.Next();
								char c4 = this.Next();

								chr = (char)Convert.ToInt32(new string(new char[] { c1, c2, c3, c4 }), 16);
							}
						}
						buff.Append(chr);
					}
					return buff.ToString();
				}

				{
					StringBuilder buff = new StringBuilder();

					buff.Append(chr);

					while (this.RPos < this.Src.Length)
					{
						chr = this.Next();

						if (
							chr == '}' ||
							chr == ']' ||
							chr == ',' ||
							chr == ':'
							)
						{
							this.RPos--;
							break;
						}
						buff.Append(chr);
					}
					string str = buff.ToString();
					str = str.Trim();
					return new Word(str);
				}
			}
		}

		public class Word
		{
			public string Value;

			public Word(string value)
			{
				this.Value = value;
			}
		}
	}
}
