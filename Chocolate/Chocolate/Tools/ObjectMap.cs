using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class ObjectMap
	{
		private Dictionary<string, object> Inner;

		public static ObjectMap Create()
		{
			return new ObjectMap()
			{
				Inner = new Dictionary<string, object>(new StringTools.IEComp()),
			};
		}

		public static ObjectMap CreateIgnoreCase()
		{
			return new ObjectMap()
			{
				Inner = new Dictionary<string, object>(new StringTools.IECompIgnoreCase()),
			};
		}

		private ObjectMap()
		{ }

		public void Add(Dictionary<object, object> map)
		{
			foreach (KeyValuePair<object, object> pair in map)
			{
				this.Add(pair.Key, pair.Value);
			}
		}

		public void Add(object key, object value)
		{
			this.Inner.Add("" + key, value);
		}

		public int Count
		{
			get { return this.Inner.Count; }
		}

		public object this[string key]
		{
			get { return this.Inner[key]; }
			set { this.Inner[key] = value; }
		}

		public ICollection<string> GetKeys()
		{
			return this.Inner.Keys;
		}

		public bool ContainsKey(string key)
		{
			return this.Inner.ContainsKey(key);
		}
	}
}
