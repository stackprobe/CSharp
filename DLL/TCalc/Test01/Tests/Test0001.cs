using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.TCalcs;
using Charlotte.Tools;
using System.Text.RegularExpressions;

namespace Charlotte.Tests
{
	public class Test0001
	{
		/// <summary>
		/// 2^127 - 1 == メルセンヌ素数
		/// </summary>
		public const string S2P127_1 = "170141183460469231731687303715884105727";

		/// <summary>
		/// 2^607 - 1 == メルセンヌ素数
		/// </summary>
		public const string S2P607_1 = "531137992816767098689588206552468627329593117727031923199444138200403559860852242739162502265229285668889329486246501015346579337652707239409519978766587351943831270835393219031728127";

		/// <summary>
		/// 2^1279 - 1 == メルセンヌ素数
		/// </summary>
		public const string S2P1279_1 = "10407932194664399081925240327364085538615262247266704805319112350403608059673360298012239441732324184842421613954281007791383566248323464908139906605677320762924129509389220345773183349661583550472959420547689811211693677147548478866962501384438260291732348885311160828538416585028255604666224831890918801847068222203140521026698435488732958028878050869736186900714720710555703168729087";

		/// <summary>
		/// 2^4253 - 1 == メルセンヌ素数
		/// </summary>
		public const string S2P4253_1 = "190797007524439073807468042969529173669356994749940177394741882673528979787005053706368049835514900244303495954950709725762186311224148828811920216904542206960744666169364221195289538436845390250168663932838805192055137154390912666527533007309292687539092257043362517857366624699975402375462954490293259233303137330643531556539739921926201438606439020075174723029056838272505051571967594608350063404495977660656269020823960825567012344189908927956646011998057988548630107637380993519826582389781888135705408653045219655801758081251164080554609057468028203308718724654081055323215860189611391296030471108443146745671967766308925858547271507311563765171008318248647110097614890313562856541784154881743146033909602737947385055355960331855614540900081456378659068370317267696980001187750995491090350108417050917991562167972281070161305972518044872048331306383715094854938415738549894606070722584737978176686422134354526989443028353644037187375385397838259511833166416134323695660367676897722287918773420968982326089026150031515424165462111337527431154890666327374921446276833564519776797633875503548665093914556482031482248883127023777039667707976559857333357013727342079099064400455741830654320379350833236245819348824064783585692924881021978332974949906122664421376034687815350484991";

		public void Test01()
		{
			for (int c = 0; c <= 100; c++)
			{
				Test01_b("" + c);
			}

			Test01_Bnd(S2P127_1);
			Test01_Bnd(S2P607_1);

			//Test01_Bnd(S2P1279_1);
			//Test01_Bnd(S2P4253_1);
		}

		private void Test01_Bnd(string val)
		{
			Test01_b(TCalc_Int.Calc(val, "-", "5"));
			Test01_b(TCalc_Int.Calc(val, "-", "4"));
			Test01_b(TCalc_Int.Calc(val, "-", "3"));
			Test01_b(TCalc_Int.Calc(val, "-", "2"));
			Test01_b(TCalc_Int.Calc(val, "-", "1"));
			Test01_b(val);
			Test01_b(TCalc_Int.Calc(val, "+", "1"));
			Test01_b(TCalc_Int.Calc(val, "+", "2"));
			Test01_b(TCalc_Int.Calc(val, "+", "3"));
			Test01_b(TCalc_Int.Calc(val, "+", "4"));
			Test01_b(TCalc_Int.Calc(val, "+", "5"));
		}

		private void Test01_b(string val)
		{
			Console.WriteLine(val + " is prime ? --> " + IsPrime(val));
		}

		private static TCalc TCalc_Int = new TCalc(10, 0);

		private bool IsPrime(string val)
		{
			if (!StringTools.LiteValidate(val, StringTools.DECIMAL))
				throw new ArgumentException();

			val = TCalc_Int.Calc(val, "+", "0");

			if (Regex.IsMatch(val, "^[01]$"))
				return false;

			if (Regex.IsMatch(val, "^[23]$"))
				return true;

			if (IsEven(val))
				return false;

			const int MR_K = 50;

			string d = val;
			string x;
			string val_1 = TCalc_Int.Calc(val, "-", "1");
			string val_3 = TCalc_Int.Calc(val, "-", "3");
			int r;
			int c;
			int k;

			d = EraseDotAster(TCalc_Int.Calc(d, "/", "2"));

			for (r = 0; IsEven(d); r++)
				d = TCalc_Int.Calc(d, "/", "2");

			for (k = 0; k < MR_K; k++)
			{
				//Console.WriteLine("k: " + k); // test

				x = SecurityTools.MakePassword(StringTools.DECIMAL, val.Length + 10); // + margin
				x = TCalc_Int.Calc(
					x,
					"-",
					TCalc_Int.Calc(
						EraseDotAster(TCalc_Int.Calc(x, "/", val_3)),
						"*",
						val_3
						)
					);
				x = TCalc_Int.Calc(x, "+", "2");

				x = ModPow(x, d, val);

				if (x != "1" && x != val_1)
				{
					for (c = r; ; c--)
					{
						if (c <= 0)
							return false;

						x = ModPow(x, "2", val);

						if (x == val_1)
							break;
					}
				}
			}
			return true;
		}

		private bool IsEven(string val)
		{
			return "02468".Contains(val[val.Length - 1]);
		}

		private string ModPow(string val, string exp, string mod)
		{
			string ret = "1";

			for (; ; )
			{
				if (!IsEven(exp))
					ret = ModMul(ret, val, mod);

				exp = EraseDotAster(TCalc_Int.Calc(exp, "/", "2"));

				if (exp == "0")
					break;

				val = ModMul(val, val, mod);
			}
			ret = Mod(ret, mod);
			return ret;
		}

		private string ModMul(string a, string b, string mod)
		{
			return Mod(TCalc_Int.Calc(a, "*", b), mod);
		}

		private string Mod(string val, string mod)
		{
			return TCalc_Int.Calc(
				val,
				"-",
				TCalc_Int.Calc(
					EraseDotAster(TCalc_Int.Calc(val, "/", mod)),
					"*",
					mod
					)
				);
		}

		private string EraseDotAster(string val)
		{
			val = val.Replace(".", "");
			val = val.Replace("*", "");
			return val;
		}
	}
}
