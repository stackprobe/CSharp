using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Test01.DateSpans.Tests;
using Test01.Modules.Tests;

namespace Test01
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				//Test01();
				//Test02();
				//Test03();
				//Test04();
				//new DateSpanListTest().Test01();
				//new NamesToGroupDateSpansTest().Test01();
				//new NamesToGroupDateSpansTest().Test02();
				new TimeLimitedTempDirTest().Test01();
				//new TimeLimitedTempDirTest().Test02();
				//new TimeLimitedTempDirTest().Test03();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			Console.WriteLine("Press ENTER");
			Console.ReadLine();
		}

		private static void Test01()
		{
			var a = new
			{
				A = 1,
				B = 2,
				C = 3,
			};

			Console.WriteLine("---- a");
			Console.WriteLine(a.GetType());
			Console.WriteLine(a);

			var b = new[]
			{
				new {X = 1},
				new {X = 2},
				new {X = 3},
			};

			Console.WriteLine("---- b");
			Console.WriteLine(b.GetType());
			Console.WriteLine(b);
			Console.WriteLine(b[0].GetType());
			Console.WriteLine(b[0]);

			var c = new[,]
			{
				{ new {X = 1}},
				{ new {X = 2}},
				{ new {X = 3}},
			};

			Console.WriteLine("---- c");
			Console.WriteLine(c.GetType());
			Console.WriteLine(c);
			Console.WriteLine(c[0, 0].GetType());
			Console.WriteLine(c[0, 0]);
			Console.WriteLine(c[1, 0]);
			Console.WriteLine(c[2, 0]);
			//Console.WriteLine(c[0, 1]); // 無いよ。
			//Console.WriteLine(c[3, 0]); // 無いよ。
			Console.WriteLine(c.Length);

			var d = new[,]
			{
				{
					new { X = 1, Z = 2 },
					new { X = 3, Z = 4 },
					new { X = 5, Z = 6 },
				},
				{
					new { X = 7, Z = 8 },
					new { X = 9, Z = 10 },
					new { X = 11, Z = 12 },
				},
			};

			Console.WriteLine("---- d");
			Console.WriteLine(d.Length);
			Console.WriteLine(d.GetLength(0));
			Console.WriteLine(d.GetLength(1));
			Console.WriteLine(d.GetUpperBound(0));
			Console.WriteLine(d.GetUpperBound(1));
			Console.WriteLine(d[1, 2]);

			var e = new[, ,]
			{
				{
					{ new { X = 1 }, new { X = 2 }, new { X = 3 }, new { X = 4 } },
					{ new { X = 1 }, new { X = 2 }, new { X = 3 }, new { X = 4 } },
					{ new { X = 1 }, new { X = 2 }, new { X = 3 }, new { X = 4 } },
				},
				{
					{ new { X = 1 }, new { X = 2 }, new { X = 3 }, new { X = 4 } },
					{ new { X = 1 }, new { X = 2 }, new { X = 3 }, new { X = 4 } },
					{ new { X = 1 }, new { X = 2 }, new { X = 3 }, new { X = 999 } },
				},
			};

			Console.WriteLine("---- e");
			Console.WriteLine(e.Length);
			Console.WriteLine(e[1, 2, 3]);

			Console.WriteLine("----");
		}

		private static void Test02()
		{
			{
				var aa = new[]
				{
					new { X = 1, Y = 1, Z = 1 }
				};

				var rr = from a in aa where a.X == 1 && a.Y == 1 && a.Z == 1 select a;

				foreach (var r in rr)
					Console.WriteLine(r);
			}

			{
				var aa = new[]
				{
					new { X = 1 },
					new { X = 2 },
					new { X = 3 },
				};

				var rr = from a in aa where Test02_a(a) select a;
				//var rr = aa.Where(a => Test02_a(a)).Select(a => a);

				Console.WriteLine(rr.ToArray<object>().Length);

				foreach (var r in rr)
					Console.WriteLine(r);

				// cout
				/*
Test02_a_a: { X = 1 }
Test02_a_a: { X = 2 }
Test02_a_a: { X = 3 }
2
Test02_a_a: { X = 1 }
{ X = 1 }
Test02_a_a: { X = 2 }
Test02_a_a: { X = 3 }
{ X = 3 }
				 */
			}
		}

		private static bool Test02_a(object a)
		{
			Console.WriteLine("Test02_a_a: " + a);
			return ("" + a) != "{ X = 2 }";
		}

		private static void Test03()
		{
			//Console.WriteLine(Path.GetDirectoryName("")); // 例外
			Console.WriteLine(Path.GetDirectoryName("aaa")); // -> ""
			Console.WriteLine(Path.GetDirectoryName("aaa\\bbb"));
			Console.WriteLine(Path.GetDirectoryName("aaa\\bbb\\ccc"));

			Console.WriteLine(Path.GetDirectoryName("\\aaa")); // -> "\\"
			Console.WriteLine(Path.GetDirectoryName("\\aaa\\bbb"));
			Console.WriteLine(Path.GetDirectoryName("\\aaa\\bbb\\ccc"));

			Console.WriteLine(Path.GetDirectoryName("\\\\aaa")); // -> ""
			Console.WriteLine(Path.GetDirectoryName("\\\\aaa\\bbb")); // -> ""
			Console.WriteLine(Path.GetDirectoryName("\\\\aaa\\bbb\\ccc")); // -> "\\\\aaa\\bbb"

			Console.WriteLine(Directory.Exists("")); // -> false
			Console.WriteLine(File.Exists("")); // -> false
		}

		private static void Test04()
		{
			Console.WriteLine("Test04_a");

			FileStream fs = null;

			try
			{
				if (File.Exists(@"C:\temp\1.txt"))
					throw new Exception("aaa");

				fs = new FileStream(@"C:\temp\1.txt", FileMode.Create, FileAccess.Write);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			finally
			{
				fs.Close();
				fs = null;
			}

			Console.WriteLine("Test04_z");
		}
	}
}
