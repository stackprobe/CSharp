﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Test01.DateSpans.Tests;
using Test01.Modules.Tests;
using Test01.Tests;

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
				//Test05();
				//Test06();
				//Test07();
				//Test07_B();
				//Test08();
				//Test09();
				//Test10();
				//Test11();
				//Test12();
				//Test13();
				//Test13_B();
				//Test14();
				//Test15();
				Test16();
				//new DateSpanListTest().Test01();
				//new NamesToGroupDateSpansTest().Test01();
				//new NamesToGroupDateSpansTest().Test02();
				//new TimeLimitedTempDirTest().Test01();
				//new TimeLimitedTempDirTest().Test02();
				//new TimeLimitedTempDirTest().Test03();
				//new RandDataFileHelperTest().Test01();
				//new MultiThreadTaskPoolTest().Test01();
				//new MultiThreadTaskPoolTest().Test02();
				//new AsyncFileWriterTest().Test01();
				//new BlockSectionTest().Test01();
				//new BlockSectionTest().Test02();
				//new Test0001().Test01();
				//new Test0002().Test01();
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

		private static void Test05()
		{
			string s = "勤怠";
			byte[] b = Encoding.GetEncoding(932).GetBytes(s);
			string s2 = Encoding.GetEncoding(932).GetString(b);

			Console.WriteLine(s2);

			// ----

			Console.WriteLine(string.Format("{0:X}", 0xffff)); // FFFF
			Console.WriteLine(string.Format("{0:x}", 0xffff)); // ffff
			Console.WriteLine(string.Format("{0:X8}", 0xffff)); // 0000FFFF
			Console.WriteLine(string.Format("{0:x8}", 0xffff)); // 0000ffff

			Console.WriteLine(string.Format("{0:x}", -0xffff)); // ffff0001
		}

		private static void Test06()
		{
			// '/' -> '\\' してくれる。
			Console.WriteLine(Path.GetFullPath("C:/AAA/BBB/CCC")); // -> @"C:\AAA\BBB\CCC"

			// '/' -> '\\' してくれる。
			Console.WriteLine(Path.GetFullPath("//xxx/aaa/bbb/ccc")); // -> @"\\xxx\aaa\bbb\ccc"
		}

		private static void Test07()
		{
			int[] array = Test07_NewArray<int>(1500000000 / 4); // ok
			//int[] array = Test07_NewArray<int>(1600000000 / 4); // ng
			//int[] array = Test07_NewArray<int>(1700000000 / 4); // ng
			//int[] array = Test07_NewArray<int>(2000000000 / 4); // ng
		}

		private static T[] Test07_NewArray<T>(int length)
		{
			return new T[length];
		}

		private static void Test07_B()
		{
			int[] array = new int[1500000000 / 4]; // ok
			//int[] array = new int[1600000000 / 4]; // ng
		}

		private static void Test08()
		{
			Queue<int> q = new Queue<int>();

			q.Enqueue(1);
			q.Enqueue(2);
			q.Enqueue(3);

			foreach (var i in q.ToArray())
			{
				Console.WriteLine(i);
			}
			while (1 <= q.Count)
			{
				Console.WriteLine(q.Dequeue());
			}
		}

		private static void Test09()
		{
			byte[] bytes = new byte[256];

			for (int index = 0; index < 256; index++)
				bytes[index] = (byte)index;

			string str = Encoding.ASCII.GetString(bytes);

			Console.WriteLine("[" + str + "]");
		}

		private static void Test10()
		{
			Console.WriteLine(DateTime.Now.Ticks / 10000000.0);
		}

		private static void Test11()
		{
			try
			{
				throw new AggregateException();
			}
			catch (AggregateException e)
			{
				Console.WriteLine(e.InnerException != null);
				Console.WriteLine(e.InnerExceptions.Count());
			}

			try
			{
				throw new AggregateException("aaa", new Exception());
			}
			catch (AggregateException e)
			{
				Console.WriteLine(e.InnerException != null);
				Console.WriteLine(e.InnerExceptions.Count());
			}

			try
			{
				throw new AggregateException(new Exception(), new Exception(), new Exception());
			}
			catch (AggregateException e)
			{
				Console.WriteLine(e.InnerException != null);
				Console.WriteLine(e.InnerExceptions.Count());
			}
		}

		private static void Test12()
		{
			Queue<string> q = new Queue<string>();

			q.Enqueue("AAA");
			q.Enqueue("BBB");
			q.Enqueue("CCC");

			foreach (string str in q) // ここでは Dequeue されない。
				Console.WriteLine("foreach -> " + str);

			Console.WriteLine(q.Dequeue());
			Console.WriteLine(q.Dequeue());
			Console.WriteLine(q.Dequeue());
		}

		private static void Test13()
		{
			string[] ss = "AAA:BBB:CCC".Split(':');

			foreach (string s in ss)
			{
				if (s == "BBB")
				{
					ss[2] = "XXX"; // CCC -> XXX // <---- OK
				}
				Console.WriteLine(s);
			}
		}

		private static void Test13_B()
		{
			List<string> ss = new List<string>("AAA:BBB:CCC".Split(':'));

			foreach (string s in ss)
			{
				if (s == "BBB")
				{
					ss[2] = "XXX"; // CCC -> XXX // <---- 例外！！！
				}
				Console.WriteLine(s);
			}
			foreach (string s in ss)
			{
				if (s == "BBB")
				{
					ss.Add("DDD"); // <---- 例外！！！
				}
				Console.WriteLine(s);
			}
		}

		private static void Test14()
		{
			double nan = double.NaN;
			double inf = double.PositiveInfinity;
			double nif = double.NegativeInfinity;

			Console.WriteLine(string.Join(", ", nan, inf, nif));
			Console.WriteLine(string.Join(", ", nan < 0, inf < 0, nif < 0));
			Console.WriteLine(string.Join(", ", nan > 0, inf > 0, nif > 0));
			Console.WriteLine(string.Join(", ", nan == 0, inf == 0, nif == 0));
			Console.WriteLine(string.Join(", ", Math.Min(nan, 0), Math.Min(inf, 0), Math.Min(nif, 0)));
			Console.WriteLine(string.Join(", ", Math.Max(nan, 0), Math.Max(inf, 0), Math.Max(nif, 0)));
			Console.WriteLine(string.Join(", ", (int)nan, (int)inf, (int)nif));
			Console.WriteLine(string.Join(", ", (long)nan, (long)inf, (long)nif));
			Console.WriteLine(string.Join(", ", nan + 1, inf + 1, nif + 1));
			Console.WriteLine(string.Join(", ", nan - 1, inf - 1, nif - 1));
			Console.WriteLine(string.Join(", ", nan * 0, inf * 0, nif * 0));
			Console.WriteLine(string.Join(", ", nan * 1, inf * 1, nif * 1));
			Console.WriteLine(string.Join(", ", nan * -1, inf * -1, nif * -1));
			Console.WriteLine(string.Join(", ", nan / -1, inf / -1, nif / -1));
			Console.WriteLine(string.Join(", ", nan / 1, inf / 1, nif / 1));
			Console.WriteLine(string.Join(", ", nan / 0, inf / 0, nif / 0));
			Console.WriteLine(string.Join(", ", 1 / nan, 1 / inf, 1 / nif));
			Console.WriteLine(string.Join(", ", 0 / nan, 0 / inf, 0 / nif));
			Console.WriteLine(string.Join(", ", nan + 0.5, inf + 0.5, nif + 0.5));
			Console.WriteLine(string.Join(", ", nan - 0.5, inf - 0.5, nif - 0.5));
			Console.WriteLine(string.Join(", ", nan * 0.5, inf * 0.5, nif * 0.5));
			Console.WriteLine(string.Join(", ", nan / 0.5, inf / 0.5, nif / 0.5));
		}

		private static void Test15()
		{
			Console.WriteLine(string.Join(", ", Enumerable.Range(0, 10)));
			Console.WriteLine(string.Join(", ", Enumerable.Range(1, 10)));
			Console.WriteLine(string.Join(", ", Enumerable.Range(101, 8)));
		}

		private static void Test16()
		{
			int[,] tbl = new int[2, 3];

			for (int x = 0; x < 2; x++)
				for (int y = 0; y < 3; y++)
					tbl[x, y] = x + y;

			foreach (int v in tbl)
				Console.WriteLine(v);

			Console.WriteLine("*" + tbl.Length);

			// ----

			tbl = new int[,]
			{
				{ 1, 2 },
				{ 3, 4 },
				{ 5, 6 },
				{ 7, 8 },
			};

			foreach (int v in tbl)
				Console.WriteLine(v);

			Console.WriteLine("*" + tbl.Length);
		}
	}
}
