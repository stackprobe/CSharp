using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte
{
	public class KnownHashes
	{
		private Dictionary<string, string> Hash2Prev = DictionaryTools.CreateIgnoreCase<string>();

		public bool Add(string hash, string prev)
		{
			if (this.Hash2Prev.ContainsKey(hash) == false)
			{
				this.Hash2Prev.Add(hash, prev);
				return true;
			}
			return false;
		}

		public string[] GetRoute(string hash)
		{
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
			return this.Hash2Prev[hash];
		}
	}
}
