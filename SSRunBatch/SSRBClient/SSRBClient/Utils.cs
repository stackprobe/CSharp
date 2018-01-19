using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte
{
	public class Utils
	{
		public static bool ContainsLPath(List<string> paths, string targetLPath)
		{
			foreach (string path in paths)
			{
				string lPath = Path.GetFileName(path);

				if (EqualsIgnoreCase(lPath, targetLPath))
					return true;
			}
			return false;
		}

		public static bool EqualsIgnoreCase(string a, string b)
		{
			return a.ToLower() == b.ToLower();
		}

		public static string ToString_Span(DateTime startedTime, DateTime endedTime)
		{
			return ToString(endedTime) + " - " + ToString(startedTime) + " = " + (endedTime - startedTime).TotalMilliseconds + " millis";
		}

		public static string ToString(DateTime dt)
		{
			return dt.ToString("yyyy/MM/dd HH:mm:ss.fff");
		}

		public static int ToInt(string str, int minval, int maxval, string errorMessage)
		{
			try
			{
				int value = int.Parse(str);

				if (value < minval || maxval < value)
					throw null;

				return value;
			}
			catch (Exception e)
			{
				throw new Exception(errorMessage, e);
			}
		}
	}
}
