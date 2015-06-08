﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Main3();
				//new Program().Main2();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			Console.WriteLine("\\e");
			Console.ReadLine();
		}

		private void Main3()
		{
			{
				CsvData cd = new CsvData();

				cd.ReadFile(@"C:\tmp\test.csv");
				cd.TTR();
				cd.WriteFile(@"C:\temp\test.csv");
			}

			{
				string src = "AAA\tBBB\rCCC\na$a$a$a\n$$$\n\n\n";
				string enc = EscapeString.I.Encode(src);
				string dec = EscapeString.I.Decode(enc);

				if (src != dec)
					throw new Exception();
			}

			{
				List<string> src = new List<string>();

				src.Add("aaa");
				src.Add("bbb");
				src.Add("a$a$a");
				src.Add("$$$");
				src.Add("///");
				src.Add("___");
				src.Add("");
				src.Add("1/2/3");
				src.Add("");

				string enc = AttachString.I.Untokenize(src);
				List<string> dec = AttachString.I.Tokenize(enc);

				if (src.Count != dec.Count)
					throw new Exception();

				for (int index = 0; index < src.Count; index++)
					if (src[index] != dec[index])
						throw new Exception();
			}
		}

		private void Main2()
		{
			Console.WriteLine("[" + "\t\n\r aaa]".Trim());
			//Console.WriteLine("[" + "\a\t\n\r aaa]".Trim());

			//Console.WriteLine("\a\a\a\a\a");
			//Console.WriteLine("\a");

			Console.WriteLine("[" + "\t\n\r aaa\t_\n_\r_ aaa]\t\n\r ".Trim());

			Print(":::".Split(':'));
			Print("a:::".Split(':'));
			Print(":a::".Split(':'));
			Print("::a:".Split(':'));
			Print(":::a".Split(':'));
			Print(":\t:\n:".Split(':'));
			Print("  :  :  :  ".Split(':'));

			{
				List<string> lines = new List<string>();

				lines.Add("aaa");
				lines.Remove("aaa");

				Console.WriteLine("lines.Count: " + lines.Count); // 0

				lines.Remove("abcdef");

				Console.WriteLine("lines.Count: " + lines.Count); // 0

				lines.Add("aaa");
				lines.Remove("abcdef");

				Console.WriteLine("lines.Count: " + lines.Count); // 1
			}
		}

		private void Print(string[] strs)
		{
			Console.WriteLine("bgn");

			foreach (string str in strs)
			{
				Console.WriteLine("[" + str + "]");
			}
			Console.WriteLine("end");
		}
	}
}
