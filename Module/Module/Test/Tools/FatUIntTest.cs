using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Test.Tools
{
	public class FatUIntTest
	{
		public static void Test01()
		{
			for (int c = 0; c < 10; c++)
			{
				Console.WriteLine("3." + c);

				Test01_3b(
					DebugTools.MakeRandString(StringTools.DIGIT, MathTools.Random(10)),
					DebugTools.MakeRandString(StringTools.DIGIT, MathTools.Random(10)),
					DebugTools.MakeRandString(StringTools.DIGIT, MathTools.Random(30)),
					DebugTools.MakeRandString(StringTools.DIGIT, MathTools.Random(30)),
					DebugTools.MakeRandString(StringTools.DIGIT, MathTools.Random(100)),
					DebugTools.MakeRandString(StringTools.DIGIT, MathTools.Random(100))
					);
			}
			//if (1 == 1) return;

			for (int c = 0; c < 10; c++)
			{
				Test01_2(DebugTools.MakeRandString(StringTools.DIGIT, 10));
				Test01_2(DebugTools.MakeRandString(StringTools.DIGIT, 30));
				Test01_2(DebugTools.MakeRandString(StringTools.DIGIT, 100));
			}
			//if (1 == 1) return;

			Test01_c("2", "/", "1");

			Test01_b("", "");
			Test01_b("", "0");
			Test01_b("0", "");
			Test01_b("0", "0");

			Test01_b2("0", "1", "2", "3", "4", "5");

			for (int c = 0; c < 10; c++)
			{
				Console.WriteLine("0." + c);

				Test01_b2(
					DebugTools.MakeRandString(StringTools.DIGIT, MathTools.Random(10)),
					DebugTools.MakeRandString(StringTools.DIGIT, MathTools.Random(20)),
					DebugTools.MakeRandString(StringTools.DIGIT, MathTools.Random(30))
					);
			}
			for (int c = 0; c < 10; c++)
			{
				Console.WriteLine("1." + c);

				Test01_b2(
					DebugTools.MakeRandString(StringTools.DIGIT, MathTools.Random(10)),
					DebugTools.MakeRandString(StringTools.DIGIT, MathTools.Random(30)),
					DebugTools.MakeRandString(StringTools.DIGIT, MathTools.Random(100))
					);
			}
			for (int c = 0; c < 10; c++)
			{
				Console.WriteLine("2." + c);

				Test01_b2(
					DebugTools.MakeRandString(StringTools.DIGIT + ",", MathTools.Random(100)),
					DebugTools.MakeRandString(StringTools.DIGIT + ",", MathTools.Random(150)),
					DebugTools.MakeRandString(StringTools.DIGIT + ",", MathTools.Random(200))
					);
			}
		}

		private static void Test01_b2(params string[] prms)
		{
			for (int a = 0; a < prms.Length; a++)
			{
				for (int b = 0; b < prms.Length; b++)
				{
					Test01_b(prms[a], prms[b]);
				}
			}
		}

		private static void Test01_b(string a, string b)
		{
			Test01_c(a, "+", b);
			Test01_c(a, "-", b);
			Test01_c(a, "*", b);
			Test01_c(a, "/", b);
			Test01_c(a, "%", b);
		}

		private static void Test01_c(string a, string operation, string b)
		{
			Console.WriteLine("Go [" + a + "] " + operation + " [" + b + "]");

			string ans;

			try
			{
				//Console.WriteLine("*1"); // test
				FatUInt v1 = FatUInt.FromString(a);
				//Console.WriteLine("*2: " + v1); // test
				FatUInt v2 = FatUInt.FromString(b);
				//Console.WriteLine("*3: " + v2); // test
				FatUInt v3;

				//DebugTools.WriteLog("[" + a + "] -> " + v1);
				//DebugTools.WriteLog("[" + b + "] -> " + v2);

				switch (operation)
				{
					case "+": v3 = FatUInt.Add(v1, v2); break;
					case "-": v3 = FatUInt.Red(v1, v2); break;
					case "*": v3 = FatUInt.Mul(v1, v2); break;
					case "/": v3 = FatUInt.Div(v1, v2); break;
					case "%": v3 = FatUInt.Mod(v1, v2); break;

					default:
						throw null;
				}
				//Console.WriteLine("*4"); // test
				ans = v3.GetString();
				//Console.WriteLine("*5"); // test

				//DebugTools.WriteLog("[" + ans + "] <- " + v3);
			}
			catch (Exception e)
			{
				//*
				ans = e.Message;
				/*/
				ans = e.ToString();
				//*/
			}
			DebugTools.WriteLog("[" + a + "] " + operation + " [" + b + "] = [" + ans + "]");

			Console.WriteLine("Done -> [" + ans + "]");
		}

		private static void Test01_2(string a)
		{
			string b = FatUInt.Power(FatUInt.FromString(a), 2).GetString();
			string c = FatUInt.Root(FatUInt.FromString(b), 2).GetString();

			DebugTools.WriteLog("[" + a + "] ^2 ->");
			DebugTools.WriteLog("[" + b + "] ^0.5 ->");
			DebugTools.WriteLog("[" + c + "]");
		}

		private static void Test01_3b(params string[] prms)
		{
			for (int a = 0; a < prms.Length; a++)
			{
				for (int b = 0; b < prms.Length; b++)
				{
					Test01_3(prms[a], prms[b]);
				}
			}
		}

		private static void Test01_3(string a, string b)
		{
			a = TestNormalize(a);
			b = TestNormalize(b);

			DebugTools.WriteLog("Test01_3: [" + a + "], [" + b + "]");

			{
				string c = TestCalc(a, "+", b);
				string d = TestCalc(b, "+", a);

				if (c != d)
					throw null;
			}

			{
				string c = TestCalc(a, "+", b);
				string d = TestCalc(c, "-", b);

				if (a != d)
					throw null;
			}

			if (b != "0")
			{
				string c = TestCalc(a, "*", b);
				string d = TestCalc(c, "/", b);
				string e = TestCalc(c, "%", b);

				if (a != d)
					throw null;

				if (e != "0")
					throw null;
			}

			DebugTools.WriteLog("Done");
		}

		private static string TestNormalize(string str)
		{
			while (1 <= str.Length && str[0] == '0')
				str = str.Substring(1);

			if (str == "")
				str = "0";

			return str;
		}

		private static string TestCalc(string a, string operation, string b)
		{
			FatUInt v1 = FatUInt.FromString(a);
			FatUInt v2 = FatUInt.FromString(b);
			FatUInt v3;

			switch (operation)
			{
				case "+": v3 = FatUInt.Add(v1, v2); break;
				case "-": v3 = FatUInt.Red(v1, v2); break;
				case "*": v3 = FatUInt.Mul(v1, v2); break;
				case "/": v3 = FatUInt.Div(v1, v2); break;
				case "%": v3 = FatUInt.Mod(v1, v2); break;

				default:
					throw null;
			}
			return v3.GetString();
		}
	}
}
