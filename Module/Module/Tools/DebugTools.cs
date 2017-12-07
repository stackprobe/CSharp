using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;

namespace Charlotte.Tools
{
	public class DebugTools
	{
		public static object toListOrMap(object instance, int depth = 3)
		{
			if (instance == null)
				return null;

			if (depth <= 0)
				return instance;

			Type type = instance.GetType();

			if (type.IsPrimitive)
				return instance;

			if (type.IsArray)
			{
				ObjectList dest = new ObjectList();

				foreach (object element in (Array)instance)
					dest.add(toListOrMap(element, depth - 1));

				return dest;
			}
			if (ReflecTools.equalsOrBase(type, typeof(List<>)))
			{
				ObjectList dest = new ObjectList();

				foreach (object element in (IEnumerable)instance)
					dest.add(toListOrMap(element, depth - 1));

				return dest;
			}

			{
				ObjectMap dest = ObjectMap.create();

				foreach (FieldInfo field in ReflecTools.getFields(instance))
					dest.add(field.Name, toListOrMap(ReflecTools.getValue(field, instance), depth - 1));

				return dest;
			}
		}
	}
}
