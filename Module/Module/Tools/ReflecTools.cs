using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Charlotte.Tools
{
	public static class ReflecTools
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="typeName">ex. namespace + "." + class_name</param>
		/// <returns></returns>
		public static FieldInfo[] getFields(string typeName)
		{
			Type type = Type.GetType(typeName);

			if (type == null)
				throw new Exception("そんなタイプありません：" + typeName);

			return getFieldsByType(type);
		}

		public static FieldInfo[] getFields(object instance)
		{
			return getFieldsByType(instance.GetType());
		}

		public static PropertyInfo[] getProperties(object instance)
		{
			return getPropertiesByType(instance.GetType());
		}

		/// <summary>
		/// 親クラスの private, private static が取ってこれないっぽいけど、名前が被ったとき面倒なので、これでいいや。
		/// </summary>
		private const BindingFlags _bindingFlags =
			BindingFlags.Public |
			BindingFlags.NonPublic |
			BindingFlags.Static |
			BindingFlags.Instance |
			BindingFlags.FlattenHierarchy;

		public static FieldInfo[] getFieldsByType(Type type)
		{
			FieldInfo[] result = type.GetFields(_bindingFlags);
			return result;
		}

		public static PropertyInfo[] getPropertiesByType(Type type)
		{
			PropertyInfo[] result = type.GetProperties(_bindingFlags);
			return result;
		}

		/// <summary>
		/// name が見つからない場合 null を返す。
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static FieldInfo getField(object instance, string name)
		{
			return getFieldByType(instance.GetType(), name);
		}

		public static FieldInfo getFieldByType(Type type, string name)
		{
			FieldInfo result = type.GetField(name, _bindingFlags);
			return result;
		}

		public static bool equals(FieldInfo a, Type b)
		{
			return equals(a.FieldType, b);
		}

		public static bool equals(Type a, Type b)
		{
			return a.ToString() == b.ToString();
		}

		public static bool equalsOrBase(FieldInfo a, Type b)
		{
			return equalsOrBase(a.FieldType, b);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns>a == b 又は a は b を継承している。</returns>
		public static bool equalsOrBase(Type a, Type b)
		{
			do
			{
				if (equals(a, b))
					return true;

				foreach (Type ai in a.GetInterfaces())
					if (equals(ai, b))
						return true;

				a = a.BaseType;
			}
			while (a != null);

			return false;
		}

		public static object getValue(FieldInfo fieldInfo, object instance)
		{
			return fieldInfo.GetValue(instance);
		}

		public static void setValue(FieldInfo fieldInfo, object instance, object value)
		{
			fieldInfo.SetValue(instance, value);
		}

		public static object getValue(PropertyInfo propInfo, object instance)
		{
			try
			{
				return propInfo.GetValue(instance, null);
			}
			catch
			{
				return null; // getter無し || 配列 || etc.
			}
		}

		public static bool isList(object instance)
		{
			return isListByType(instance.GetType());
		}

		public static bool isListByType(Type type)
		{
			try
			{
				return typeof(List<>).IsAssignableFrom(type.GetGenericTypeDefinition());
			}
			catch
			{
				return false; // ジェネリック型じゃない || etc.
			}
		}
	}
}
