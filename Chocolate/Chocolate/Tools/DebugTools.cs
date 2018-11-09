using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

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
			if (ReflectTools.EqualsOrBase(type, typeof(string)))
				return instance;

			if (ReflectTools.IsList(type))
			{
				ObjectList dest = new ObjectList();

				foreach (object element in (IEnumerable)instance)
					dest.Add(ToListOrMap(element, depth - 1));

				return dest;
			}

			{
				ObjectMap dest = ObjectMap.Create();

				foreach (ReflectTools.FieldBox field in ReflectTools.GetFieldsByInstance(instance))
					dest.Add(field.Value.Name, ToListOrMap(ReflectTools.GetValue(field, instance), depth - 1));

				foreach (ReflectTools.PropertyBox prop in ReflectTools.GetPropertiesByInstance(instance))
					dest.Add(prop.Value.Name, ToListOrMap(ReflectTools.GetValue(prop, instance), depth - 1));

				return dest;
			}
		}

		public static void GoToHomeSig()
		{
			while (File.Exists("home.sig") == false)
			{
				if (Directory.GetCurrentDirectory().Length <= 3)
					throw new Exception("no home.sig");

				Directory.SetCurrentDirectory("..");
			}
		}
	}
}
