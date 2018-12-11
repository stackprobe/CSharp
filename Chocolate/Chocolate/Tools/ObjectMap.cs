using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class ObjectMap
	{
		public class ValueInfo
		{
			public object Value;
			public long Index;
			public string Key;
		}

		private Dictionary<string, ValueInfo> Inner;
		private long Counter = 0L;

		public static ObjectMap Create()
		{
			return new ObjectMap()
			{
				Inner = new Dictionary<string, ValueInfo>(new StringTools.IEComp()),
			};
		}

		public static ObjectMap CreateIgnoreCase()
		{
			return new ObjectMap()
			{
				Inner = new Dictionary<string, ValueInfo>(new StringTools.IECompIgnoreCase()),
			};
		}

		private ObjectMap()
		{ }

		public void Add(Dictionary<object, object> dict)
		{
			foreach (KeyValuePair<object, object> pair in dict)
			{
				this.Add(pair.Key, pair.Value);
			}
		}

		public void Add(object key, object value)
		{
			this.Inner.Add("" + key, new ValueInfo()
			{
				Value = value,
				Index = this.Counter++,
				Key = "" + key,
			});
		}

		public int Count
		{
			get
			{
				return this.Inner.Count;
			}
		}

		public object this[object key]
		{
			get
			{
				return this.Inner["" + key].Value;
			}

			set
			{
				if (this.Inner.ContainsKey("" + key))
					this.Inner["" + key].Value = value;
				else
					this.Add(key, value);
			}
		}

		public IEnumerable<string> GetKeySet()
		{
			return this.Inner.Keys;
		}

		public List<ValueInfo> GetInfos()
		{
			List<ValueInfo> infos = new List<ValueInfo>(this.Inner.Values);
			infos.Sort((a, b) => LongTools.Comp(a.Index, b.Index));
			return infos;
		}

		public IEnumerable<string> GetKeys()
		{
			return GetInfos().Select(info => info.Key);
		}

		public IEnumerable<object> GetValues()
		{
			return GetInfos().Select(info => info.Value);
		}

		public Dictionary<string, ValueInfo> Direct()
		{
			return this.Inner;
		}
	}
}
