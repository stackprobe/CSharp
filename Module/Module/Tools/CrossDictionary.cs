using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class CrossDictionary<K, V>
	{
		private Dictionary<K, V> _kv = new Dictionary<K, V>();
		private Dictionary<V, K> _vk = new Dictionary<V, K>();

		public void Add(K key, V value)
		{
			_kv.Add(key, value);
			_vk.Add(value, key);
		}

		public Dictionary<K, V> Values
		{
			get
			{
				return _kv;
			}
		}

		public Dictionary<V, K> Keys
		{
			get
			{
				return _vk;
			}
		}
	}
}
