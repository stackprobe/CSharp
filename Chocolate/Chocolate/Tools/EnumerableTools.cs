using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class EnumerableTools
	{
		public static IEnumerable<T> Linearize<T>(IEnumerable<IEnumerable<T>> src)
		{
			foreach (IEnumerable<T> part in src)
				foreach (T element in part)
					yield return element;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="enu1"></param>
		/// <param name="enu2"></param>
		/// <param name="destOnly1">null可</param>
		/// <param name="destBoth1">null可</param>
		/// <param name="destBoth2">null可</param>
		/// <param name="destOnly2">null可</param>
		/// <param name="comp"></param>
		public static void Merge<T>(IEnumerable<T> enu1, IEnumerable<T> enu2, Action<T> destOnly1, Action<T> destBoth1, Action<T> destBoth2, Action<T> destOnly2, Comparison<T> comp)
		{
			EnumeratorCartridge<T> reader1 = new EnumeratorCartridge<T>(enu1.GetEnumerator()).Seek();
			EnumeratorCartridge<T> reader2 = new EnumeratorCartridge<T>(enu2.GetEnumerator()).Seek();

			if (destOnly1 == null)
				destOnly1 = v => { };

			if (destBoth1 == null)
				destBoth1 = v => { };

			if (destBoth2 == null)
				destBoth2 = v => { };

			if (destOnly2 == null)
				destOnly2 = v => { };

#if true
			while (reader1.HasCurrent() && reader2.HasCurrent())
			{
				int ret = comp(reader1.Current, reader2.Current);

				if (ret < 0)
				{
					destOnly1(reader1.Current);
					reader1.Next();
				}
				else if (0 < ret)
				{
					destOnly2(reader2.Current);
					reader2.Next();
				}
				else
				{
					destBoth1(reader1.Current);
					destBoth2(reader2.Current);
					reader1.Next();
					reader2.Next();
				}
			}
			while (reader1.HasCurrent())
			{
				destOnly1(reader1.Current);
				reader1.Next();
			}
			while (reader2.HasCurrent())
			{
				destOnly2(reader2.Current);
				reader2.Next();
			}
#else // same_code
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
					destOnly1(reader1.Current);
					reader1.Next();
				}
				else if (0 < ret)
				{
					destOnly2(reader2.Current);
					reader2.Next();
				}
				else
				{
					destBoth1(reader1.Current);
					destBoth2(reader2.Current);
					reader1.Next();
					reader2.Next();
				}
			}
#endif
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

		public static IEnumerable<T> Repeat<T>(T element, int count)
		{
			while (1 <= count)
			{
				yield return element;
				count--;
			}
		}

		public static IEnumerable<T> Reverse<T>(T[] src)
		{
			for (int index = src.Length - 1; 0 <= index; index--)
			{
				yield return src[index];
			}
		}
	}
}
