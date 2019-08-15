using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public static class BluffListTools
	{
		public static BluffList<T> Create<T>(T[] entity_binding, T defval = default(T), int count = IntTools.IMAX)
		{
			T[] entity = entity_binding;

			return new BluffList<T>()
			{
				GetCount = () => count,
				GetElement = index =>
				{
					if (index < 0 || entity.Length <= index) // ? out of range
					{
						return defval;
					}
					return entity[index];
				},
				SetElement = null,
			};
		}
	}
}
