using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte
{
	public class KnownHashes
	{
		public KnownHashes()
		{
			Console.WriteLine("KH.1");
			FileTools.Delete(Consts.KNOWN_HASH_ROOT_DIR);
			Console.WriteLine("KH.2");
			FileTools.CreateDir(Consts.KNOWN_HASH_ROOT_DIR);
			Console.WriteLine("KH.3");
		}

		public bool Add(string hash, string prev)
		{
			//if (StringTools.LiteValidate(hash, StringTools.hexadecimal, 16) == false) throw null; // test
			//if (StringTools.LiteValidate(prev, StringTools.hexadecimal, 16) == false) throw null; // test

			string d1 = hash.Substring(0, 2);
			string d2 = hash.Substring(2, 2);
			string d3 = hash.Substring(4, 2);
			string d4 = hash.Substring(6);

			string dir = Path.Combine(Consts.KNOWN_HASH_ROOT_DIR, d1, d2, d3, d4);

			if (Directory.Exists(dir))
				return false;

			string file = Path.Combine(dir, prev);

			FileTools.CreateDir(dir);
			File.WriteAllBytes(file, BinTools.EMPTY);

			return true;
		}

		public string[] GetRoute(string hash)
		{
			//if (StringTools.LiteValidate(hash, StringTools.hexadecimal, 16) == false) throw null; // test

			List<string> dest = new List<string>();

			dest.Add(hash);

			for (; ; )
			{
				string prev = this.GetPrev(dest[dest.Count - 1]);

				if (prev == Consts.PREV_NONE)
					break;

				dest.Add(prev);
			}
			dest.Reverse();
			return dest.ToArray();
		}

		private string GetPrev(string hash)
		{
			string d1 = hash.Substring(0, 2);
			string d2 = hash.Substring(2, 2);
			string d3 = hash.Substring(4, 2);
			string d4 = hash.Substring(6);

			string dir = Path.Combine(Consts.KNOWN_HASH_ROOT_DIR, d1, d2, d3, d4);
			string[] files = Directory.GetFiles(dir);

			if (files.Length != 1)
				throw new Exception("そんなハッシュ知りません。");

			string prev = Path.GetFileName(files[0]);
			prev = prev.ToLower();

			if (StringTools.LiteValidate(prev, StringTools.hexadecimal, 16) == false) throw null; // test

			return prev;
		}
	}
}
