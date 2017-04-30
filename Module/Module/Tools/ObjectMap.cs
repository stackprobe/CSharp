using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class ObjectMap
	{
		private Dictionary<string, object> _map = new Dictionary<string, object>();

		public void add(Dictionary<object, object> map)
		{
			foreach (object key in map.Keys)
			{
				add(key, map[key]);
			}
		}

		public void add(object key, object value)
		{
			_map.Add("" + key, value);
		}

		public int getCount()
		{
			return _map.Count;
		}

		public ICollection<string> getKeys()
		{
			return _map.Keys;
		}

		public object this[string key]
		{
			get
			{
				return _map[key];
			}
		}
	}
}
