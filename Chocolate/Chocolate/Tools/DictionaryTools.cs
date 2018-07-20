﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class DictionaryTools
	{
		public static Dictionary<string, V> Create<V>()
		{
			return new Dictionary<string, V>(new StringTools.IEComp());
		}

		public static Dictionary<string, V> CreateIgnoreCase<V>()
		{
			return new Dictionary<string, V>(new StringTools.IECompIgnoreCase());
		}
	}
}
