using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Charlotte.Tools
{
	public class ReflecTools
	{
		public class FieldBox
		{
			public FieldInfo Value;

			public FieldBox(FieldInfo value)
			{
				this.Value = value;
			}
		}

		public class PropertyBox
		{
			public PropertyInfo Value;

			public PropertyBox(PropertyInfo value)
			{
				this.Value = value;
			}
		}

		public class MethodBox
		{
			public MethodBase Value; // MethodInfo | ConstructorInfo

			public MethodBox(MethodBase value)
			{
				this.Value = value;
			}

			public ParameterBox[] GetParameters()
			{
				return this.Value.GetParameters().Select<ParameterInfo, ParameterBox>(prm => new ParameterBox(prm)).ToArray();
			}

			public object Construct(object[] prms)
			{
				return ((ConstructorInfo)this.Value).Invoke(prms);
			}
		}

		public class ParameterBox
		{
			public ParameterInfo Value;

			public ParameterBox(ParameterInfo value)
			{
				this.Value = value;
			}
		}

		public static FieldBox[] GetFieldsByTypeName(string typeName)
		{
			Type type = Type.GetType(typeName);

			if (type == null)
				throw new Exception("指定されたタイプが見つかりません。" + typeName);

			return GetFieldsByType(type);
		}

		public static FieldBox[] GetFields(object instance)
		{
			return GetFieldsByType(instance.GetType());
		}

		public static PropertyBox[] GetProperties(object instance)
		{
			return GetPropertiesByType(instance.GetType());
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

		public static FieldBox[] GetFieldsByType(Type type)
		{
			return type.GetFields(_bindingFlags).Select<FieldInfo, FieldBox>(field => new FieldBox(field)).ToArray();
		}

		public static PropertyBox[] GetPropertiesByType(Type type)
		{
			return type.GetProperties(_bindingFlags).Select<PropertyInfo, PropertyBox>(prop => new PropertyBox(prop)).ToArray();
		}

		/// <summary>
		/// name が見つからない場合 null を返す。
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static FieldBox GetField(object instance, string name)
		{
			return GetFieldByType(instance.GetType(), name);
		}

		public static FieldBox GetFieldByType(Type type, string name)
		{
			return new FieldBox(type.GetField(name, _bindingFlags));
		}

		public static bool Equals(FieldBox a, Type b)
		{
			return Equals(a.Value.FieldType, b);
		}

		public static bool Equals(Type a, Type b)
		{
			return a.ToString() == b.ToString();
		}

		public static bool EqualsOrBase(FieldBox a, Type b)
		{
			return EqualsOrBase(a.Value.FieldType, b);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns>a == b 又は a は b を継承している。</returns>
		public static bool EqualsOrBase(Type a, Type b)
		{
			do
			{
				if (Equals(a, b))
					return true;

				foreach (Type ai in a.GetInterfaces())
					if (Equals(ai, b))
						return true;

				a = a.BaseType;
			}
			while (a != null);

			return false;
		}

		public static object GetValue(FieldBox field, object instance)
		{
			return field.Value.GetValue(instance);
		}

		public static void SetValue(FieldBox field, object instance, object value)
		{
			field.Value.SetValue(instance, value);
		}

		public static object GetValue(PropertyBox prop, object instance)
		{
			try
			{
				return prop.Value.GetValue(instance, null);
			}
			catch
			{
				return null; // getter無し || 配列 || etc.
			}
		}

		public static bool IsList(object instance)
		{
			return IsListByType(instance.GetType());
		}

		public static bool IsListByType(Type type)
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

		public static MethodBox[] GetMethods(object instance)
		{
			return GetMethodsByType(instance.GetType());
		}

		public static MethodBox[] GetMethodsByType(Type type)
		{
			return type.GetMethods(_bindingFlags).Select<MethodInfo, MethodBox>(method => new MethodBox(method)).ToArray();
		}

		public static MethodBox[] GetConstructors(object instance)
		{
			return GetConstructorsByType(instance.GetType());
		}

		public static MethodBox[] GetConstructorsByType(Type type)
		{
			return type.GetConstructors(_bindingFlags).Select<ConstructorInfo, MethodBox>(constructor => new MethodBox(constructor)).ToArray();
		}
	}
}
