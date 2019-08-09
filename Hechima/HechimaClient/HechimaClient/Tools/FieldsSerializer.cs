using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Charlotte.Tools
{
	public static class FieldsSerializer
	{
		public static string[] serialize(object src)
		{
			List<string> dest = new List<string>();

			foreach (FieldInfo fi in ReflecTools.getFields(src))
			{
				object value = ReflecTools.getValue(fi, src);

				dest.Add(fi.Name);

				if (value == null)
					dest.Add("null");
				else
					dest.Add("=" + getString(fi, value));
			}
			return dest.ToArray();
		}

		public static void deserialize(object dest, string[] src)
		{
			for (int index = 0; index < src.Length; index += 2)
			{
				FieldInfo fi = ReflecTools.getField(dest, src[index]);
				string value = src[index + 1];
				object objval;

				if (value == "null")
					objval = null;
				else
					objval = getObject(fi, value.Substring(1));

				ReflecTools.setValue(fi, dest, objval);
			}
		}

		public static string getString(FieldInfo fi, object src)
		{
			if (ReflecTools.equals(fi, typeof(string)))
			{
				return StringTools.escape((string)src);
			}
			if (ReflecTools.equals(fi, typeof(int)))
			{
				return "" + (int)src;
			}
			if (ReflecTools.equals(fi, typeof(long)))
			{
				return "" + (long)src;
			}
			if (ReflecTools.equals(fi, typeof(bool)))
			{
				return StringTools.toString((bool)src);
			}
			if (ReflecTools.equalsOrBase(fi, typeof(Serializable)))
			{
				return StringTools.encodeLines(serialize(src));
			}
			throw new Exception("そんなタイプ知りません：" + fi);
		}

		public static object getObject(FieldInfo fi, string src)
		{
			if (ReflecTools.equals(fi, typeof(string)))
			{
				return StringTools.unescape(src);
			}
			if (ReflecTools.equals(fi, typeof(int)))
			{
				return int.Parse(src);
			}
			if (ReflecTools.equals(fi, typeof(long)))
			{
				return long.Parse(src);
			}
			if (ReflecTools.equals(fi, typeof(bool)))
			{
				return StringTools.toFlag(src);
			}
			if (ReflecTools.equalsOrBase(fi, typeof(Serializable)))
			{
				object dest = fi.FieldType.InvokeMember(null, BindingFlags.CreateInstance, null, null, null);
				deserialize(dest, StringTools.decodeLines(src));
				return dest;
			}
			throw new Exception("そんなタイプ知りません：" + fi);
		}

		public interface Serializable
		{ }
	}
}
