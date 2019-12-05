using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class CrossDictionary<K, V>
	{
		private Dictionary<K, V> Key_Value;
		private Dictionary<V, K> Value_Key;

		public CrossDictionary(Dictionary<K, V> key_value_binding, Dictionary<V, K> value_key_binding)
		{
			this.Key_Value = key_value_binding;
			this.Value_Key = value_key_binding;
		}

		public void Clear()
		{
			this.Key_Value.Clear();
			this.Value_Key.Clear();
		}

		public void Add(K key, V value)
		{
			if (this.Key_Value.ContainsKey(key))
				throw new Exception("キーの重複");

			if (this.Value_Key.ContainsKey(value))
				throw new Exception("値の重複");

			this.Key_Value.Add(key, value);
			this.Value_Key.Add(value, key);
		}

		public V this[K key]
		{
			get
			{
				return this.Key_Value[key];
			}
		}

		public K GetKey(V value)
		{
			return this.Value_Key[value];
		}

		public IEnumerable<K> Keys()
		{
			return this.Key_Value.Keys;
		}

		public IEnumerable<V> Values()
		{
			return this.Value_Key.Keys;
		}
	}
}
