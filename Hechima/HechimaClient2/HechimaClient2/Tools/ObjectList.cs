using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class ObjectList
	{
		private List<object> _list = new List<object>();

		public ObjectList()
		{ }

		public ObjectList(params object[] arr)
		{
			this.addRange(arr);
		}

		public void addRange(ICollection<object> list)
		{
			_list.AddRange(list);
		}

		public void add(object obj)
		{
			_list.Add(obj);
		}

		public int getCount()
		{
			return _list.Count;
		}

		public List<object> getList()
		{
			return _list;
		}

		public object this[int index]
		{
			get
			{
				return _list[index];
			}
		}
	}
}
