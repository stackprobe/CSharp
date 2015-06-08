using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class MapData<K, V>
	{
		private Dictionary<K, V> Map = new Dictionary<K, V>();

		public MapData()
		{ }

		public void Remove(K key)
		{
			if (this.Map.ContainsKey(key))
				this.Map.Remove(key);
		}

		public void Put(K key, V value)
		{
			this.Remove(key);
			this.Map.Add(key, value);
		}

		public V Get(K key, V defval)
		{
			if (this.Map.ContainsKey(key) == false)
				return defval;

			return this.Map[key];
		}

		public IEnumerable<K> GetKeys()
		{
			return this.Map.Keys;
		}

		public void Clear()
		{
			this.Map.Clear();
		}
	}
}
