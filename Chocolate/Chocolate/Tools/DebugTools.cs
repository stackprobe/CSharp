using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Charlotte.Tools
{
	public class DebugTools
	{
		public static object ToListOrMap(object instance, int depth = 3)
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
					dest.Add(ToListOrMap(element, depth - 1));

				return dest;
			}
			if (ReflecTools.EqualsOrBase(type, typeof(string)))
				return instance;

			if (ReflecTools.IsList(type))
			{
				ObjectList dest = new ObjectList();

				foreach (object element in (IEnumerable)instance)
					dest.Add(ToListOrMap(element, depth - 1));

				return dest;
			}

			{
				ObjectMap dest = ObjectMap.Create();

				foreach (ReflecTools.FieldBox field in ReflecTools.GetFieldsByInstance(instance))
					dest.Add(field.Value.Name, ToListOrMap(ReflecTools.GetValue(field, instance), depth - 1));

				foreach (ReflecTools.PropertyBox prop in ReflecTools.GetPropertiesByInstance(instance))
					dest.Add(prop.Value.Name, ToListOrMap(ReflecTools.GetValue(prop, instance), depth - 1));

				return dest;
			}
		}
	}
}
