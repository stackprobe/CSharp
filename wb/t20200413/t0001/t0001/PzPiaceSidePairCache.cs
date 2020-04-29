using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public static class PzPiaceSidePairCache
	{
		private static CacheMap<string, PzPiaceSidePair> _cache = DictionaryTools.CreateCache(ident => CreatePair());

		private static PzPiaceSide _a;
		private static PzPiaceSide _b;

		public static PzPiaceSidePair GetPair(PzPiaceSide a, PzPiaceSide b)
		{
			try
			{
				_a = a;
				_b = b;

				return _cache[PzPiaceSidePair.GetIdent(a, b)];
			}
			finally
			{
				_a = null;
				_b = null;
			}
		}

		private static PzPiaceSidePair CreatePair()
		{
			return new PzPiaceSidePair(_a, _b);
		}
	}
}
