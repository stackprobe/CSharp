using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class EnumerableTools
	{
		public static void Merge<T>(IEnumerable<T> enu1, IEnumerable<T> enu2, Action<T> destOnly1, Action<T> destBoth1, Action<T> destBoth2, Action<T> destOnly2, Comparison<T> comp)
		{
			EnumeratorCartridge<T> reader1 = new EnumeratorCartridge<T>(enu1.GetEnumerator()).Seek();
			EnumeratorCartridge<T> reader2 = new EnumeratorCartridge<T>(enu2.GetEnumerator()).Seek();

			for (; ; )
			{
				int ret;

				if (reader1.HasCurrent() == false)
				{
					if (reader2.HasCurrent() == false)
						break;

					ret = 1;
				}
				else if (reader2.HasCurrent() == false)
				{
					ret = -1;
				}
				else
				{
					ret = comp(reader1.Current, reader2.Current);
				}

				if (ret < 0)
				{
					if (destOnly1 != null)
						destOnly1(reader1.Current);

					reader1.Next();
				}
				else if (0 < ret)
				{
					if (destOnly2 != null)
						destOnly2(reader2.Current);

					reader2.Next();
				}
				else
				{
					if (destBoth1 != null)
						destBoth1(reader1.Current);

					if (destBoth2 != null)
						destBoth2(reader2.Current);

					reader1.Next();
					reader2.Next();
				}
			}
		}

		public static void CollectMergedPairs<T>(IEnumerable<T> enu1, IEnumerable<T> enu2, Action<T[]> dest, T defval, Comparison<T> comp)
		{
			EnumeratorCartridge<T> reader1 = new EnumeratorCartridge<T>(enu1.GetEnumerator()).Seek();
			EnumeratorCartridge<T> reader2 = new EnumeratorCartridge<T>(enu2.GetEnumerator()).Seek();

			for (; ; )
			{
				int ret;

				if (reader1.HasCurrent() == false)
				{
					if (reader2.HasCurrent() == false)
						break;

					ret = 1;
				}
				else if (reader2.HasCurrent() == false)
				{
					ret = -1;
				}
				else
				{
					ret = comp(reader1.Current, reader2.Current);
				}

				if (ret < 0)
				{
					dest(new T[] { reader1.Current, defval });
					reader1.Next();
				}
				else if (0 < ret)
				{
					dest(new T[] { defval, reader2.Current });
					reader2.Next();
				}
				else
				{
					dest(new T[] { reader1.Current, reader2.Current });
					reader1.Next();
					reader2.Next();
				}
			}
		}
	}
}
