using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Charlotte
{
	public class ReflecTools
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="typeName">ex. namespace + "." + class_name</param>
		/// <returns></returns>
		public static FieldInfo[] GetFields(string typeName)
		{
			Type type = Type.GetType(typeName);

			if (type == null)
				throw new Exception("そんなタイプありません：" + typeName);

			return GetFields(type);
		}

		public static FieldInfo[] GetFields(object instance)
		{
			return GetFields(instance.GetType());
		}

		private const BindingFlags _bindingFlags =
			BindingFlags.Public |
			BindingFlags.NonPublic |
			BindingFlags.Static |
			BindingFlags.Instance;

		public static FieldInfo[] GetFields(Type type)
		{
			FieldInfo[] result = type.GetFields(_bindingFlags);
			return result;
		}

		/// <summary>
		/// name が見つからない場合 null を返す。
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static FieldInfo GetField(object instance, string name)
		{
			return GetField(instance.GetType(), name);
		}

		public static FieldInfo GetField(Type type, string name)
		{
			FieldInfo result = type.GetField(name, _bindingFlags);
			return result;
		}

		public static bool IsSame(FieldInfo fieldInfo, Type type)
		{
			return IsSame(fieldInfo.FieldType, type);
		}

		public static bool IsSame(Type type1, Type type2)
		{
			//SystemTools.WriteLog("type1: " + type1 + ", type2: " + type2); // test
			return type1.ToString() == type2.ToString();
		}

		public static object GetValue(FieldInfo fieldInfo, object instance)
		{
			return fieldInfo.GetValue(instance);
		}

		public static void SetValue(FieldInfo fieldInfo, object instance, object value)
		{
			fieldInfo.SetValue(instance, value);
		}
	}
}
