﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Satellite
{
	public class ObjectList
	{
		private List<object> List = new List<object>();

		public ObjectList()
		{ }

		public ObjectList(ICollection<object> list)
		{
			this.Add(list);
		}

		public void Add(ICollection<object> list)
		{
			this.List.AddRange(list);
		}

		public void Add(object obj)
		{
			this.List.Add(obj);
		}

		public int GetCount()
		{
			return this.List.Count;
		}

		public List<object> GetList()
		{
			return this.List;
		}
	}
}
