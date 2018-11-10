using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Charlotte.Tools
{
	public class ReflectTools
	{
		public class FieldUnit
		{
			public FieldInfo Value;

			public FieldUnit(FieldInfo value)
			{
				this.Value = value;
			}
		}

		public class PropertyUnit
		{
			public PropertyInfo Value;

			public PropertyUnit(PropertyInfo value)
			{
				this.Value = value;
			}
		}

		public class MethodUnit
		{
			public MethodBase Value; // MethodInfo | ConstructorInfo

			public MethodUnit(MethodBase value)
			{
				this.Value = value;
			}

			public ParameterUnit[] GetParameters()
			{
				return this.Value.GetParameters().Select<ParameterInfo, ParameterUnit>(prm => new ParameterUnit(prm)).ToArray();
			}

			/// <summary>
			/// invoke static method
			/// </summary>
			/// <param name="prms"></param>
			/// <returns></returns>
			public object Invoke(object[] prms)
			{
				return this.Value.Invoke(null, prms);
			}

			/// <summary>
			/// invoke instance method
			/// </summary>
			/// <param name="instance"></param>
			/// <param name="prms"></param>
			/// <returns></returns>
			public object Invoke(object instance, object[] prms)
			{
				return this.Value.Invoke(instance, prms);
			}

			/// <summary>
			/// invoke constructor
			/// </summary>
			/// <param name="prms"></param>
			/// <returns></returns>
			public object Construct(object[] prms)
			{
				return ((ConstructorInfo)this.Value).Invoke(prms);
			}
		}

		public class ParameterUnit
		{
			public ParameterInfo Value;

			public ParameterUnit(ParameterInfo value)
			{
				this.Value = value;
			}
		}

		public static FieldUnit[] GetFieldsByInstance(object instance)
		{
			return GetFields(instance.GetType());
		}

		public static PropertyUnit[] GetPropertiesByInstance(object instance)
		{
			return GetProperties(instance.GetType());
		}

		public static FieldUnit GetFieldByInstance(object instance, string name)
		{
			return GetField(instance.GetType(), name);
		}

		public static PropertyUnit GetPropertyByInstance(object instance, string name)
		{
			return GetProperty(instance.GetType(), name);
		}

		/// <summary>
		/// 親クラスの private, private static を取得出来ない模様。現状名前が被った場合を考慮していないので、このままにする。
		/// </summary>
		private const BindingFlags _bindingFlags =
			BindingFlags.Public |
			BindingFlags.NonPublic |
			BindingFlags.Static |
			BindingFlags.Instance |
			BindingFlags.FlattenHierarchy;

		public static FieldUnit[] GetFields(Type type)
		{
			return type.GetFields(_bindingFlags).Select<FieldInfo, FieldUnit>(field => new FieldUnit(field)).ToArray();
		}

		public static PropertyUnit[] GetProperties(Type type)
		{
			return type.GetProperties(_bindingFlags).Select<PropertyInfo, PropertyUnit>(prop => new PropertyUnit(prop)).ToArray();
		}

		public static FieldUnit GetField(Type type, string name) // ret: null == not found
		{
			FieldInfo field = type.GetField(name, _bindingFlags);

			if (field == null)
				return null;

			return new FieldUnit(field);
		}

		public static PropertyUnit GetProperty(Type type, string name)
		{
			PropertyInfo prop = type.GetProperty(name, _bindingFlags);

			if (prop == null)
				return null;

			return new PropertyUnit(prop);
		}

		public static bool Equals(FieldUnit a, Type b)
		{
			return Equals(a.Value.FieldType, b);
		}

		public static bool Equals(Type a, Type b)
		{
			return a.ToString() == b.ToString();
		}

		public static bool EqualsOrBase(FieldUnit a, Type b)
		{
			return EqualsOrBase(a.Value.FieldType, b);
		}

		public static bool EqualsOrBase(Type a, Type b) // ret: ? a == b || a は b を継承している。
		{
			foreach (Type ai in a.GetInterfaces())
				if (Equals(ai, b))
					return true;

			do
			{
				if (Equals(a, b))
					return true;

				a = a.BaseType;
			}
			while (a != null);

			return false;
		}

		public static object GetValue(FieldUnit field, object instance)
		{
			return field.Value.GetValue(instance);
		}

		public static void SetValue(FieldUnit field, object instance, object value)
		{
			field.Value.SetValue(instance, value);
		}

		public static object GetValue(PropertyUnit prop, object instance)
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

		public static bool IsList(Type type)
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

		public static MethodUnit[] GetMethodsByInstance(object instance)
		{
			return GetMethods(instance.GetType());
		}

		public static MethodUnit[] GetMethods(Type type)
		{
			return type.GetMethods(_bindingFlags).Select<MethodInfo, MethodUnit>(method => new MethodUnit(method)).ToArray();
		}

		public static MethodUnit[] GetConstructorsByInstance(object instance)
		{
			return GetConstructors(instance.GetType());
		}

		public static MethodUnit[] GetConstructors(Type type)
		{
			return type.GetConstructors(_bindingFlags).Select<ConstructorInfo, MethodUnit>(constructor => new MethodUnit(constructor)).ToArray();
		}
	}
}
