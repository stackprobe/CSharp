using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class JsonTools
	{
		public static string encode(object src, bool noBlank = false)
		{
			Encoder e = new Encoder(
					noBlank ? "" : " ",
					noBlank ? "" : "\t",
					noBlank ? "" : "\r\n"
					);
			e.add(src, 0);
			return e.get();
		}

		private class Encoder
		{
			private string _blank;
			private string _indent;
			private string _newLine;

			public Encoder(string blank, string indent, string newLine)
			{
				_blank = blank;
				_indent = indent;
				_newLine = newLine;
			}

			private StringBuilder _buff = new StringBuilder();

			public void add(object src, int indent)
			{
				if (src is ObjectMap)
				{
					ObjectMap om = (ObjectMap)src;
					bool secondOrLater = false;

					_buff.Append("{");
					_buff.Append(_newLine);

					foreach (string key in om.getKeys()) // XXX keyOrder
					{
						object value = om[key];

						if (secondOrLater)
						{
							_buff.Append(",");
							_buff.Append(_newLine);
						}
						addIndent(indent + 1);
						_buff.Append("\"");
						_buff.Append(key);
						_buff.Append("\"");
						_buff.Append(_blank);
						_buff.Append(":");
						_buff.Append(_blank);
						add(value, indent + 1);

						secondOrLater = true;
					}
					_buff.Append(_newLine);
					addIndent(indent);
					_buff.Append("}");
				}
				else if (src is ObjectList)
				{
					ObjectList ol = (ObjectList)src;
					bool secondOrLater = false;

					_buff.Append("[");
					_buff.Append(_newLine);

					foreach (object value in ol.getList())
					{
						if (secondOrLater)
						{
							_buff.Append(",");
							_buff.Append(_newLine);
						}
						addIndent(indent + 1);
						add(value, indent + 1);

						secondOrLater = true;
					}
					_buff.Append(_newLine);
					addIndent(indent);
					_buff.Append("]");
				}
				else if (src is string)
				{
					string str = (string)src;

					_buff.Append("\"");

					foreach (char chr in str)
					{
						if (chr == '"')
						{
							_buff.Append("\\\"");
						}
						else if (chr == '\\')
						{
							_buff.Append("\\\\");
						}
						else if (chr == '\b')
						{
							_buff.Append("\\b");
						}
						else if (chr == '\f')
						{
							_buff.Append("\\f");
						}
						else if (chr == '\n')
						{
							_buff.Append("\\n");
						}
						else if (chr == '\r')
						{
							_buff.Append("\\r");
						}
						else if (chr == '\t')
						{
							_buff.Append("\\t");
						}
						else
						{
							_buff.Append(chr);
						}
					}
					_buff.Append("\"");
				}
				else
				{
					_buff.Append(src);
				}
			}

			public void addIndent(int count)
			{
				while (0 < count)
				{
					_buff.Append(_indent);
					count--;
				}
			}

			public string get()
			{
				return _buff.ToString();
			}
		}

		public static object decode(byte[] src)
		{
			return decode(getEncoding(src).GetString(src));
		}

		private static Encoding getEncoding(byte[] src)
		{
			if (4 <= src.Length)
			{
				string x4 = StringTools.toHex(BinaryTools.getSubBytes(src, 0, 4));

				if ("0000feff" == x4 || "fffe0000" == x4)
				{
					return Encoding.UTF32;
				}
			}
			if (2 <= src.Length)
			{
				string x2 = StringTools.toHex(BinaryTools.getSubBytes(src, 0, 2));

				if ("feff" == x2 || "fffe" == x2)
				{
					return Encoding.Unicode;
				}
			}
			return Encoding.UTF8;
		}

		public static object decode(string src)
		{
			return new Decoder(src).get();
		}

		private class Decoder
		{
			private string _src;
			private int _rPos;

			public Decoder(string src)
			{
				_src = src;
			}

			private char next()
			{
				return _src[_rPos++];
			}

			private char nextNS()
			{
				char chr;

				do
				{
					chr = next();
				}
				while (chr <= ' ');

				return chr;
			}

			public object get()
			{
				char chr = nextNS();

				if (chr == '{')
				{
					ObjectMap om = ObjectMap.createIgnoreCase();

					if (nextNS() != '}')
					{
						_rPos--;

						do
						{
							object key = get();
							nextNS(); // ':'
							object value = get();
							om.add(key, value);
						}
						while (nextNS() != '}');
					}
					return om;
				}
				if (chr == '[')
				{
					ObjectList ol = new ObjectList();

					if (nextNS() != ']')
					{
						_rPos--;

						do
						{
							ol.add(get());
						}
						while (nextNS() != ']');
					}
					return ol;
				}
				if (chr == '"')
				{
					StringBuilder buff = new StringBuilder();

					for (; ; )
					{
						chr = next();

						if (chr == '"')
						{
							break;
						}
						if (chr == '\\')
						{
							chr = next();

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
								char c1 = next();
								char c2 = next();
								char c3 = next();
								char c4 = next();

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

					while (_rPos < _src.Length)
					{
						chr = next();

						if (chr == '}' ||
								chr == ']' ||
								chr == ',' ||
								chr == ':'
								)
						{
							_rPos--;
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
			public string value;

			public Word(string initVal)
			{
				value = initVal;
			}
		}
	}
}
