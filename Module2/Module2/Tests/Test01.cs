using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.Tests
{
	public class Test01
	{
		public void Main01()
		{
			foreach (char chr in StringTools.HALF)
			{
				Console.Write("[" + ((int)chr).ToString("x2") + "]");
			}
			Console.WriteLine("");

			foreach (char chr in "\t\r\n ")
			{
				Console.Write("[" + ((int)chr).ToString("x2") + "]");
			}
			Console.WriteLine("");

			{
				string CONTROL = StringTools.GetString_SJISHalfCodeRange(0x00, 0x1f);

				foreach (char chr in CONTROL)
				{
					Console.Write("[" + ((int)chr).ToString("x2") + "]");
				}
				Console.WriteLine("");
			}

			{
				char c = (char)0;

				Console.WriteLine("" + (int)c);
				c--;
				Console.WriteLine("" + (int)c);
			}
		}

		public void Main02()
		{
			Console.WriteLine(Path.Combine("abc", "def"));
			Console.WriteLine(Path.Combine("abc", ""));
			Console.WriteLine(Path.Combine("", "def"));
			Console.WriteLine(Path.Combine("", ""));

			Console.WriteLine("---- 01");

			Console.WriteLine(Path.Combine("abc", "\\"));
			Console.WriteLine(Path.Combine("\\", "def"));
			Console.WriteLine(Path.Combine("abc", "\\\\"));
			Console.WriteLine(Path.Combine("\\\\", "def"));

			Console.WriteLine("---- 02");

			Console.WriteLine(Path.Combine("", "\\"));
			Console.WriteLine(Path.Combine("\\", ""));
			Console.WriteLine(Path.Combine("\\", "\\"));

			Console.WriteLine("---- 03");

			Console.WriteLine(Path.Combine("", "\\\\"));
			Console.WriteLine(Path.Combine("\\\\", ""));
			Console.WriteLine(Path.Combine("\\", "\\\\"));
			Console.WriteLine(Path.Combine("\\\\", "\\"));
			Console.WriteLine(Path.Combine("\\\\", "\\\\"));

			Console.WriteLine("---- 04");
		}

		public void Main03()
		{
			{
				int a = 1;

				if (
					int.TryParse("2", out a) &&
					a == 2
					)
					Console.WriteLine("OK");
			}

			// ----

			try
			{
				throw new Exception("aaa");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			try
			{
				throw new Exception("aaa", null);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			try
			{
				throw new Exception("aaa", new Exception("bbb"));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			try
			{
				throw new Exception("aaa", new Exception("bbb", null));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			// ----

			Console.WriteLine("----");

			using (null)
			{
				Console.WriteLine("a");
			}

			Console.WriteLine("----");

			using (GetNullOfIDisposable())
			{
				Console.WriteLine("a");
			}

			Console.WriteLine("----");

			try
			{
				Console.WriteLine("IDisposableCtorThrowException before using");
				using (new IDisposableCtorThrowException())
				{
					Console.WriteLine("IDisposableCtorThrowException inside using");
				}
				Console.WriteLine("IDisposableCtorThrowException after using");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			Console.WriteLine("----");

			try
			{
				Console.WriteLine("IDisposableDisposeThrowException before using");
				using (new IDisposableDisposeThrowException())
				{
					Console.WriteLine("IDisposableDisposeThrowException inside using");
				}
				Console.WriteLine("IDisposableDisposeThrowException after using");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			Console.WriteLine("----");

			// ----

			Console.WriteLine("dw.1");
			do
			{
				continue;
			}
			while (1 == 2);
			Console.WriteLine("dw.2");
		}

		private IDisposable GetNullOfIDisposable()
		{
			return null;
		}

		private class IDisposableCtorThrowException : IDisposable
		{
			public IDisposableCtorThrowException()
			{
				Console.WriteLine("IDisposableCtorThrowException.Ctor");
				throw new Exception("IDisposableCtorThrowException.EXCEPTION");
			}

			public void Dispose()
			{
				Console.WriteLine("IDisposableCtorThrowException.Dispose");
			}
		}

		private class IDisposableDisposeThrowException : IDisposable
		{
			public IDisposableDisposeThrowException()
			{
				Console.WriteLine("IDisposableDisposeThrowException.Ctor");
			}

			public void Dispose()
			{
				Console.WriteLine("IDisposableDisposeThrowException.Dispose");
				throw new Exception("IDisposableDisposeThrowException.EXCEPTION");
			}
		}
	}
}
