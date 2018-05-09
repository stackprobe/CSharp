using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class ObjectList
	{
		public static ObjectList Create(ICollection<object> list)
		{
			ObjectList dest = new ObjectList();
			dest.AddRange(list);
			return dest;
		}

		private List<object> Inner = new List<object>();

		public ObjectList()
		{ }

		public ObjectList(params object[] arr)
		{
			this.AddRange(arr);
		}

		public void AddRange(ICollection<object> list)
		{
			this.Inner.AddRange(list);
		}

		public void Add(object obj)
		{
			this.Inner.Add(obj);
		}

		public int Count
		{
			get { return this.Inner.Count; }
		}

		public object this[int index]
		{
			get { return this.Inner[index]; }
		}

		public List<object> Direct()
		{
			return this.Inner;
		}
	}
}
