using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using System.IO;

namespace Charlotte.Tools
{
	public static class DebugTools
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
			if (ReflecTools.equalsOrBase(type, typeof(string)))
				return instance;

			if (ReflecTools.isListByType(type))
			//if (ReflecTools.equalsOrBase(type, typeof(List<>)))
			{
				ObjectList dest = new ObjectList();

				foreach (object element in (IEnumerable)instance)
					dest.add(toListOrMap(element, depth - 1));

				return dest;
			}

			{
				ObjectMap dest = ObjectMap.create();

				foreach (FieldInfo fieldInfo in ReflecTools.getFields(instance))
					dest.add(fieldInfo.Name, toListOrMap(ReflecTools.getValue(fieldInfo, instance), depth - 1));

				foreach (PropertyInfo propInfo in ReflecTools.getProperties(instance))
					dest.add(propInfo.Name, toListOrMap(ReflecTools.getValue(propInfo, instance), depth - 1));

				return dest;
			}
		}
	}
}
